using eCOFIN.Infrastructure.BackgroundJobs;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14,
        shared: true)
    .CreateLogger();

builder.Host.UseSerilog();

builder.WebHost.UseUrls("http://localhost:5000");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------------------------------------------------------------
// SqlCompatibilityLevel — set to 100 for SQL Server 2008, 0 to skip (modern).
// SqlMaxBatchSize       — set to 1 for SQL Server 2008. This is the critical
// ---------------------------------------------------------------------------
int sqlCompatibilityLevel = builder.Configuration.GetValue<int>("SqlCompatibilityLevel");
int sqlMaxBatchSize = builder.Configuration.GetValue<int>("SqlMaxBatchSize");

builder.Services.AddDbContext<BilzFinDbContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(180);

            if (sqlCompatibilityLevel > 0)
            {
                sqlOptions.UseCompatibilityLevel(sqlCompatibilityLevel);
                Log.Information(
                    "EF Core SQL compatibility level set to {Level} (legacy SQL Server mode)",
                    sqlCompatibilityLevel);
            }

            if (sqlMaxBatchSize > 0)
            {
                sqlOptions.MaxBatchSize(sqlMaxBatchSize);
                Log.Information(
                    "EF Core SQL MaxBatchSize set to {Size} (legacy SQL Server mode)",
                    sqlMaxBatchSize);
            }
        });

    if (builder.Environment.IsDevelopment())
    {
        opt.EnableDetailedErrors();
    }
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ---------------------------------------------------------------------------
// Required by DefinePrefixService, which takes IMemoryCache in its constructor
// to cache the account and customer lookup lists. Must be registered BEFORE
// builder.Build() or DI fails to construct IDefinePrefixService at startup.
// ---------------------------------------------------------------------------
builder.Services.AddMemoryCache();

var infraAsm = typeof(BilzFinDbContext).Assembly;
foreach (var impl in infraAsm.GetTypes().Where(t => t.IsClass && t.Name.EndsWith("Service")))
{
    var iface = impl.GetInterfaces().FirstOrDefault(i => i.Name == $"I{impl.Name}");
    if (iface != null)
        builder.Services.AddScoped(iface, impl);
}

builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<LedgerBackgroundService>();

const string CorsPolicy = "WebApp";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(CorsPolicy, p => p
        .WithOrigins(
            "http://localhost:4300",
            "http://localhost:5000",
            "http://projects.aisofttech.com")
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();
app.UsePathBase("/eCOFIN_API");

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

app.Use(async (ctx, next) =>
{
    var requestId = Guid.NewGuid().ToString("N");
    ctx.Response.Headers["X-Correlation-Id"] = requestId;

    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Unhandled exception. CorrelationId={CorrelationId}", requestId);
        if (!ctx.Response.HasStarted)
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsJsonAsync(new
            {
                success = false,
                status = 500,
                message = "An unexpected error occurred.",
                correlationId = requestId
            });
        }
    }
});

app.UseSwagger();
app.UseRouting();
app.UseCors(CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapMethods("{*path}", new[] { "OPTIONS" }, () => Results.NoContent());
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/eCOFIN_API/swagger/v1/swagger.json", "eCOFIN API V1");
    c.RoutePrefix = string.Empty;
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/eCOFIN_API/index.html");
    return Task.CompletedTask;
});

try
{
    Log.Information("Starting eCOFIN API...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "eCOFIN API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
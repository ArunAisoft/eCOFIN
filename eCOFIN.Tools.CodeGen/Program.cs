using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using eCOFIN.Infrastructure.Context;

static class CSharpName
{
    public static string Of(Type t)
    {
        var type = Nullable.GetUnderlyingType(t) ?? t;
        var alias = type == typeof(int) ? "int"
                 : type == typeof(short) ? "short"
                 : type == typeof(long) ? "long"
                 : type == typeof(bool) ? "bool"
                 : type == typeof(string) ? "string"
                 : type == typeof(decimal) ? "decimal"
                 : type == typeof(double) ? "double"
                 : type == typeof(float) ? "float"
                 : type == typeof(byte) ? "byte"
                 : type == typeof(DateTime) ? "DateTime"
                 : type == typeof(Guid) ? "Guid"
                 : type == typeof(byte[]) ? "byte[]"
                 : type.Name;
        var nullable = Nullable.GetUnderlyingType(t) != null ? "?" : "";
        return alias + nullable;
    }
}

static class Paths
{
    public static string Root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
    public static string Api = Path.Combine(Root, "eCOFIN.API");
    public static string App = Path.Combine(Root, "eCOFIN.Application");
    public static string Infra = Path.Combine(Root, "eCOFIN.Infrastructure");

    public static string Dtos = Path.Combine(App, "DTOs");
    public static string Ifaces = Path.Combine(App, "Interfaces");
    public static string Services = Path.Combine(Infra, "Services");
    public static string Controllers = Path.Combine(Api, "Controllers");
    public static string Mappings = Path.Combine(App, "Mappings");

    public static void Ensure()
    {
        Directory.CreateDirectory(Dtos);
        Directory.CreateDirectory(Ifaces);
        Directory.CreateDirectory(Services);
        Directory.CreateDirectory(Controllers);
        Directory.CreateDirectory(Mappings);
    }
}

class Program
{
    static int Main()
    {
        Console.WriteLine("== CodeGen starting ==");
        Paths.Ensure();

        // Build DbContextOptions with your connection string
        var optionsBuilder = new DbContextOptionsBuilder<BilzFinDbContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-MRP\\SQLEXPRESS;Database=BilzFinDB;User Id=sa;Password=home@38;TrustServerCertificate=True");

        // Create context
        using var ctx = new BilzFinDbContext(optionsBuilder.Options);

        // Get all entities
        var entities = ctx.Model.GetEntityTypes()
            .Where(e => !e.IsOwned())
            .OrderBy(e => e.ClrType.Name)
            .ToList();

        var maps = new StringBuilder();
        maps.AppendLine("using AutoMapper;");
        maps.AppendLine("using eCOFIN.Domain.Entities;");
        maps.AppendLine("using eCOFIN.Application.DTOs;");
        maps.AppendLine("");
        maps.AppendLine("namespace eCOFIN.Application.Mappings");
        maps.AppendLine("{");
        maps.AppendLine("    public class MappingProfile : Profile");
        maps.AppendLine("    {");
        maps.AppendLine("        public MappingProfile()");
        maps.AppendLine("        {");

        foreach (var e in entities) GenerateForEntity(e, maps);

        maps.AppendLine("        }");
        maps.AppendLine("    }");
        maps.AppendLine("}");

        File.WriteAllText(Path.Combine(Paths.Mappings, "MappingProfile.cs"), maps.ToString());

        Console.WriteLine("== CodeGen done. ==");
        return 0;
    }

    static void GenerateForEntity(IEntityType e, StringBuilder maps)
    {
        var entityType = e.ClrType;
        var entityName = entityType.Name;

        var key = e.FindPrimaryKey();
        if (key == null || key.Properties.Count != 1)
        {
            Console.WriteLine($"[WARN] Skipping {entityName}: composite/no key not supported.");
            return;
        }

        var keyProp = key.Properties.Single();
        var keyTypeName = CSharpName.Of(keyProp.ClrType);
        var keyName = keyProp.Name;

        var navProps = e.GetNavigations().Select(n => n.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var scalarProps = e.GetProperties()
            .Where(p => !p.IsShadowProperty() && !navProps.Contains(p.Name))
            .ToList();

        // DTO
        File.WriteAllText(Path.Combine(Paths.Dtos, $"{entityName}Dto.cs"),
            Dto(entityName, scalarProps));

        // Interface
        File.WriteAllText(Path.Combine(Paths.Ifaces, $"I{entityName}Service.cs"),
            Interface(entityName, keyTypeName));

        // Service
        File.WriteAllText(Path.Combine(Paths.Services, $"{entityName}Service.cs"),
            Service(entityName, keyName, keyTypeName));

        // Controller
        File.WriteAllText(Path.Combine(Paths.Controllers, $"{entityName}Controller.cs"),
            Controller(entityName, keyName, keyTypeName));

        // AutoMapper
        maps.AppendLine($"            CreateMap<{entityName}, {entityName}Dto>().ReverseMap();");

        Console.WriteLine($"Generated: {entityName}");
    }

    static string Dto(string entity, System.Collections.Generic.IEnumerable<IProperty> props)
    {
        var sb = new StringBuilder();
        sb.AppendLine("namespace eCOFIN.Application.DTOs");
        sb.AppendLine("{");
        sb.AppendLine($"    public class {entity}Dto");
        sb.AppendLine("    {");
        foreach (var p in props)
            sb.AppendLine($"        public {CSharpName.Of(p.ClrType)} {p.Name} {{ get; set; }}");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }

    static string Interface(string entity, string keyType)
    {
        return $@"namespace eCOFIN.Application.Interfaces
{{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface I{entity}Service
    {{
        Task<IEnumerable<{entity}Dto>> GetAllAsync();
        Task<{entity}Dto?> GetByIdAsync({keyType} id);
        Task<{entity}Dto> CreateAsync({entity}Dto dto);
        Task<{entity}Dto> UpdateAsync({entity}Dto dto);
        Task<bool> DeleteAsync({keyType} id);
    }}
}}";
    }

    static string Service(string entity, string keyName, string keyType)
    {
        return $@"using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Domain.Entities;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{{
    public class {entity}Service : I{entity}Service
    {{
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public {entity}Service(BilzFinDbContext context, IMapper mapper)
        {{
            _context = context;
            _mapper = mapper;
        }}

        public async Task<IEnumerable<{entity}Dto>> GetAllAsync()
        {{
            var entities = await _context.Set<{entity}>().ToListAsync();
            return _mapper.Map<IEnumerable<{entity}Dto>>(entities);
        }}

        public async Task<{entity}Dto?> GetByIdAsync({keyType} id)
        {{
            var entity = await _context.Set<{entity}>().FindAsync(id);
            return _mapper.Map<{entity}Dto?>(entity);
        }}

        public async Task<{entity}Dto> CreateAsync({entity}Dto dto)
        {{
            var entity = _mapper.Map<{entity}>(dto);
            _context.Set<{entity}>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<{entity}Dto>(entity);
        }}

        public async Task<{entity}Dto> UpdateAsync({entity}Dto dto)
        {{
            var entity = await _context.Set<{entity}>().FindAsync(dto.{keyName});
            if (entity == null) throw new KeyNotFoundException(""{entity} not found"");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<{entity}Dto>(entity);
        }}

        public async Task<bool> DeleteAsync({keyType} id)
        {{
            var entity = await _context.Set<{entity}>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }}
    }}
}}";
    }

    static string Controller(string entity, string keyName, string keyType)
    {
        return $@"using Microsoft.AspNetCore.Mvc;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.API.Controllers
{{
    [ApiController]
    [Route(""api/[controller]"")]
    public class {entity}Controller : ControllerBase
    {{
        private readonly I{entity}Service _service;
        public {entity}Controller(I{entity}Service service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet(""{{id}}"")]
        public async Task<IActionResult> GetById({keyType} id)
        {{
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] {entity}Dto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] {entity}Dto dto)
            => Ok(await _service.UpdateAsync(dto));

        [HttpDelete(""{{id}}"")]
        public async Task<IActionResult> Delete({keyType} id)
            => Ok(await _service.DeleteAsync(id));
    }}
}}";
    }
}

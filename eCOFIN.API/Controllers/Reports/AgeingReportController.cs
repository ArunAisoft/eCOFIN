using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgeingReportController : ControllerBase
    {
        private readonly IAgeingReportService _svc;

        public AgeingReportController(IAgeingReportService svc)
        {
            _svc = svc;
        }

        /// <summary>
        /// Returns Debtors or Creditors ageing analysis rows as at the specified date.
        /// Query params: reportType ("Debtors" | "Creditors"), reportDate (yyyy-MM-dd).
        /// </summary>
        [HttpGet("GetAgeingReport")]
        public async Task<IActionResult> GetAgeingReport(
            [FromQuery] string reportType,
            [FromQuery] DateTime reportDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(reportType) ||
                    (reportType != "Debtors" && reportType != "Creditors"))
                    return Ok(new
                    {
                        success = false,
                        status = 201,
                        message = "reportType must be 'Debtors' or 'Creditors'."
                    });

                var data = reportType == "Debtors"
                    ? await _svc.GetDebtorsAgeingAsync(reportDate)
                    : await _svc.GetCreditorsAgeingAsync(reportDate);

                if (data == null || !data.Any())
                    return Ok(new
                    {
                        success = false,
                        status = 201,
                        message = $"No {reportType} ageing records found for {reportDate:dd-MM-yyyy}.",
                        data = (object?)null
                    });

                return Ok(new
                {
                    success = true,
                    status = 200,
                    message = $"{reportType} ageing report retrieved successfully.",
                    data
                });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, status = 500, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    status = 500,
                    message = "Unexpected error.",
                    error = ex.Message
                });
            }
        }
    }
}

using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Vouchers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanksAndAccountsController : ControllerBase
    {
        private readonly IBanksAndAccountsService _bankService;
        private readonly ILogger<BanksAndAccountsController> _logger;

        public BanksAndAccountsController(IBanksAndAccountsService bankService, ILogger<BanksAndAccountsController> logger)
        {
            _bankService = bankService;
            _logger = logger;
        }

        [HttpGet("GetAllBanksWithAccounts")]
        public async Task<IActionResult> GetAllBanksWithAccounts()
        {
            try
            {
                var banks = await _bankService.GetAllBanksWithAccountsAsync(
                    HttpContext.RequestAborted);

                if (banks == null || !banks.Any())
                    return Ok(new { success = false, status = 404, message = "No Banks found." });

                return Ok(new { success = true, status = 200, message = "Banks with Accounts retrieved successfully.", data = banks });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Banks with Accounts.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Banks with Accounts.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Banks with Accounts.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Banks with Accounts.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllBanksWithAccountsVouchersBalances")]
        public async Task<IActionResult> GetAllBanksWithAccountsVouchersBalances([FromQuery] string userName, [FromQuery] string voucherGroup)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(voucherGroup))
                return BadRequest(new { success = false, status = 400, message = "Both Username and Voucher Group are required." });

            try
            {
                var banks = await _bankService.GetAllBanksWithAccountsVouchersBalancesAsync(userName.Trim(), voucherGroup.Trim(), HttpContext.RequestAborted);

                if (banks == null || !banks.Any())
                    return Ok(new { success = false, status = 404, message = "No Banks found for the specified Username and Voucher Group." });

                return Ok(new { success = true, status = 200, message = "Banks with Accounts, Balances & Voucher Types retrieved successfully.", data = banks });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Banks with Accounts, Balances & Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Banks with Accounts, Balances & Voucher Types.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Banks with Accounts, Balances & Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Banks with Accounts, Balances & Voucher Types.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllAccountsVouchersBalances")]
        public async Task<IActionResult> GetAllAccountsVouchersBalances([FromQuery] string userName, [FromQuery] string accountType, [FromQuery] string voucherGroup)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(accountType) || string.IsNullOrWhiteSpace(voucherGroup))
                return BadRequest(new { success = false, status = 400, message = "Username, Account Type and Voucher Group are all required." });

            try
            {
                var accounts = await _bankService.GetAllAccountsVouchersBalancesAsync(userName.Trim(), accountType.Trim(), voucherGroup.Trim(), HttpContext.RequestAborted);

                if (accounts == null || !accounts.Any())
                    return Ok(new { success = false, status = 404, message = "No Bank Accounts found for the specified Username, Account Type and Voucher Group." });

                return Ok(new { success = true, status = 200, message = "Bank Accounts, Balances & Voucher Types retrieved successfully.", data = accounts });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Bank Accounts, Balances & Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Bank Accounts, Balances & Voucher Types.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Bank Accounts, Balances & Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Bank Accounts, Balances & Voucher Types.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllGroupAccountsByVoucherType")]
        public async Task<IActionResult> GetAllGroupAccountsByVoucherType(
            [FromQuery] string voucherType)
        {
            if (string.IsNullOrWhiteSpace(voucherType))
                return BadRequest(new { success = false, status = 400, message = "Voucher Type is required." });

            try
            {
                var accounts = await _bankService.GetAllGroupAccountsByVoucherTypeAsync(
                    voucherType.Trim(), HttpContext.RequestAborted);

                if (accounts == null || !accounts.Any())
                    return Ok(new { success = false, status = 404, message = "No Group Accounts found for the specified Voucher Type." });

                return Ok(new { success = true, status = 200, message = "Group Accounts retrieved successfully.", data = accounts });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Group Accounts.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Group Accounts.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Group Accounts.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Group Accounts.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllSubAccountsByCodeAndType")]
        public async Task<IActionResult> GetAllSubAccountsByCodeAndType([FromQuery] string accountCode, [FromQuery] string accountType, [FromQuery] bool includeCostCentres = false)
        {
            if (string.IsNullOrWhiteSpace(accountCode) || string.IsNullOrWhiteSpace(accountType))
                return BadRequest(new { success = false, status = 400, message = "Both Account Code and Account Type are required." });

            try
            {
                var accounts = await _bankService.GetAllSubAccountsByCodeAndTypeAsync(
                    accountCode.Trim(), accountType.Trim(), includeCostCentres, HttpContext.RequestAborted);

                if (accounts == null || !accounts.Any())
                    return Ok(new { success = false, status = 404, message = "No Sub Accounts found for the specified Account Code and Account Type." });

                return Ok(new { success = true, status = 200, message = "Sub Accounts retrieved successfully.", data = accounts });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Sub Accounts.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Sub Accounts.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Sub Accounts.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Sub Accounts.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllCreditAccountsVouchers")]
        public async Task<IActionResult> GetAllCreditAccountsVouchers([FromQuery] string userName, [FromQuery] string voucherGroup)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(voucherGroup))
                return BadRequest(new { success = false, status = 400, message = "Both Username and Voucher Group are required." });

            try
            {
                var accounts = await _bankService.GetAllCreditAccountsVouchersAsync(
                    userName.Trim(), voucherGroup.Trim(), HttpContext.RequestAborted);

                if (accounts == null || !accounts.Any())
                    return Ok(new { success = false, status = 404, message = "No Credit Accounts found for the specified Username and Voucher Group." });

                return Ok(new { success = true, status = 200, message = "Credit Accounts with Voucher Types retrieved successfully.", data = accounts });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Credit Accounts with Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Credit Accounts with Voucher Types.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Credit Accounts with Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Credit Accounts with Voucher Types.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllDebitAccountsVouchers")]
        public async Task<IActionResult> GetAllDebitAccountsVouchers([FromQuery] string userName, [FromQuery] string voucherGroup)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(voucherGroup))
                return BadRequest(new { success = false, status = 400, message = "Both Username and Voucher Group are required." });

            try
            {
                var accounts = await _bankService.GetAllDebitAccountsVouchersAsync(
                    userName.Trim(), voucherGroup.Trim(), HttpContext.RequestAborted);

                if (accounts == null || !accounts.Any())
                    return Ok(new { success = false, status = 404, message = "No Debit Accounts found for the specified Username and Voucher Group." });

                return Ok(new { success = true, status = 200, message = "Debit Accounts with Voucher Types retrieved successfully.", data = accounts });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Debit Accounts with Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Debit Accounts with Voucher Types.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Debit Accounts with Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Debit Accounts with Voucher Types.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllCreditDebitAccountsVouchers")]
        public async Task<IActionResult> GetAllCreditDebitAccountsVouchers([FromQuery] string userName, [FromQuery] string voucherGroup)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(voucherGroup))
                return BadRequest(new { success = false, status = 400, message = "Both Username and Voucher Group are required." });

            try
            {
                var accounts = await _bankService.GetAllCreditDebitAccountsVouchersAsync(
                    userName.Trim(), voucherGroup.Trim(), HttpContext.RequestAborted);

                if (accounts == null || !accounts.Any())
                    return Ok(new { success = false, status = 404, message = "No Credit/Debit Accounts found for the specified Username and Voucher Group." });

                return Ok(new { success = true, status = 200, message = "Credit/Debit Accounts with Voucher Types retrieved successfully.", data = accounts });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Credit/Debit Accounts with Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Credit/Debit Accounts with Voucher Types.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Credit/Debit Accounts with Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Credit/Debit Accounts with Voucher Types.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllDebitCreditVoucherTypes")]
        public async Task<IActionResult> GetAllDebitCreditVoucherTypes([FromQuery] string userName, [FromQuery] string voucherGroup)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(voucherGroup))
                return BadRequest(new { success = false, status = 400, message = "Both Username and Voucher Group are required." });

            try
            {
                var voucherTypes = await _bankService.GetAllDebitCreditVoucherTypesAsync(
                    userName.Trim(), voucherGroup.Trim(), HttpContext.RequestAborted);

                if (voucherTypes == null || !voucherTypes.Any())
                    return Ok(new { success = false, status = 404, message = "No Voucher Types found for the specified Username and Voucher Group." });

                return Ok(new { success = true, status = 200, message = "Voucher Types retrieved successfully.", data = voucherTypes });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Voucher Types.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Voucher Types.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Voucher Types.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetVendorInvoices")]
        public async Task<IActionResult> GetVendorInvoices([FromQuery] string invoiceAccount, [FromQuery] string invoiceVendor)
        {
            if (string.IsNullOrWhiteSpace(invoiceAccount) || string.IsNullOrWhiteSpace(invoiceVendor))
                return BadRequest(new { success = false, status = 400, message = "Both Invoice Account and Invoice Vendor are required." });

            try
            {
                var invoices = await _bankService.GetVendorInvoicesAsync(invoiceAccount.Trim(), invoiceVendor.Trim(), HttpContext.RequestAborted);

                if (invoices == null || !invoices.Any())
                    return Ok(new { success = false, status = 404, message = "No invoices found for the specified account and vendor." });

                return Ok(new { success = true, status = 200, message = "Vendor invoices retrieved successfully.", data = invoices });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving vendor invoices.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving vendor invoices.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving vendor invoices.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving vendor invoices.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetBillAndPaymentDetails")]
        public async Task<IActionResult> GetBillAndPaymentDetails([FromQuery] string accountCode, [FromQuery] string subAccountCode)
        {
            if (string.IsNullOrWhiteSpace(accountCode) || string.IsNullOrWhiteSpace(subAccountCode))
                return BadRequest(new { success = false, status = 400, message = "Both Account Code and Sub Account Code are required." });

            try
            {
                var result = await _bankService.GetBillAndPaymentDetailsAsync(
                    accountCode.Trim(), subAccountCode.Trim(), HttpContext.RequestAborted);

                if (result == null || (!result.BillDetails.Any() && !result.PaymentDetails.Any()))
                    return Ok(new { success = false, status = 404, message = "No bills or payments found for the specified account and sub account." });

                return Ok(new { success = true, status = 200, message = "Bill & Payment details retrieved successfully.", data = result });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while retrieving Bill & Payment details.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while retrieving Bill & Payment details.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Bill & Payment details.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving Bill & Payment details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("SaveBillAndPaymentAdjustment")]
        public async Task<IActionResult> SaveBillAndPaymentAdjustment([FromBody] BillPaymentAdjustmentRequestDto request)
        {
            if (request?.Bill == null || request.Payments == null || !request.Payments.Any())
                return BadRequest(new { success = false, status = 400, message = "Bill and at least one Payment adjustment detail are required." });

            try
            {
                await _bankService.SaveBillAndPaymentAdjustmentAsync(request, HttpContext.RequestAborted);
                return Ok(new { success = true, status = 200, message = "Bill and Payment adjustment saved successfully.", data = request.Bill.VoucherNo });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Argument error while saving Bill & Payment adjustment.");
                return BadRequest(new { success = false, status = 400, message = "Invalid data provided for bill payment adjustment.", error = argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while saving Bill & Payment adjustment.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving bill payment adjustment.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while saving Bill & Payment adjustment.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving bill payment adjustment.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("CheckBillExists")]
        public async Task<IActionResult> CheckBillExists([FromQuery] string bankCode, [FromQuery] string bankAccount, [FromQuery] string billNo, [FromQuery] DateTime billDate, [FromQuery] string? excludeOnHoldNo = null)
        {
            if (string.IsNullOrWhiteSpace(bankCode) || string.IsNullOrWhiteSpace(bankAccount) || string.IsNullOrWhiteSpace(billNo))
                return BadRequest(new { success = false, status = 400, message = "Bank Code, Bank Account and Bill No are required." });

            try
            {
                bool exists = await _bankService.IsBillAlreadyExistsAsync(bankCode.Trim(), bankAccount.Trim(), billNo.Trim(), billDate, excludeOnHoldNo?.Trim(), HttpContext.RequestAborted);

                return Ok(new { success = true, status = 200, data = exists });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error while checking bill existence.");
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while checking bill existence.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while checking bill existence.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while checking bill existence.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
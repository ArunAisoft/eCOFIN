namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICreditNotesService
    {
        Task<IEnumerable<ExistingCreditNoteDto>> GetAllCreditNotesAsync(string accPeriod);
        Task<CreditNoteWithDetailsDto> GetCreditNoteWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldCreditNoteAsync(CreditNotesRequestDto request);
        Task<string> PostCreditNoteAsync(CreditNotesRequestDto request);
    }
}
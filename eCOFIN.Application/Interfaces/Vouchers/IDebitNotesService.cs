namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDebitNotesService
    {
        Task<IEnumerable<ExistingDebitNoteDto>> GetAllDebitNotesAsync(string accPeriod);
        Task<DebitNoteWithDetailsDto> GetDebitNoteWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldDebitNoteAsync(DebitNotesRequestDto request);
        Task<string> PostDebitNoteAsync(DebitNotesRequestDto request);
        Task<PostMultipleResult> PostMultipleDebitNotesAsync(List<string> onHoldNumbers, string accountingPeriod, string username, string locationCode);
    }
}
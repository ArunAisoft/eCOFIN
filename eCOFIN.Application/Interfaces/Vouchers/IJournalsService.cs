namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJournalsService
    {
        Task<IEnumerable<ExistingJournalDto>> GetAllJournalsAsync(string accPeriod);
        Task<JournalWithDetailsDto> GetJournalWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldJournalAsync(JournalsRequestDto request);
        Task<string> PostJournalAsync(JournalsRequestDto request);
    }
}
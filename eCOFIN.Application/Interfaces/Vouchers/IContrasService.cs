namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContrasService
    {
        Task<IEnumerable<ExistingContraDto>> GetAllContrasAsync(string accPeriod);
        Task<ContraWithDetailsDto> GetContraWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldContraAsync(ContrasRequestDto request);
        Task<string> PostContraAsync(ContrasRequestDto request);
    }
}
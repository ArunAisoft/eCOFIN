namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IAccCostPieceTabService
    {
        Task<IEnumerable<AccCostPieceTabDto>> GetAllAsync();
        Task<AccCostPieceTabDto?> GetByIdAsync(string id);
        Task<AccCostPieceTabDto> CreateAsync(AccCostPieceTabDto dto);
        Task<AccCostPieceTabDto> UpdateAsync(AccCostPieceTabDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
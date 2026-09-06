using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBudgetsanctionService : ICfnBudgetsanctionService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBudgetsanctionService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBudgetsanctionDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBudgetsanction>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBudgetsanctionDto>>(entities);
        }

        public async Task<CfnBudgetsanctionDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBudgetsanction>().FindAsync(id);
            return _mapper.Map<CfnBudgetsanctionDto?>(entity);
        }

        public async Task<CfnBudgetsanctionDto> CreateAsync(CfnBudgetsanctionDto dto)
        {
            var entity = _mapper.Map<CfnBudgetsanction>(dto);
            _context.Set<CfnBudgetsanction>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBudgetsanctionDto>(entity);
        }

        public async Task<CfnBudgetsanctionDto> UpdateAsync(CfnBudgetsanctionDto dto)
        {
            var entity = await _context.Set<CfnBudgetsanction>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBudgetsanction not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBudgetsanctionDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBudgetsanction>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBudgetService : ICfnBudgetService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBudgetService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBudgetDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBudget>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBudgetDto>>(entities);
        }

        public async Task<CfnBudgetDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBudget>().FindAsync(id);
            return _mapper.Map<CfnBudgetDto?>(entity);
        }

        public async Task<CfnBudgetDto> CreateAsync(CfnBudgetDto dto)
        {
            var entity = _mapper.Map<CfnBudget>(dto);
            _context.Set<CfnBudget>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBudgetDto>(entity);
        }

        public async Task<CfnBudgetDto> UpdateAsync(CfnBudgetDto dto)
        {
            var entity = await _context.Set<CfnBudget>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBudget not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBudgetDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBudget>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
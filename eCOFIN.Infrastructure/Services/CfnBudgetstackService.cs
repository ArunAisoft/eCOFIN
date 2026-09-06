using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBudgetstackService : ICfnBudgetstackService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBudgetstackService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBudgetstackDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBudgetstack>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBudgetstackDto>>(entities);
        }

        public async Task<CfnBudgetstackDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnBudgetstack>().FindAsync(id);
            return _mapper.Map<CfnBudgetstackDto?>(entity);
        }

        public async Task<CfnBudgetstackDto> CreateAsync(CfnBudgetstackDto dto)
        {
            var entity = _mapper.Map<CfnBudgetstack>(dto);
            _context.Set<CfnBudgetstack>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBudgetstackDto>(entity);
        }

        public async Task<CfnBudgetstackDto> UpdateAsync(CfnBudgetstackDto dto)
        {
            var entity = await _context.Set<CfnBudgetstack>().FindAsync(dto.CtrlSequenceno);
            if (entity == null) throw new KeyNotFoundException("CfnBudgetstack not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBudgetstackDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnBudgetstack>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnFinancialyearService : ICfnFinancialyearService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnFinancialyearService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnFinancialyearDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnFinancialyear>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnFinancialyearDto>>(entities);
        }

        public async Task<CfnFinancialyearDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnFinancialyear>().FindAsync(id);
            return _mapper.Map<CfnFinancialyearDto?>(entity);
        }

        public async Task<CfnFinancialyearDto> CreateAsync(CfnFinancialyearDto dto)
        {
            var entity = _mapper.Map<CfnFinancialyear>(dto);
            _context.Set<CfnFinancialyear>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnFinancialyearDto>(entity);
        }

        public async Task<CfnFinancialyearDto> UpdateAsync(CfnFinancialyearDto dto)
        {
            var entity = await _context.Set<CfnFinancialyear>().FindAsync(dto.Financialyear);
            if (entity == null) throw new KeyNotFoundException("CfnFinancialyear not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnFinancialyearDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnFinancialyear>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
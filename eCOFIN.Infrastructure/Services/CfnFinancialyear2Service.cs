using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnFinancialyear2Service : ICfnFinancialyear2Service
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnFinancialyear2Service(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnFinancialyear2Dto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnFinancialyear2>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnFinancialyear2Dto>>(entities);
        }

        public async Task<CfnFinancialyear2Dto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnFinancialyear2>().FindAsync(id);
            return _mapper.Map<CfnFinancialyear2Dto?>(entity);
        }

        public async Task<CfnFinancialyear2Dto> CreateAsync(CfnFinancialyear2Dto dto)
        {
            var entity = _mapper.Map<CfnFinancialyear2>(dto);
            _context.Set<CfnFinancialyear2>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnFinancialyear2Dto>(entity);
        }

        public async Task<CfnFinancialyear2Dto> UpdateAsync(CfnFinancialyear2Dto dto)
        {
            var entity = await _context.Set<CfnFinancialyear2>().FindAsync(dto.Financialyear);
            if (entity == null) throw new KeyNotFoundException("CfnFinancialyear2 not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnFinancialyear2Dto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnFinancialyear2>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
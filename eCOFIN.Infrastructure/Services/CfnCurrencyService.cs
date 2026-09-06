using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCurrencyService : ICfnCurrencyService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCurrencyService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCurrencyDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCurrency>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCurrencyDto>>(entities);
        }

        public async Task<CfnCurrencyDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCurrency>().FindAsync(id);
            return _mapper.Map<CfnCurrencyDto?>(entity);
        }

        public async Task<CfnCurrencyDto> CreateAsync(CfnCurrencyDto dto)
        {
            var entity = _mapper.Map<CfnCurrency>(dto);
            _context.Set<CfnCurrency>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCurrencyDto>(entity);
        }

        public async Task<CfnCurrencyDto> UpdateAsync(CfnCurrencyDto dto)
        {
            var entity = await _context.Set<CfnCurrency>().FindAsync(dto.Currencycode);
            if (entity == null) throw new KeyNotFoundException("CfnCurrency not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCurrencyDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCurrency>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
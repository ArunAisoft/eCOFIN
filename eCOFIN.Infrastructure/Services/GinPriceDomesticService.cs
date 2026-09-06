using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class GinPriceDomesticService : IGinPriceDomesticService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public GinPriceDomesticService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GinPriceDomesticDto>> GetAllAsync()
        {
            var entities = await _context.Set<GinPriceDomestic>().ToListAsync();
            return _mapper.Map<IEnumerable<GinPriceDomesticDto>>(entities);
        }

        public async Task<GinPriceDomesticDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<GinPriceDomestic>().FindAsync(id);
            return _mapper.Map<GinPriceDomesticDto?>(entity);
        }

        public async Task<GinPriceDomesticDto> CreateAsync(GinPriceDomesticDto dto)
        {
            var entity = _mapper.Map<GinPriceDomestic>(dto);
            _context.Set<GinPriceDomestic>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GinPriceDomesticDto>(entity);
        }

        public async Task<GinPriceDomesticDto> UpdateAsync(GinPriceDomesticDto dto)
        {
            var entity = await _context.Set<GinPriceDomestic>().FindAsync(dto.GinNo);
            if (entity == null) throw new KeyNotFoundException("GinPriceDomestic not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GinPriceDomesticDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<GinPriceDomestic>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
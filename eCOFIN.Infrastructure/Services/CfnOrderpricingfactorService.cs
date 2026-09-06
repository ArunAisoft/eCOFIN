using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnOrderpricingfactorService : ICfnOrderpricingfactorService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnOrderpricingfactorService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnOrderpricingfactorDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnOrderpricingfactor>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnOrderpricingfactorDto>>(entities);
        }

        public async Task<CfnOrderpricingfactorDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnOrderpricingfactor>().FindAsync(id);
            return _mapper.Map<CfnOrderpricingfactorDto?>(entity);
        }

        public async Task<CfnOrderpricingfactorDto> CreateAsync(CfnOrderpricingfactorDto dto)
        {
            var entity = _mapper.Map<CfnOrderpricingfactor>(dto);
            _context.Set<CfnOrderpricingfactor>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnOrderpricingfactorDto>(entity);
        }

        public async Task<CfnOrderpricingfactorDto> UpdateAsync(CfnOrderpricingfactorDto dto)
        {
            var entity = await _context.Set<CfnOrderpricingfactor>().FindAsync(dto.Pricingfactorcode);
            if (entity == null) throw new KeyNotFoundException("CfnOrderpricingfactor not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnOrderpricingfactorDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnOrderpricingfactor>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
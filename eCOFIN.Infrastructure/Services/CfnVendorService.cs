using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnVendorService : ICfnVendorService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnVendorService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnVendorDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnVendor>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnVendorDto>>(entities);
        }

        public async Task<CfnVendorDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnVendor>().FindAsync(id);
            return _mapper.Map<CfnVendorDto?>(entity);
        }

        public async Task<CfnVendorDto> CreateAsync(CfnVendorDto dto)
        {
            var entity = _mapper.Map<CfnVendor>(dto);
            _context.Set<CfnVendor>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVendorDto>(entity);
        }

        public async Task<CfnVendorDto> UpdateAsync(CfnVendorDto dto)
        {
            var entity = await _context.Set<CfnVendor>().FindAsync(dto.Vendorcode);
            if (entity == null) throw new KeyNotFoundException("CfnVendor not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVendorDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnVendor>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
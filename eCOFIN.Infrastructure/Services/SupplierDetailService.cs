using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class SupplierDetailService : ISupplierDetailService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public SupplierDetailService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDetailDto>> GetAllAsync()
        {
            var entities = await _context.Set<SupplierDetail>().ToListAsync();
            return _mapper.Map<IEnumerable<SupplierDetailDto>>(entities);
        }

        public async Task<SupplierDetailDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<SupplierDetail>().FindAsync(id);
            return _mapper.Map<SupplierDetailDto?>(entity);
        }

        public async Task<SupplierDetailDto> CreateAsync(SupplierDetailDto dto)
        {
            var entity = _mapper.Map<SupplierDetail>(dto);
            _context.Set<SupplierDetail>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierDetailDto>(entity);
        }

        public async Task<SupplierDetailDto> UpdateAsync(SupplierDetailDto dto)
        {
            var entity = await _context.Set<SupplierDetail>().FindAsync(dto.SuppCode1);
            if (entity == null) throw new KeyNotFoundException("SupplierDetail not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierDetailDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<SupplierDetail>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
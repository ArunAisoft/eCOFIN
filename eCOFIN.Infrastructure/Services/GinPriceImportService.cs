using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class GinPriceImportService : IGinPriceImportService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public GinPriceImportService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GinPriceImportDto>> GetAllAsync()
        {
            var entities = await _context.Set<GinPriceImport>().ToListAsync();
            return _mapper.Map<IEnumerable<GinPriceImportDto>>(entities);
        }

        public async Task<GinPriceImportDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<GinPriceImport>().FindAsync(id);
            return _mapper.Map<GinPriceImportDto?>(entity);
        }

        public async Task<GinPriceImportDto> CreateAsync(GinPriceImportDto dto)
        {
            var entity = _mapper.Map<GinPriceImport>(dto);
            _context.Set<GinPriceImport>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GinPriceImportDto>(entity);
        }

        public async Task<GinPriceImportDto> UpdateAsync(GinPriceImportDto dto)
        {
            var entity = await _context.Set<GinPriceImport>().FindAsync(dto.GinNo);
            if (entity == null) throw new KeyNotFoundException("GinPriceImport not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GinPriceImportDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<GinPriceImport>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
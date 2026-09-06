using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnProductService : ICfnProductService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnProductService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnProductDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnProduct>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnProductDto>>(entities);
        }

        public async Task<CfnProductDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnProduct>().FindAsync(id);
            return _mapper.Map<CfnProductDto?>(entity);
        }

        public async Task<CfnProductDto> CreateAsync(CfnProductDto dto)
        {
            var entity = _mapper.Map<CfnProduct>(dto);
            _context.Set<CfnProduct>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnProductDto>(entity);
        }

        public async Task<CfnProductDto> UpdateAsync(CfnProductDto dto)
        {
            var entity = await _context.Set<CfnProduct>().FindAsync(dto.Productcode);
            if (entity == null) throw new KeyNotFoundException("CfnProduct not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnProductDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnProduct>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
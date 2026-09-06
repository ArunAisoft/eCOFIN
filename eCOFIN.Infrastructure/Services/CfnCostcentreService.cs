using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCostcentreService : ICfnCostcentreService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCostcentreService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCostcentreDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCostcentre>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCostcentreDto>>(entities);
        }

        public async Task<CfnCostcentreDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCostcentre>().FindAsync(id);
            return _mapper.Map<CfnCostcentreDto?>(entity);
        }

        public async Task<CfnCostcentreDto> CreateAsync(CfnCostcentreDto dto)
        {
            var entity = _mapper.Map<CfnCostcentre>(dto);
            _context.Set<CfnCostcentre>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCostcentreDto>(entity);
        }

        public async Task<CfnCostcentreDto> UpdateAsync(CfnCostcentreDto dto)
        {
            var entity = await _context.Set<CfnCostcentre>().FindAsync(dto.Costcentrecode);
            if (entity == null) throw new KeyNotFoundException("CfnCostcentre not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCostcentreDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCostcentre>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
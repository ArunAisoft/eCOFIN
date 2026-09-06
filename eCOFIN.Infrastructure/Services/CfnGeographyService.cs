using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnGeographyService : ICfnGeographyService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnGeographyService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnGeographyDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnGeography>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnGeographyDto>>(entities);
        }

        public async Task<CfnGeographyDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnGeography>().FindAsync(id);
            return _mapper.Map<CfnGeographyDto?>(entity);
        }

        public async Task<CfnGeographyDto> CreateAsync(CfnGeographyDto dto)
        {
            var entity = _mapper.Map<CfnGeography>(dto);
            _context.Set<CfnGeography>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGeographyDto>(entity);
        }

        public async Task<CfnGeographyDto> UpdateAsync(CfnGeographyDto dto)
        {
            var entity = await _context.Set<CfnGeography>().FindAsync(dto.Geographycode);
            if (entity == null) throw new KeyNotFoundException("CfnGeography not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGeographyDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnGeography>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
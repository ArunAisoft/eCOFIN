using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnConaccfgService : ICfnConaccfgService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnConaccfgService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnConaccfgDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnConaccfg>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnConaccfgDto>>(entities);
        }

        public async Task<CfnConaccfgDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnConaccfg>().FindAsync(id);
            return _mapper.Map<CfnConaccfgDto?>(entity);
        }

        public async Task<CfnConaccfgDto> CreateAsync(CfnConaccfgDto dto)
        {
            var entity = _mapper.Map<CfnConaccfg>(dto);
            _context.Set<CfnConaccfg>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnConaccfgDto>(entity);
        }

        public async Task<CfnConaccfgDto> UpdateAsync(CfnConaccfgDto dto)
        {
            var entity = await _context.Set<CfnConaccfg>().FindAsync(dto.Accperiod);
            if (entity == null) throw new KeyNotFoundException("CfnConaccfg not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnConaccfgDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnConaccfg>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
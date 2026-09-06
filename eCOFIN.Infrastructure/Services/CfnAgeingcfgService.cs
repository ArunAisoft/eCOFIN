using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAgeingcfgService : ICfnAgeingcfgService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAgeingcfgService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAgeingcfgDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAgeingcfg>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAgeingcfgDto>>(entities);
        }

        public async Task<CfnAgeingcfgDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnAgeingcfg>().FindAsync(id);
            return _mapper.Map<CfnAgeingcfgDto?>(entity);
        }

        public async Task<CfnAgeingcfgDto> CreateAsync(CfnAgeingcfgDto dto)
        {
            var entity = _mapper.Map<CfnAgeingcfg>(dto);
            _context.Set<CfnAgeingcfg>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAgeingcfgDto>(entity);
        }

        public async Task<CfnAgeingcfgDto> UpdateAsync(CfnAgeingcfgDto dto)
        {
            var entity = await _context.Set<CfnAgeingcfg>().FindAsync(dto.Slno);
            if (entity == null) throw new KeyNotFoundException("CfnAgeingcfg not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAgeingcfgDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnAgeingcfg>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
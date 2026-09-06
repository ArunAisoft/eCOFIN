using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnConloccfgService : ICfnConloccfgService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnConloccfgService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnConloccfgDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnConloccfg>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnConloccfgDto>>(entities);
        }

        public async Task<CfnConloccfgDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnConloccfg>().FindAsync(id);
            return _mapper.Map<CfnConloccfgDto?>(entity);
        }

        public async Task<CfnConloccfgDto> CreateAsync(CfnConloccfgDto dto)
        {
            var entity = _mapper.Map<CfnConloccfg>(dto);
            _context.Set<CfnConloccfg>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnConloccfgDto>(entity);
        }

        public async Task<CfnConloccfgDto> UpdateAsync(CfnConloccfgDto dto)
        {
            var entity = await _context.Set<CfnConloccfg>().FindAsync(dto.Locationcode);
            if (entity == null) throw new KeyNotFoundException("CfnConloccfg not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnConloccfgDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnConloccfg>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
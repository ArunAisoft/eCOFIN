using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnLevelService : ICfnLevelService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnLevelService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnLevelDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnLevel>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnLevelDto>>(entities);
        }

        public async Task<CfnLevelDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnLevel>().FindAsync(id);
            return _mapper.Map<CfnLevelDto?>(entity);
        }

        public async Task<CfnLevelDto> CreateAsync(CfnLevelDto dto)
        {
            var entity = _mapper.Map<CfnLevel>(dto);
            _context.Set<CfnLevel>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnLevelDto>(entity);
        }

        public async Task<CfnLevelDto> UpdateAsync(CfnLevelDto dto)
        {
            var entity = await _context.Set<CfnLevel>().FindAsync(dto.Levelnumber);
            if (entity == null) throw new KeyNotFoundException("CfnLevel not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnLevelDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnLevel>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPlgroupService : ICfnPlgroupService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPlgroupService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPlgroupDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPlgroup>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPlgroupDto>>(entities);
        }

        public async Task<CfnPlgroupDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPlgroup>().FindAsync(id);
            return _mapper.Map<CfnPlgroupDto?>(entity);
        }

        public async Task<CfnPlgroupDto> CreateAsync(CfnPlgroupDto dto)
        {
            var entity = _mapper.Map<CfnPlgroup>(dto);
            _context.Set<CfnPlgroup>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPlgroupDto>(entity);
        }

        public async Task<CfnPlgroupDto> UpdateAsync(CfnPlgroupDto dto)
        {
            var entity = await _context.Set<CfnPlgroup>().FindAsync(dto.Groupcode);
            if (entity == null) throw new KeyNotFoundException("CfnPlgroup not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPlgroupDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPlgroup>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
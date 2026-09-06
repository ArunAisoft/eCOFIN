using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnHierarchyService : ICfnHierarchyService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnHierarchyService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnHierarchyDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnHierarchy>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnHierarchyDto>>(entities);
        }

        public async Task<CfnHierarchyDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnHierarchy>().FindAsync(id);
            return _mapper.Map<CfnHierarchyDto?>(entity);
        }

        public async Task<CfnHierarchyDto> CreateAsync(CfnHierarchyDto dto)
        {
            var entity = _mapper.Map<CfnHierarchy>(dto);
            _context.Set<CfnHierarchy>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnHierarchyDto>(entity);
        }

        public async Task<CfnHierarchyDto> UpdateAsync(CfnHierarchyDto dto)
        {
            var entity = await _context.Set<CfnHierarchy>().FindAsync(dto.Hierarchyid);
            if (entity == null) throw new KeyNotFoundException("CfnHierarchy not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnHierarchyDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnHierarchy>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
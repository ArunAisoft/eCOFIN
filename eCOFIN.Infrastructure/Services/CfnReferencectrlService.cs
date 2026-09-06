using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnReferencectrlService : ICfnReferencectrlService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnReferencectrlService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnReferencectrlDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnReferencectrl>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnReferencectrlDto>>(entities);
        }

        public async Task<CfnReferencectrlDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnReferencectrl>().FindAsync(id);
            return _mapper.Map<CfnReferencectrlDto?>(entity);
        }

        public async Task<CfnReferencectrlDto> CreateAsync(CfnReferencectrlDto dto)
        {
            var entity = _mapper.Map<CfnReferencectrl>(dto);
            _context.Set<CfnReferencectrl>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnReferencectrlDto>(entity);
        }

        public async Task<CfnReferencectrlDto> UpdateAsync(CfnReferencectrlDto dto)
        {
            var entity = await _context.Set<CfnReferencectrl>().FindAsync(dto.Referencetype);
            if (entity == null) throw new KeyNotFoundException("CfnReferencectrl not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnReferencectrlDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnReferencectrl>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
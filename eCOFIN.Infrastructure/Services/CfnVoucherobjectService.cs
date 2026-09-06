using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnVoucherobjectService : ICfnVoucherobjectService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnVoucherobjectService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnVoucherobjectDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnVoucherobject>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnVoucherobjectDto>>(entities);
        }

        public async Task<CfnVoucherobjectDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnVoucherobject>().FindAsync(id);
            return _mapper.Map<CfnVoucherobjectDto?>(entity);
        }

        public async Task<CfnVoucherobjectDto> CreateAsync(CfnVoucherobjectDto dto)
        {
            var entity = _mapper.Map<CfnVoucherobject>(dto);
            _context.Set<CfnVoucherobject>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVoucherobjectDto>(entity);
        }

        public async Task<CfnVoucherobjectDto> UpdateAsync(CfnVoucherobjectDto dto)
        {
            var entity = await _context.Set<CfnVoucherobject>().FindAsync(dto.Taskid);
            if (entity == null) throw new KeyNotFoundException("CfnVoucherobject not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVoucherobjectDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnVoucherobject>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnGoodsinvchService : ICfnGoodsinvchService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnGoodsinvchService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnGoodsinvchDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnGoodsinvch>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnGoodsinvchDto>>(entities);
        }

        public async Task<CfnGoodsinvchDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnGoodsinvch>().FindAsync(id);
            return _mapper.Map<CfnGoodsinvchDto?>(entity);
        }

        public async Task<CfnGoodsinvchDto> CreateAsync(CfnGoodsinvchDto dto)
        {
            var entity = _mapper.Map<CfnGoodsinvch>(dto);
            _context.Set<CfnGoodsinvch>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGoodsinvchDto>(entity);
        }

        public async Task<CfnGoodsinvchDto> UpdateAsync(CfnGoodsinvchDto dto)
        {
            var entity = await _context.Set<CfnGoodsinvch>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnGoodsinvch not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGoodsinvchDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnGoodsinvch>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
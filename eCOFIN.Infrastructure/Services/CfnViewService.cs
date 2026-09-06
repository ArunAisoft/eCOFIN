using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnViewService : ICfnViewService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnViewService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnViewDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnView>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnViewDto>>(entities);
        }

        public async Task<CfnViewDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnView>().FindAsync(id);
            return _mapper.Map<CfnViewDto?>(entity);
        }

        public async Task<CfnViewDto> CreateAsync(CfnViewDto dto)
        {
            var entity = _mapper.Map<CfnView>(dto);
            _context.Set<CfnView>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnViewDto>(entity);
        }

        public async Task<CfnViewDto> UpdateAsync(CfnViewDto dto)
        {
            var entity = await _context.Set<CfnView>().FindAsync(dto.Viewid);
            if (entity == null) throw new KeyNotFoundException("CfnView not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnViewDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnView>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
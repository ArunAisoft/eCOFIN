using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class GinMasterService : IGinMasterService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public GinMasterService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GinMasterDto>> GetAllAsync()
        {
            var entities = await _context.Set<GinMaster>().ToListAsync();
            return _mapper.Map<IEnumerable<GinMasterDto>>(entities);
        }

        public async Task<GinMasterDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<GinMaster>().FindAsync(id);
            return _mapper.Map<GinMasterDto?>(entity);
        }

        public async Task<GinMasterDto> CreateAsync(GinMasterDto dto)
        {
            var entity = _mapper.Map<GinMaster>(dto);
            _context.Set<GinMaster>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GinMasterDto>(entity);
        }

        public async Task<GinMasterDto> UpdateAsync(GinMasterDto dto)
        {
            var entity = await _context.Set<GinMaster>().FindAsync(dto.GinNo);
            if (entity == null) throw new KeyNotFoundException("GinMaster not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GinMasterDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<GinMaster>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
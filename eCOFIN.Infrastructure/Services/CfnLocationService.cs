using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnLocationService : ICfnLocationService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnLocationService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnLocationDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnLocation>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnLocationDto>>(entities);
        }

        public async Task<CfnLocationDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnLocation>().FindAsync(id);
            return _mapper.Map<CfnLocationDto?>(entity);
        }

        public async Task<CfnLocationDto> CreateAsync(CfnLocationDto dto)
        {
            var entity = _mapper.Map<CfnLocation>(dto);
            _context.Set<CfnLocation>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnLocationDto>(entity);
        }

        public async Task<CfnLocationDto> UpdateAsync(CfnLocationDto dto)
        {
            var entity = await _context.Set<CfnLocation>().FindAsync(dto.Locationcode);
            if (entity == null) throw new KeyNotFoundException("CfnLocation not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnLocationDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnLocation>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
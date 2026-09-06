using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class AutoDatapullService : IAutoDatapullService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public AutoDatapullService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutoDatapullDto>> GetAllAsync()
        {
            var entities = await _context.Set<AutoDatapull>().ToListAsync();
            return _mapper.Map<IEnumerable<AutoDatapullDto>>(entities);
        }

        public async Task<AutoDatapullDto?> GetByIdAsync(long id)
        {
            var entity = await _context.Set<AutoDatapull>().FindAsync(id);
            return _mapper.Map<AutoDatapullDto?>(entity);
        }

        public async Task<AutoDatapullDto> CreateAsync(AutoDatapullDto dto)
        {
            var entity = _mapper.Map<AutoDatapull>(dto);
            _context.Set<AutoDatapull>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AutoDatapullDto>(entity);
        }

        public async Task<AutoDatapullDto> UpdateAsync(AutoDatapullDto dto)
        {
            var entity = await _context.Set<AutoDatapull>().FindAsync(dto.RowId);
            if (entity == null) throw new KeyNotFoundException("AutoDatapull not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AutoDatapullDto>(entity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.Set<AutoDatapull>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
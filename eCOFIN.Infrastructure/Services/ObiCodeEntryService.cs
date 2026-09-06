using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class ObiCodeEntryService : IObiCodeEntryService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public ObiCodeEntryService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ObiCodeEntryDto>> GetAllAsync()
        {
            var entities = await _context.Set<ObiCodeEntry>().ToListAsync();
            return _mapper.Map<IEnumerable<ObiCodeEntryDto>>(entities);
        }

        public async Task<ObiCodeEntryDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<ObiCodeEntry>().FindAsync(id);
            return _mapper.Map<ObiCodeEntryDto?>(entity);
        }

        public async Task<ObiCodeEntryDto> CreateAsync(ObiCodeEntryDto dto)
        {
            var entity = _mapper.Map<ObiCodeEntry>(dto);
            _context.Set<ObiCodeEntry>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ObiCodeEntryDto>(entity);
        }

        public async Task<ObiCodeEntryDto> UpdateAsync(ObiCodeEntryDto dto)
        {
            var entity = await _context.Set<ObiCodeEntry>().FindAsync(dto.ArticleNo);
            if (entity == null) throw new KeyNotFoundException("ObiCodeEntry not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ObiCodeEntryDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<ObiCodeEntry>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
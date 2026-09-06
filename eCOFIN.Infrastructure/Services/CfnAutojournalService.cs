using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAutojournalService : ICfnAutojournalService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAutojournalService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAutojournalDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAutojournal>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAutojournalDto>>(entities);
        }

        public async Task<CfnAutojournalDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnAutojournal>().FindAsync(id);
            return _mapper.Map<CfnAutojournalDto?>(entity);
        }

        public async Task<CfnAutojournalDto> CreateAsync(CfnAutojournalDto dto)
        {
            var entity = _mapper.Map<CfnAutojournal>(dto);
            _context.Set<CfnAutojournal>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAutojournalDto>(entity);
        }

        public async Task<CfnAutojournalDto> UpdateAsync(CfnAutojournalDto dto)
        {
            var entity = await _context.Set<CfnAutojournal>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnAutojournal not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAutojournalDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnAutojournal>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnJournalService : ICfnJournalService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnJournalService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnJournalDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnJournal>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnJournalDto>>(entities);
        }

        public async Task<CfnJournalDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnJournal>().FindAsync(id);
            return _mapper.Map<CfnJournalDto?>(entity);
        }

        public async Task<CfnJournalDto> CreateAsync(CfnJournalDto dto)
        {
            var entity = _mapper.Map<CfnJournal>(dto);
            _context.Set<CfnJournal>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnJournalDto>(entity);
        }

        public async Task<CfnJournalDto> UpdateAsync(CfnJournalDto dto)
        {
            var entity = await _context.Set<CfnJournal>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnJournal not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnJournalDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnJournal>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
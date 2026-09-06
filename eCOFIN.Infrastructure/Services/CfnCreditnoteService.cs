using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCreditnoteService : ICfnCreditnoteService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCreditnoteService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCreditnoteDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCreditnote>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCreditnoteDto>>(entities);
        }

        public async Task<CfnCreditnoteDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCreditnote>().FindAsync(id);
            return _mapper.Map<CfnCreditnoteDto?>(entity);
        }

        public async Task<CfnCreditnoteDto> CreateAsync(CfnCreditnoteDto dto)
        {
            var entity = _mapper.Map<CfnCreditnote>(dto);
            _context.Set<CfnCreditnote>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCreditnoteDto>(entity);
        }

        public async Task<CfnCreditnoteDto> UpdateAsync(CfnCreditnoteDto dto)
        {
            var entity = await _context.Set<CfnCreditnote>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnCreditnote not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCreditnoteDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCreditnote>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
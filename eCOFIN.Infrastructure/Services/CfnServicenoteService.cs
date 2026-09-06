using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnServicenoteService : ICfnServicenoteService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnServicenoteService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnServicenoteDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnServicenote>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnServicenoteDto>>(entities);
        }

        public async Task<CfnServicenoteDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnServicenote>().FindAsync(id);
            return _mapper.Map<CfnServicenoteDto?>(entity);
        }

        public async Task<CfnServicenoteDto> CreateAsync(CfnServicenoteDto dto)
        {
            var entity = _mapper.Map<CfnServicenote>(dto);
            _context.Set<CfnServicenote>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnServicenoteDto>(entity);
        }

        public async Task<CfnServicenoteDto> UpdateAsync(CfnServicenoteDto dto)
        {
            var entity = await _context.Set<CfnServicenote>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnServicenote not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnServicenoteDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnServicenote>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
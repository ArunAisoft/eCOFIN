using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnDebitnoteService : ICfnDebitnoteService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnDebitnoteService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnDebitnoteDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnDebitnote>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnDebitnoteDto>>(entities);
        }

        public async Task<CfnDebitnoteDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnDebitnote>().FindAsync(id);
            return _mapper.Map<CfnDebitnoteDto?>(entity);
        }

        public async Task<CfnDebitnoteDto> CreateAsync(CfnDebitnoteDto dto)
        {
            var entity = _mapper.Map<CfnDebitnote>(dto);
            _context.Set<CfnDebitnote>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnDebitnoteDto>(entity);
        }

        public async Task<CfnDebitnoteDto> UpdateAsync(CfnDebitnoteDto dto)
        {
            var entity = await _context.Set<CfnDebitnote>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnDebitnote not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnDebitnoteDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnDebitnote>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
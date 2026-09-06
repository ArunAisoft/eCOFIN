using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAgeinghdrService : ICfnAgeinghdrService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAgeinghdrService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAgeinghdrDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAgeinghdr>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAgeinghdrDto>>(entities);
        }

        public async Task<CfnAgeinghdrDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnAgeinghdr>().FindAsync(id);
            return _mapper.Map<CfnAgeinghdrDto?>(entity);
        }

        public async Task<CfnAgeinghdrDto> CreateAsync(CfnAgeinghdrDto dto)
        {
            var entity = _mapper.Map<CfnAgeinghdr>(dto);
            _context.Set<CfnAgeinghdr>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAgeinghdrDto>(entity);
        }

        public async Task<CfnAgeinghdrDto> UpdateAsync(CfnAgeinghdrDto dto)
        {
            var entity = await _context.Set<CfnAgeinghdr>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnAgeinghdr not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAgeinghdrDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnAgeinghdr>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
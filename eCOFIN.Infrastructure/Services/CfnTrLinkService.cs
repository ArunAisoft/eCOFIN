using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTrLinkService : ICfnTrLinkService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTrLinkService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTrLinkDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTrLink>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTrLinkDto>>(entities);
        }

        public async Task<CfnTrLinkDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTrLink>().FindAsync(id);
            return _mapper.Map<CfnTrLinkDto?>(entity);
        }

        public async Task<CfnTrLinkDto> CreateAsync(CfnTrLinkDto dto)
        {
            var entity = _mapper.Map<CfnTrLink>(dto);
            _context.Set<CfnTrLink>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTrLinkDto>(entity);
        }

        public async Task<CfnTrLinkDto> UpdateAsync(CfnTrLinkDto dto)
        {
            var entity = await _context.Set<CfnTrLink>().FindAsync(dto.Accountcode);
            if (entity == null) throw new KeyNotFoundException("CfnTrLink not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTrLinkDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTrLink>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTdService : ICfnTdService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTdService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTdDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTd>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTdDto>>(entities);
        }

        public async Task<CfnTdDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTd>().FindAsync(id);
            return _mapper.Map<CfnTdDto?>(entity);
        }

        public async Task<CfnTdDto> CreateAsync(CfnTdDto dto)
        {
            var entity = _mapper.Map<CfnTd>(dto);
            _context.Set<CfnTd>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTdDto>(entity);
        }

        public async Task<CfnTdDto> UpdateAsync(CfnTdDto dto)
        {
            var entity = await _context.Set<CfnTd>().FindAsync(dto.Tdscode);
            if (entity == null) throw new KeyNotFoundException("CfnTd not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTdDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTd>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
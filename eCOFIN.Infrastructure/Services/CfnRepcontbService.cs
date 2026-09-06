using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnRepcontbService : ICfnRepcontbService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnRepcontbService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnRepcontbDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnRepcontb>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnRepcontbDto>>(entities);
        }

        public async Task<CfnRepcontbDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnRepcontb>().FindAsync(id);
            return _mapper.Map<CfnRepcontbDto?>(entity);
        }

        public async Task<CfnRepcontbDto> CreateAsync(CfnRepcontbDto dto)
        {
            var entity = _mapper.Map<CfnRepcontb>(dto);
            _context.Set<CfnRepcontb>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRepcontbDto>(entity);
        }

        public async Task<CfnRepcontbDto> UpdateAsync(CfnRepcontbDto dto)
        {
            var entity = await _context.Set<CfnRepcontb>().FindAsync(dto.Accountcode);
            if (entity == null) throw new KeyNotFoundException("CfnRepcontb not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRepcontbDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnRepcontb>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
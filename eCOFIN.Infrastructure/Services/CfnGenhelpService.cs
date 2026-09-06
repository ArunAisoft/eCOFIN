using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnGenhelpService : ICfnGenhelpService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnGenhelpService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnGenhelpDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnGenhelp>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnGenhelpDto>>(entities);
        }

        public async Task<CfnGenhelpDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnGenhelp>().FindAsync(id);
            return _mapper.Map<CfnGenhelpDto?>(entity);
        }

        public async Task<CfnGenhelpDto> CreateAsync(CfnGenhelpDto dto)
        {
            var entity = _mapper.Map<CfnGenhelp>(dto);
            _context.Set<CfnGenhelp>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGenhelpDto>(entity);
        }

        public async Task<CfnGenhelpDto> UpdateAsync(CfnGenhelpDto dto)
        {
            var entity = await _context.Set<CfnGenhelp>().FindAsync(dto.Helpid);
            if (entity == null) throw new KeyNotFoundException("CfnGenhelp not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGenhelpDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnGenhelp>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
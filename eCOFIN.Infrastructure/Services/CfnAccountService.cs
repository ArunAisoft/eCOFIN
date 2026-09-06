using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAccountService : ICfnAccountService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAccountService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAccountDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAccount>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAccountDto>>(entities);
        }

        public async Task<CfnAccountDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnAccount>().FindAsync(id);
            return _mapper.Map<CfnAccountDto?>(entity);
        }

        public async Task<CfnAccountDto> CreateAsync(CfnAccountDto dto)
        {
            var entity = _mapper.Map<CfnAccount>(dto);
            _context.Set<CfnAccount>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAccountDto>(entity);
        }

        public async Task<CfnAccountDto> UpdateAsync(CfnAccountDto dto)
        {
            var entity = await _context.Set<CfnAccount>().FindAsync(dto.Accountcode);
            if (entity == null) throw new KeyNotFoundException("CfnAccount not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAccountDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnAccount>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
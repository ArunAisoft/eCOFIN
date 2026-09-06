using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankService : ICfnBankService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBank>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankDto>>(entities);
        }

        public async Task<CfnBankDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBank>().FindAsync(id);
            return _mapper.Map<CfnBankDto?>(entity);
        }

        public async Task<CfnBankDto> CreateAsync(CfnBankDto dto)
        {
            var entity = _mapper.Map<CfnBank>(dto);
            _context.Set<CfnBank>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankDto>(entity);
        }

        public async Task<CfnBankDto> UpdateAsync(CfnBankDto dto)
        {
            var entity = await _context.Set<CfnBank>().FindAsync(dto.Bankcode);
            if (entity == null) throw new KeyNotFoundException("CfnBank not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBank>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankdepositService : ICfnBankdepositService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankdepositService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankdepositDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBankdeposit>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankdepositDto>>(entities);
        }

        public async Task<CfnBankdepositDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBankdeposit>().FindAsync(id);
            return _mapper.Map<CfnBankdepositDto?>(entity);
        }

        public async Task<CfnBankdepositDto> CreateAsync(CfnBankdepositDto dto)
        {
            var entity = _mapper.Map<CfnBankdeposit>(dto);
            _context.Set<CfnBankdeposit>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankdepositDto>(entity);
        }

        public async Task<CfnBankdepositDto> UpdateAsync(CfnBankdepositDto dto)
        {
            var entity = await _context.Set<CfnBankdeposit>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBankdeposit not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankdepositDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBankdeposit>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
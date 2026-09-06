using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankreceiptService : ICfnBankreceiptService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankreceiptService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankreceiptDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBankreceipt>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankreceiptDto>>(entities);
        }

        public async Task<CfnBankreceiptDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBankreceipt>().FindAsync(id);
            return _mapper.Map<CfnBankreceiptDto?>(entity);
        }

        public async Task<CfnBankreceiptDto> CreateAsync(CfnBankreceiptDto dto)
        {
            var entity = _mapper.Map<CfnBankreceipt>(dto);
            _context.Set<CfnBankreceipt>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankreceiptDto>(entity);
        }

        public async Task<CfnBankreceiptDto> UpdateAsync(CfnBankreceiptDto dto)
        {
            var entity = await _context.Set<CfnBankreceipt>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBankreceipt not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankreceiptDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBankreceipt>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
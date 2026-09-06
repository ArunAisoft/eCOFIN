using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankpaymentService : ICfnBankpaymentService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankpaymentService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankpaymentDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBankpayment>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankpaymentDto>>(entities);
        }

        public async Task<CfnBankpaymentDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBankpayment>().FindAsync(id);
            return _mapper.Map<CfnBankpaymentDto?>(entity);
        }

        public async Task<CfnBankpaymentDto> CreateAsync(CfnBankpaymentDto dto)
        {
            var entity = _mapper.Map<CfnBankpayment>(dto);
            _context.Set<CfnBankpayment>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankpaymentDto>(entity);
        }

        public async Task<CfnBankpaymentDto> UpdateAsync(CfnBankpaymentDto dto)
        {
            var entity = await _context.Set<CfnBankpayment>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBankpayment not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankpaymentDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBankpayment>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCashpaymentService : ICfnCashpaymentService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCashpaymentService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCashpaymentDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCashpayment>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCashpaymentDto>>(entities);
        }

        public async Task<CfnCashpaymentDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCashpayment>().FindAsync(id);
            return _mapper.Map<CfnCashpaymentDto?>(entity);
        }

        public async Task<CfnCashpaymentDto> CreateAsync(CfnCashpaymentDto dto)
        {
            var entity = _mapper.Map<CfnCashpayment>(dto);
            _context.Set<CfnCashpayment>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCashpaymentDto>(entity);
        }

        public async Task<CfnCashpaymentDto> UpdateAsync(CfnCashpaymentDto dto)
        {
            var entity = await _context.Set<CfnCashpayment>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnCashpayment not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCashpaymentDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCashpayment>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
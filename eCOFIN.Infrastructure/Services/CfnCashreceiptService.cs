using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCashreceiptService : ICfnCashreceiptService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCashreceiptService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCashreceiptDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCashreceipt>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCashreceiptDto>>(entities);
        }

        public async Task<CfnCashreceiptDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCashreceipt>().FindAsync(id);
            return _mapper.Map<CfnCashreceiptDto?>(entity);
        }

        public async Task<CfnCashreceiptDto> CreateAsync(CfnCashreceiptDto dto)
        {
            var entity = _mapper.Map<CfnCashreceipt>(dto);
            _context.Set<CfnCashreceipt>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCashreceiptDto>(entity);
        }

        public async Task<CfnCashreceiptDto> UpdateAsync(CfnCashreceiptDto dto)
        {
            var entity = await _context.Set<CfnCashreceipt>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnCashreceipt not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCashreceiptDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCashreceipt>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvreceiptService : ICfnInvreceiptService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvreceiptService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvreceiptDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvreceipt>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvreceiptDto>>(entities);
        }

        public async Task<CfnInvreceiptDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInvreceipt>().FindAsync(id);
            return _mapper.Map<CfnInvreceiptDto?>(entity);
        }

        public async Task<CfnInvreceiptDto> CreateAsync(CfnInvreceiptDto dto)
        {
            var entity = _mapper.Map<CfnInvreceipt>(dto);
            _context.Set<CfnInvreceipt>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvreceiptDto>(entity);
        }

        public async Task<CfnInvreceiptDto> UpdateAsync(CfnInvreceiptDto dto)
        {
            var entity = await _context.Set<CfnInvreceipt>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnInvreceipt not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvreceiptDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInvreceipt>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
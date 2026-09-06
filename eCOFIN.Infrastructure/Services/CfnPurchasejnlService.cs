using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPurchasejnlService : ICfnPurchasejnlService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPurchasejnlService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPurchasejnlDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPurchasejnl>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPurchasejnlDto>>(entities);
        }

        public async Task<CfnPurchasejnlDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPurchasejnl>().FindAsync(id);
            return _mapper.Map<CfnPurchasejnlDto?>(entity);
        }

        public async Task<CfnPurchasejnlDto> CreateAsync(CfnPurchasejnlDto dto)
        {
            var entity = _mapper.Map<CfnPurchasejnl>(dto);
            _context.Set<CfnPurchasejnl>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPurchasejnlDto>(entity);
        }

        public async Task<CfnPurchasejnlDto> UpdateAsync(CfnPurchasejnlDto dto)
        {
            var entity = await _context.Set<CfnPurchasejnl>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnPurchasejnl not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPurchasejnlDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPurchasejnl>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
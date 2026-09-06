using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnSalevoucherService : ICfnSalevoucherService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnSalevoucherService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnSalevoucherDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnSalevoucher>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnSalevoucherDto>>(entities);
        }

        public async Task<CfnSalevoucherDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnSalevoucher>().FindAsync(id);
            return _mapper.Map<CfnSalevoucherDto?>(entity);
        }

        public async Task<CfnSalevoucherDto> CreateAsync(CfnSalevoucherDto dto)
        {
            var entity = _mapper.Map<CfnSalevoucher>(dto);
            _context.Set<CfnSalevoucher>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSalevoucherDto>(entity);
        }

        public async Task<CfnSalevoucherDto> UpdateAsync(CfnSalevoucherDto dto)
        {
            var entity = await _context.Set<CfnSalevoucher>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnSalevoucher not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSalevoucherDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnSalevoucher>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
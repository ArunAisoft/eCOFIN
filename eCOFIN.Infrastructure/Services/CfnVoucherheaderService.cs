using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnVoucherheaderService : ICfnVoucherheaderService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnVoucherheaderService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnVoucherheaderDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnVoucherheader>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnVoucherheaderDto>>(entities);
        }

        public async Task<CfnVoucherheaderDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnVoucherheader>().FindAsync(id);
            return _mapper.Map<CfnVoucherheaderDto?>(entity);
        }

        public async Task<CfnVoucherheaderDto> CreateAsync(CfnVoucherheaderDto dto)
        {
            var entity = _mapper.Map<CfnVoucherheader>(dto);
            _context.Set<CfnVoucherheader>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVoucherheaderDto>(entity);
        }

        public async Task<CfnVoucherheaderDto> UpdateAsync(CfnVoucherheaderDto dto)
        {
            var entity = await _context.Set<CfnVoucherheader>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnVoucherheader not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVoucherheaderDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnVoucherheader>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
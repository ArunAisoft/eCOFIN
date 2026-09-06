using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvoiceService : ICfnInvoiceService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvoiceService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvoiceDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvoice>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvoiceDto>>(entities);
        }

        public async Task<CfnInvoiceDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInvoice>().FindAsync(id);
            return _mapper.Map<CfnInvoiceDto?>(entity);
        }

        public async Task<CfnInvoiceDto> CreateAsync(CfnInvoiceDto dto)
        {
            var entity = _mapper.Map<CfnInvoice>(dto);
            _context.Set<CfnInvoice>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvoiceDto>(entity);
        }

        public async Task<CfnInvoiceDto> UpdateAsync(CfnInvoiceDto dto)
        {
            var entity = await _context.Set<CfnInvoice>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnInvoice not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvoiceDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInvoice>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
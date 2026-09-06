using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class AccPaymentReceiptService : IAccPaymentReceiptService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public AccPaymentReceiptService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccPaymentReceiptDto>> GetAllAsync()
        {
            var entities = await _context.Set<AccPaymentReceipt>().ToListAsync();
            return _mapper.Map<IEnumerable<AccPaymentReceiptDto>>(entities);
        }

        public async Task<AccPaymentReceiptDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<AccPaymentReceipt>().FindAsync(id);
            return _mapper.Map<AccPaymentReceiptDto?>(entity);
        }

        public async Task<AccPaymentReceiptDto> CreateAsync(AccPaymentReceiptDto dto)
        {
            var entity = _mapper.Map<AccPaymentReceipt>(dto);
            _context.Set<AccPaymentReceipt>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccPaymentReceiptDto>(entity);
        }

        public async Task<AccPaymentReceiptDto> UpdateAsync(AccPaymentReceiptDto dto)
        {
            var entity = await _context.Set<AccPaymentReceipt>().FindAsync(dto.PayReceiptId);
            if (entity == null) throw new KeyNotFoundException("AccPaymentReceipt not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccPaymentReceiptDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<AccPaymentReceipt>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
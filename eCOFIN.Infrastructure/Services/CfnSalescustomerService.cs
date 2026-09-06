using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnSalescustomerService : ICfnSalescustomerService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnSalescustomerService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnSalescustomerDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnSalescustomer>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnSalescustomerDto>>(entities);
        }

        public async Task<CfnSalescustomerDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnSalescustomer>().FindAsync(id);
            return _mapper.Map<CfnSalescustomerDto?>(entity);
        }

        public async Task<CfnSalescustomerDto> CreateAsync(CfnSalescustomerDto dto)
        {
            var entity = _mapper.Map<CfnSalescustomer>(dto);
            _context.Set<CfnSalescustomer>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSalescustomerDto>(entity);
        }

        public async Task<CfnSalescustomerDto> UpdateAsync(CfnSalescustomerDto dto)
        {
            var entity = await _context.Set<CfnSalescustomer>().FindAsync(dto.Salescustomercode);
            if (entity == null) throw new KeyNotFoundException("CfnSalescustomer not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSalescustomerDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnSalescustomer>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
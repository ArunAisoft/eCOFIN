using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCustomerService : ICfnCustomerService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCustomerService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCustomerDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCustomer>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCustomerDto>>(entities);
        }

        public async Task<CfnCustomerDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCustomer>().FindAsync(id);
            return _mapper.Map<CfnCustomerDto?>(entity);
        }

        public async Task<CfnCustomerDto> CreateAsync(CfnCustomerDto dto)
        {
            var entity = _mapper.Map<CfnCustomer>(dto);
            _context.Set<CfnCustomer>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCustomerDto>(entity);
        }

        public async Task<CfnCustomerDto> UpdateAsync(CfnCustomerDto dto)
        {
            var entity = await _context.Set<CfnCustomer>().FindAsync(dto.Customercode);
            if (entity == null) throw new KeyNotFoundException("CfnCustomer not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCustomerDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCustomer>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
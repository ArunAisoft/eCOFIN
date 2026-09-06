using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var entities = await _context.Set<Customer>().ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(entities);
        }

        public async Task<CustomerDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<Customer>().FindAsync(id);
            return _mapper.Map<CustomerDto?>(entity);
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);
            _context.Set<Customer>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto dto)
        {
            var entity = await _context.Set<Customer>().FindAsync(dto.Custcode);
            if (entity == null) throw new KeyNotFoundException("Customer not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<Customer>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class VendcodeService : IVendcodeService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public VendcodeService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendcodeDto>> GetAllAsync()
        {
            var entities = await _context.Set<Vendcode>().ToListAsync();
            return _mapper.Map<IEnumerable<VendcodeDto>>(entities);
        }

        public async Task<VendcodeDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<Vendcode>().FindAsync(id);
            return _mapper.Map<VendcodeDto?>(entity);
        }

        public async Task<VendcodeDto> CreateAsync(VendcodeDto dto)
        {
            var entity = _mapper.Map<Vendcode>(dto);
            _context.Set<Vendcode>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<VendcodeDto>(entity);
        }

        public async Task<VendcodeDto> UpdateAsync(VendcodeDto dto)
        {
            var entity = await _context.Set<Vendcode>().FindAsync(dto.Code);
            if (entity == null) throw new KeyNotFoundException("Vendcode not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<VendcodeDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<Vendcode>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
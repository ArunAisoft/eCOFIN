using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnWarehouseService : ICfnWarehouseService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnWarehouseService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnWarehouseDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnWarehouse>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnWarehouseDto>>(entities);
        }

        public async Task<CfnWarehouseDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnWarehouse>().FindAsync(id);
            return _mapper.Map<CfnWarehouseDto?>(entity);
        }

        public async Task<CfnWarehouseDto> CreateAsync(CfnWarehouseDto dto)
        {
            var entity = _mapper.Map<CfnWarehouse>(dto);
            _context.Set<CfnWarehouse>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnWarehouseDto>(entity);
        }

        public async Task<CfnWarehouseDto> UpdateAsync(CfnWarehouseDto dto)
        {
            var entity = await _context.Set<CfnWarehouse>().FindAsync(dto.Warehousecode);
            if (entity == null) throw new KeyNotFoundException("CfnWarehouse not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnWarehouseDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnWarehouse>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
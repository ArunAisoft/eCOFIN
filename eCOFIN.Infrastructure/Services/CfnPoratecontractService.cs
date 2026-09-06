using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPoratecontractService : ICfnPoratecontractService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPoratecontractService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPoratecontractDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPoratecontract>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPoratecontractDto>>(entities);
        }

        public async Task<CfnPoratecontractDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPoratecontract>().FindAsync(id);
            return _mapper.Map<CfnPoratecontractDto?>(entity);
        }

        public async Task<CfnPoratecontractDto> CreateAsync(CfnPoratecontractDto dto)
        {
            var entity = _mapper.Map<CfnPoratecontract>(dto);
            _context.Set<CfnPoratecontract>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPoratecontractDto>(entity);
        }

        public async Task<CfnPoratecontractDto> UpdateAsync(CfnPoratecontractDto dto)
        {
            var entity = await _context.Set<CfnPoratecontract>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnPoratecontract not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPoratecontractDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPoratecontract>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
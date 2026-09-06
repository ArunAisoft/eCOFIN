using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBtlordoaService : ICfnBtlordoaService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBtlordoaService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBtlordoaDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBtlordoa>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBtlordoaDto>>(entities);
        }

        public async Task<CfnBtlordoaDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBtlordoa>().FindAsync(id);
            return _mapper.Map<CfnBtlordoaDto?>(entity);
        }

        public async Task<CfnBtlordoaDto> CreateAsync(CfnBtlordoaDto dto)
        {
            var entity = _mapper.Map<CfnBtlordoa>(dto);
            _context.Set<CfnBtlordoa>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBtlordoaDto>(entity);
        }

        public async Task<CfnBtlordoaDto> UpdateAsync(CfnBtlordoaDto dto)
        {
            var entity = await _context.Set<CfnBtlordoa>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBtlordoa not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBtlordoaDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBtlordoa>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
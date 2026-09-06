using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBtlcustomerorderService : ICfnBtlcustomerorderService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBtlcustomerorderService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBtlcustomerorderDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBtlcustomerorder>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBtlcustomerorderDto>>(entities);
        }

        public async Task<CfnBtlcustomerorderDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBtlcustomerorder>().FindAsync(id);
            return _mapper.Map<CfnBtlcustomerorderDto?>(entity);
        }

        public async Task<CfnBtlcustomerorderDto> CreateAsync(CfnBtlcustomerorderDto dto)
        {
            var entity = _mapper.Map<CfnBtlcustomerorder>(dto);
            _context.Set<CfnBtlcustomerorder>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBtlcustomerorderDto>(entity);
        }

        public async Task<CfnBtlcustomerorderDto> UpdateAsync(CfnBtlcustomerorderDto dto)
        {
            var entity = await _context.Set<CfnBtlcustomerorder>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBtlcustomerorder not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBtlcustomerorderDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBtlcustomerorder>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
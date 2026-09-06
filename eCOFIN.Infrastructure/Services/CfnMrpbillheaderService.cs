using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnMrpbillheaderService : ICfnMrpbillheaderService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnMrpbillheaderService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnMrpbillheaderDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnMrpbillheader>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnMrpbillheaderDto>>(entities);
        }

        public async Task<CfnMrpbillheaderDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnMrpbillheader>().FindAsync(id);
            return _mapper.Map<CfnMrpbillheaderDto?>(entity);
        }

        public async Task<CfnMrpbillheaderDto> CreateAsync(CfnMrpbillheaderDto dto)
        {
            var entity = _mapper.Map<CfnMrpbillheader>(dto);
            _context.Set<CfnMrpbillheader>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnMrpbillheaderDto>(entity);
        }

        public async Task<CfnMrpbillheaderDto> UpdateAsync(CfnMrpbillheaderDto dto)
        {
            var entity = await _context.Set<CfnMrpbillheader>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnMrpbillheader not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnMrpbillheaderDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnMrpbillheader>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
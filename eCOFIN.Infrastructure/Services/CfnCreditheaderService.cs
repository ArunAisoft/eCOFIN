using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCreditheaderService : ICfnCreditheaderService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCreditheaderService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCreditheaderDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCreditheader>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCreditheaderDto>>(entities);
        }

        public async Task<CfnCreditheaderDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCreditheader>().FindAsync(id);
            return _mapper.Map<CfnCreditheaderDto?>(entity);
        }

        public async Task<CfnCreditheaderDto> CreateAsync(CfnCreditheaderDto dto)
        {
            var entity = _mapper.Map<CfnCreditheader>(dto);
            _context.Set<CfnCreditheader>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCreditheaderDto>(entity);
        }

        public async Task<CfnCreditheaderDto> UpdateAsync(CfnCreditheaderDto dto)
        {
            var entity = await _context.Set<CfnCreditheader>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnCreditheader not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCreditheaderDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCreditheader>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
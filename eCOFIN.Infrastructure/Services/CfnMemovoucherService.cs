using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnMemovoucherService : ICfnMemovoucherService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnMemovoucherService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnMemovoucherDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnMemovoucher>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnMemovoucherDto>>(entities);
        }

        public async Task<CfnMemovoucherDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnMemovoucher>().FindAsync(id);
            return _mapper.Map<CfnMemovoucherDto?>(entity);
        }

        public async Task<CfnMemovoucherDto> CreateAsync(CfnMemovoucherDto dto)
        {
            var entity = _mapper.Map<CfnMemovoucher>(dto);
            _context.Set<CfnMemovoucher>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnMemovoucherDto>(entity);
        }

        public async Task<CfnMemovoucherDto> UpdateAsync(CfnMemovoucherDto dto)
        {
            var entity = await _context.Set<CfnMemovoucher>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnMemovoucher not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnMemovoucherDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnMemovoucher>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
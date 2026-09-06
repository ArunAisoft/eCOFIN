using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTravelvoucherService : ICfnTravelvoucherService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTravelvoucherService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTravelvoucherDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTravelvoucher>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTravelvoucherDto>>(entities);
        }

        public async Task<CfnTravelvoucherDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTravelvoucher>().FindAsync(id);
            return _mapper.Map<CfnTravelvoucherDto?>(entity);
        }

        public async Task<CfnTravelvoucherDto> CreateAsync(CfnTravelvoucherDto dto)
        {
            var entity = _mapper.Map<CfnTravelvoucher>(dto);
            _context.Set<CfnTravelvoucher>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTravelvoucherDto>(entity);
        }

        public async Task<CfnTravelvoucherDto> UpdateAsync(CfnTravelvoucherDto dto)
        {
            var entity = await _context.Set<CfnTravelvoucher>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnTravelvoucher not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTravelvoucherDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTravelvoucher>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBillpassinghdrService : ICfnBillpassinghdrService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBillpassinghdrService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBillpassinghdrDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBillpassinghdr>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBillpassinghdrDto>>(entities);
        }

        public async Task<CfnBillpassinghdrDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBillpassinghdr>().FindAsync(id);
            return _mapper.Map<CfnBillpassinghdrDto?>(entity);
        }

        public async Task<CfnBillpassinghdrDto> CreateAsync(CfnBillpassinghdrDto dto)
        {
            var entity = _mapper.Map<CfnBillpassinghdr>(dto);
            _context.Set<CfnBillpassinghdr>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBillpassinghdrDto>(entity);
        }

        public async Task<CfnBillpassinghdrDto> UpdateAsync(CfnBillpassinghdrDto dto)
        {
            var entity = await _context.Set<CfnBillpassinghdr>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBillpassinghdr not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBillpassinghdrDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBillpassinghdr>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
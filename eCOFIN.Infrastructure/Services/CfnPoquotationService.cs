using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPoquotationService : ICfnPoquotationService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPoquotationService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPoquotationDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPoquotation>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPoquotationDto>>(entities);
        }

        public async Task<CfnPoquotationDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPoquotation>().FindAsync(id);
            return _mapper.Map<CfnPoquotationDto?>(entity);
        }

        public async Task<CfnPoquotationDto> CreateAsync(CfnPoquotationDto dto)
        {
            var entity = _mapper.Map<CfnPoquotation>(dto);
            _context.Set<CfnPoquotation>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPoquotationDto>(entity);
        }

        public async Task<CfnPoquotationDto> UpdateAsync(CfnPoquotationDto dto)
        {
            var entity = await _context.Set<CfnPoquotation>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnPoquotation not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPoquotationDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPoquotation>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
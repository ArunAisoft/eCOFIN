using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnQuotationService : ICfnQuotationService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnQuotationService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnQuotationDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnQuotation>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnQuotationDto>>(entities);
        }

        public async Task<CfnQuotationDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnQuotation>().FindAsync(id);
            return _mapper.Map<CfnQuotationDto?>(entity);
        }

        public async Task<CfnQuotationDto> CreateAsync(CfnQuotationDto dto)
        {
            var entity = _mapper.Map<CfnQuotation>(dto);
            _context.Set<CfnQuotation>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnQuotationDto>(entity);
        }

        public async Task<CfnQuotationDto> UpdateAsync(CfnQuotationDto dto)
        {
            var entity = await _context.Set<CfnQuotation>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnQuotation not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnQuotationDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnQuotation>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
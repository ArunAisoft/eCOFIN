using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPoenquiryService : ICfnPoenquiryService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPoenquiryService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPoenquiryDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPoenquiry>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPoenquiryDto>>(entities);
        }

        public async Task<CfnPoenquiryDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPoenquiry>().FindAsync(id);
            return _mapper.Map<CfnPoenquiryDto?>(entity);
        }

        public async Task<CfnPoenquiryDto> CreateAsync(CfnPoenquiryDto dto)
        {
            var entity = _mapper.Map<CfnPoenquiry>(dto);
            _context.Set<CfnPoenquiry>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPoenquiryDto>(entity);
        }

        public async Task<CfnPoenquiryDto> UpdateAsync(CfnPoenquiryDto dto)
        {
            var entity = await _context.Set<CfnPoenquiry>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnPoenquiry not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPoenquiryDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPoenquiry>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
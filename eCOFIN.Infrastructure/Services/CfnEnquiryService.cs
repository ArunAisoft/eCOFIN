using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnEnquiryService : ICfnEnquiryService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnEnquiryService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnEnquiryDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnEnquiry>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnEnquiryDto>>(entities);
        }

        public async Task<CfnEnquiryDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnEnquiry>().FindAsync(id);
            return _mapper.Map<CfnEnquiryDto?>(entity);
        }

        public async Task<CfnEnquiryDto> CreateAsync(CfnEnquiryDto dto)
        {
            var entity = _mapper.Map<CfnEnquiry>(dto);
            _context.Set<CfnEnquiry>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnEnquiryDto>(entity);
        }

        public async Task<CfnEnquiryDto> UpdateAsync(CfnEnquiryDto dto)
        {
            var entity = await _context.Set<CfnEnquiry>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnEnquiry not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnEnquiryDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnEnquiry>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
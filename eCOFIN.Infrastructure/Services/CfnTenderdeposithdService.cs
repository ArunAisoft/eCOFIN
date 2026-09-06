using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTenderdeposithdService : ICfnTenderdeposithdService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTenderdeposithdService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTenderdeposithdDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTenderdeposithd>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTenderdeposithdDto>>(entities);
        }

        public async Task<CfnTenderdeposithdDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTenderdeposithd>().FindAsync(id);
            return _mapper.Map<CfnTenderdeposithdDto?>(entity);
        }

        public async Task<CfnTenderdeposithdDto> CreateAsync(CfnTenderdeposithdDto dto)
        {
            var entity = _mapper.Map<CfnTenderdeposithd>(dto);
            _context.Set<CfnTenderdeposithd>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTenderdeposithdDto>(entity);
        }

        public async Task<CfnTenderdeposithdDto> UpdateAsync(CfnTenderdeposithdDto dto)
        {
            var entity = await _context.Set<CfnTenderdeposithd>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnTenderdeposithd not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTenderdeposithdDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTenderdeposithd>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
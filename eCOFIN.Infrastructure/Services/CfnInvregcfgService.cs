using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvregcfgService : ICfnInvregcfgService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvregcfgService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvregcfgDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvregcfg>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvregcfgDto>>(entities);
        }

        public async Task<CfnInvregcfgDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInvregcfg>().FindAsync(id);
            return _mapper.Map<CfnInvregcfgDto?>(entity);
        }

        public async Task<CfnInvregcfgDto> CreateAsync(CfnInvregcfgDto dto)
        {
            var entity = _mapper.Map<CfnInvregcfg>(dto);
            _context.Set<CfnInvregcfg>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvregcfgDto>(entity);
        }

        public async Task<CfnInvregcfgDto> UpdateAsync(CfnInvregcfgDto dto)
        {
            var entity = await _context.Set<CfnInvregcfg>().FindAsync(dto.Pricingfactorcode);
            if (entity == null) throw new KeyNotFoundException("CfnInvregcfg not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvregcfgDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInvregcfg>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
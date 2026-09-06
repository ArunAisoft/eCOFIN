using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvcprncfgService : ICfnInvcprncfgService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvcprncfgService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvcprncfgDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvcprncfg>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvcprncfgDto>>(entities);
        }

        public async Task<CfnInvcprncfgDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInvcprncfg>().FindAsync(id);
            return _mapper.Map<CfnInvcprncfgDto?>(entity);
        }

        public async Task<CfnInvcprncfgDto> CreateAsync(CfnInvcprncfgDto dto)
        {
            var entity = _mapper.Map<CfnInvcprncfg>(dto);
            _context.Set<CfnInvcprncfg>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvcprncfgDto>(entity);
        }

        public async Task<CfnInvcprncfgDto> UpdateAsync(CfnInvcprncfgDto dto)
        {
            var entity = await _context.Set<CfnInvcprncfg>().FindAsync(dto.Pricingfactorcode);
            if (entity == null) throw new KeyNotFoundException("CfnInvcprncfg not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvcprncfgDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInvcprncfg>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
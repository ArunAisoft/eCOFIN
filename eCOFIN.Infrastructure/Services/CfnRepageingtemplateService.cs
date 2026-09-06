using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnRepageingtemplateService : ICfnRepageingtemplateService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnRepageingtemplateService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnRepageingtemplateDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnRepageingtemplate>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnRepageingtemplateDto>>(entities);
        }

        public async Task<CfnRepageingtemplateDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnRepageingtemplate>().FindAsync(id);
            return _mapper.Map<CfnRepageingtemplateDto?>(entity);
        }

        public async Task<CfnRepageingtemplateDto> CreateAsync(CfnRepageingtemplateDto dto)
        {
            var entity = _mapper.Map<CfnRepageingtemplate>(dto);
            _context.Set<CfnRepageingtemplate>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRepageingtemplateDto>(entity);
        }

        public async Task<CfnRepageingtemplateDto> UpdateAsync(CfnRepageingtemplateDto dto)
        {
            var entity = await _context.Set<CfnRepageingtemplate>().FindAsync(dto.Srrecno);
            if (entity == null) throw new KeyNotFoundException("CfnRepageingtemplate not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRepageingtemplateDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnRepageingtemplate>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
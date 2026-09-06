using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnRectemplateService : ICfnRectemplateService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnRectemplateService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnRectemplateDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnRectemplate>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnRectemplateDto>>(entities);
        }

        public async Task<CfnRectemplateDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnRectemplate>().FindAsync(id);
            return _mapper.Map<CfnRectemplateDto?>(entity);
        }

        public async Task<CfnRectemplateDto> CreateAsync(CfnRectemplateDto dto)
        {
            var entity = _mapper.Map<CfnRectemplate>(dto);
            _context.Set<CfnRectemplate>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRectemplateDto>(entity);
        }

        public async Task<CfnRectemplateDto> UpdateAsync(CfnRectemplateDto dto)
        {
            var entity = await _context.Set<CfnRectemplate>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnRectemplate not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRectemplateDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnRectemplate>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
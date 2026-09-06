using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnRecurringvchrService : ICfnRecurringvchrService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnRecurringvchrService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnRecurringvchrDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnRecurringvchr>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnRecurringvchrDto>>(entities);
        }

        public async Task<CfnRecurringvchrDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnRecurringvchr>().FindAsync(id);
            return _mapper.Map<CfnRecurringvchrDto?>(entity);
        }

        public async Task<CfnRecurringvchrDto> CreateAsync(CfnRecurringvchrDto dto)
        {
            var entity = _mapper.Map<CfnRecurringvchr>(dto);
            _context.Set<CfnRecurringvchr>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRecurringvchrDto>(entity);
        }

        public async Task<CfnRecurringvchrDto> UpdateAsync(CfnRecurringvchrDto dto)
        {
            var entity = await _context.Set<CfnRecurringvchr>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnRecurringvchr not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRecurringvchrDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnRecurringvchr>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTendercompetitorService : ICfnTendercompetitorService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTendercompetitorService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTendercompetitorDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTendercompetitor>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTendercompetitorDto>>(entities);
        }

        public async Task<CfnTendercompetitorDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTendercompetitor>().FindAsync(id);
            return _mapper.Map<CfnTendercompetitorDto?>(entity);
        }

        public async Task<CfnTendercompetitorDto> CreateAsync(CfnTendercompetitorDto dto)
        {
            var entity = _mapper.Map<CfnTendercompetitor>(dto);
            _context.Set<CfnTendercompetitor>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTendercompetitorDto>(entity);
        }

        public async Task<CfnTendercompetitorDto> UpdateAsync(CfnTendercompetitorDto dto)
        {
            var entity = await _context.Set<CfnTendercompetitor>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnTendercompetitor not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTendercompetitorDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTendercompetitor>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
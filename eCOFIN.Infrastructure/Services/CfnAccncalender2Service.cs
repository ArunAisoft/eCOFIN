using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAccncalender2Service : ICfnAccncalender2Service
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAccncalender2Service(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAccncalender2Dto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAccncalender2>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAccncalender2Dto>>(entities);
        }

        public async Task<CfnAccncalender2Dto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnAccncalender2>().FindAsync(id);
            return _mapper.Map<CfnAccncalender2Dto?>(entity);
        }

        public async Task<CfnAccncalender2Dto> CreateAsync(CfnAccncalender2Dto dto)
        {
            var entity = _mapper.Map<CfnAccncalender2>(dto);
            _context.Set<CfnAccncalender2>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAccncalender2Dto>(entity);
        }

        public async Task<CfnAccncalender2Dto> UpdateAsync(CfnAccncalender2Dto dto)
        {
            var entity = await _context.Set<CfnAccncalender2>().FindAsync(dto.Accperiod);
            if (entity == null) throw new KeyNotFoundException("CfnAccncalender2 not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAccncalender2Dto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnAccncalender2>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
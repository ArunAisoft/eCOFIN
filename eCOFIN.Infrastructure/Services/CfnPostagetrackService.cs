using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPostagetrackService : ICfnPostagetrackService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPostagetrackService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPostagetrackDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPostagetrack>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPostagetrackDto>>(entities);
        }

        public async Task<CfnPostagetrackDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPostagetrack>().FindAsync(id);
            return _mapper.Map<CfnPostagetrackDto?>(entity);
        }

        public async Task<CfnPostagetrackDto> CreateAsync(CfnPostagetrackDto dto)
        {
            var entity = _mapper.Map<CfnPostagetrack>(dto);
            _context.Set<CfnPostagetrack>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPostagetrackDto>(entity);
        }

        public async Task<CfnPostagetrackDto> UpdateAsync(CfnPostagetrackDto dto)
        {
            var entity = await _context.Set<CfnPostagetrack>().FindAsync(dto.Ponumber);
            if (entity == null) throw new KeyNotFoundException("CfnPostagetrack not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPostagetrackDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPostagetrack>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
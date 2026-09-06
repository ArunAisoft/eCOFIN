using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnFyrefcontrolService : ICfnFyrefcontrolService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnFyrefcontrolService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnFyrefcontrolDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnFyrefcontrol>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnFyrefcontrolDto>>(entities);
        }

        public async Task<CfnFyrefcontrolDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnFyrefcontrol>().FindAsync(id);
            return _mapper.Map<CfnFyrefcontrolDto?>(entity);
        }

        public async Task<CfnFyrefcontrolDto> CreateAsync(CfnFyrefcontrolDto dto)
        {
            var entity = _mapper.Map<CfnFyrefcontrol>(dto);
            _context.Set<CfnFyrefcontrol>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnFyrefcontrolDto>(entity);
        }

        public async Task<CfnFyrefcontrolDto> UpdateAsync(CfnFyrefcontrolDto dto)
        {
            var entity = await _context.Set<CfnFyrefcontrol>().FindAsync(dto.Financialyear);
            if (entity == null) throw new KeyNotFoundException("CfnFyrefcontrol not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnFyrefcontrolDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnFyrefcontrol>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
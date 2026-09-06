using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvorderrelatedService : ICfnInvorderrelatedService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvorderrelatedService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvorderrelatedDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvorderrelated>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvorderrelatedDto>>(entities);
        }

        public async Task<CfnInvorderrelatedDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnInvorderrelated>().FindAsync(id);
            return _mapper.Map<CfnInvorderrelatedDto?>(entity);
        }

        public async Task<CfnInvorderrelatedDto> CreateAsync(CfnInvorderrelatedDto dto)
        {
            var entity = _mapper.Map<CfnInvorderrelated>(dto);
            _context.Set<CfnInvorderrelated>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvorderrelatedDto>(entity);
        }

        public async Task<CfnInvorderrelatedDto> UpdateAsync(CfnInvorderrelatedDto dto)
        {
            var entity = await _context.Set<CfnInvorderrelated>().FindAsync(dto.Serialno);
            if (entity == null) throw new KeyNotFoundException("CfnInvorderrelated not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvorderrelatedDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnInvorderrelated>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
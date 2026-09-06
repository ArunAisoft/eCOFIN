using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnUserService : ICfnUserService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnUserService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnUserDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnUser>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnUserDto>>(entities);
        }

        public async Task<CfnUserDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnUser>().FindAsync(id);
            return _mapper.Map<CfnUserDto?>(entity);
        }

        public async Task<CfnUserDto> CreateAsync(CfnUserDto dto)
        {
            var entity = _mapper.Map<CfnUser>(dto);
            _context.Set<CfnUser>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnUserDto>(entity);
        }

        public async Task<CfnUserDto> UpdateAsync(CfnUserDto dto)
        {
            var entity = await _context.Set<CfnUser>().FindAsync(dto.Username);
            if (entity == null) throw new KeyNotFoundException("CfnUser not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnUserDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnUser>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
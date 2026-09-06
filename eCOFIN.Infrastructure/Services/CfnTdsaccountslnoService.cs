using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTdsaccountslnoService : ICfnTdsaccountslnoService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTdsaccountslnoService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTdsaccountslnoDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTdsaccountslno>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTdsaccountslnoDto>>(entities);
        }

        public async Task<CfnTdsaccountslnoDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTdsaccountslno>().FindAsync(id);
            return _mapper.Map<CfnTdsaccountslnoDto?>(entity);
        }

        public async Task<CfnTdsaccountslnoDto> CreateAsync(CfnTdsaccountslnoDto dto)
        {
            var entity = _mapper.Map<CfnTdsaccountslno>(dto);
            _context.Set<CfnTdsaccountslno>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTdsaccountslnoDto>(entity);
        }

        public async Task<CfnTdsaccountslnoDto> UpdateAsync(CfnTdsaccountslnoDto dto)
        {
            var entity = await _context.Set<CfnTdsaccountslno>().FindAsync(dto.Accountcode);
            if (entity == null) throw new KeyNotFoundException("CfnTdsaccountslno not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTdsaccountslnoDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTdsaccountslno>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
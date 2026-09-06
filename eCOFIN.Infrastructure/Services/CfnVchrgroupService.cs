using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnVchrgroupService : ICfnVchrgroupService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnVchrgroupService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnVchrgroupDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnVchrgroup>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnVchrgroupDto>>(entities);
        }

        public async Task<CfnVchrgroupDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnVchrgroup>().FindAsync(id);
            return _mapper.Map<CfnVchrgroupDto?>(entity);
        }

        public async Task<CfnVchrgroupDto> CreateAsync(CfnVchrgroupDto dto)
        {
            var entity = _mapper.Map<CfnVchrgroup>(dto);
            _context.Set<CfnVchrgroup>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVchrgroupDto>(entity);
        }

        public async Task<CfnVchrgroupDto> UpdateAsync(CfnVchrgroupDto dto)
        {
            var entity = await _context.Set<CfnVchrgroup>().FindAsync(dto.Vouchergroup);
            if (entity == null) throw new KeyNotFoundException("CfnVchrgroup not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnVchrgroupDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnVchrgroup>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
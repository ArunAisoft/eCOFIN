using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBscsexportService : ICfnBscsexportService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBscsexportService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBscsexportDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBscsexport>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBscsexportDto>>(entities);
        }

        public async Task<CfnBscsexportDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBscsexport>().FindAsync(id);
            return _mapper.Map<CfnBscsexportDto?>(entity);
        }

        public async Task<CfnBscsexportDto> CreateAsync(CfnBscsexportDto dto)
        {
            var entity = _mapper.Map<CfnBscsexport>(dto);
            _context.Set<CfnBscsexport>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBscsexportDto>(entity);
        }

        public async Task<CfnBscsexportDto> UpdateAsync(CfnBscsexportDto dto)
        {
            var entity = await _context.Set<CfnBscsexport>().FindAsync(dto.Accountcode);
            if (entity == null) throw new KeyNotFoundException("CfnBscsexport not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBscsexportDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBscsexport>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInterbranchService : ICfnInterbranchService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInterbranchService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInterbranchDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInterbranch>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInterbranchDto>>(entities);
        }

        public async Task<CfnInterbranchDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInterbranch>().FindAsync(id);
            return _mapper.Map<CfnInterbranchDto?>(entity);
        }

        public async Task<CfnInterbranchDto> CreateAsync(CfnInterbranchDto dto)
        {
            var entity = _mapper.Map<CfnInterbranch>(dto);
            _context.Set<CfnInterbranch>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInterbranchDto>(entity);
        }

        public async Task<CfnInterbranchDto> UpdateAsync(CfnInterbranchDto dto)
        {
            var entity = await _context.Set<CfnInterbranch>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnInterbranch not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInterbranchDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInterbranch>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
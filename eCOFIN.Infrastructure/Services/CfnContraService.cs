using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnContraService : ICfnContraService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnContraService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnContraDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnContra>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnContraDto>>(entities);
        }

        public async Task<CfnContraDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnContra>().FindAsync(id);
            return _mapper.Map<CfnContraDto?>(entity);
        }

        public async Task<CfnContraDto> CreateAsync(CfnContraDto dto)
        {
            var entity = _mapper.Map<CfnContra>(dto);
            _context.Set<CfnContra>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnContraDto>(entity);
        }

        public async Task<CfnContraDto> UpdateAsync(CfnContraDto dto)
        {
            var entity = await _context.Set<CfnContra>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnContra not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnContraDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnContra>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
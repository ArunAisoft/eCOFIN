using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnPhystockvchService : ICfnPhystockvchService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnPhystockvchService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnPhystockvchDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnPhystockvch>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnPhystockvchDto>>(entities);
        }

        public async Task<CfnPhystockvchDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnPhystockvch>().FindAsync(id);
            return _mapper.Map<CfnPhystockvchDto?>(entity);
        }

        public async Task<CfnPhystockvchDto> CreateAsync(CfnPhystockvchDto dto)
        {
            var entity = _mapper.Map<CfnPhystockvch>(dto);
            _context.Set<CfnPhystockvch>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPhystockvchDto>(entity);
        }

        public async Task<CfnPhystockvchDto> UpdateAsync(CfnPhystockvchDto dto)
        {
            var entity = await _context.Set<CfnPhystockvch>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnPhystockvch not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnPhystockvchDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnPhystockvch>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
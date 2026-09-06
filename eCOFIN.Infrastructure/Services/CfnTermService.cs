using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTermService : ICfnTermService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTermService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTermDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTerm>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTermDto>>(entities);
        }

        public async Task<CfnTermDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTerm>().FindAsync(id);
            return _mapper.Map<CfnTermDto?>(entity);
        }

        public async Task<CfnTermDto> CreateAsync(CfnTermDto dto)
        {
            var entity = _mapper.Map<CfnTerm>(dto);
            _context.Set<CfnTerm>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTermDto>(entity);
        }

        public async Task<CfnTermDto> UpdateAsync(CfnTermDto dto)
        {
            var entity = await _context.Set<CfnTerm>().FindAsync(dto.Termcode);
            if (entity == null) throw new KeyNotFoundException("CfnTerm not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTermDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTerm>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
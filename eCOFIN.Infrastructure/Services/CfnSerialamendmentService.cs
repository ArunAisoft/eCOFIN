using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnSerialamendmentService : ICfnSerialamendmentService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnSerialamendmentService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnSerialamendmentDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnSerialamendment>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnSerialamendmentDto>>(entities);
        }

        public async Task<CfnSerialamendmentDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnSerialamendment>().FindAsync(id);
            return _mapper.Map<CfnSerialamendmentDto?>(entity);
        }

        public async Task<CfnSerialamendmentDto> CreateAsync(CfnSerialamendmentDto dto)
        {
            var entity = _mapper.Map<CfnSerialamendment>(dto);
            _context.Set<CfnSerialamendment>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSerialamendmentDto>(entity);
        }

        public async Task<CfnSerialamendmentDto> UpdateAsync(CfnSerialamendmentDto dto)
        {
            var entity = await _context.Set<CfnSerialamendment>().FindAsync(dto.Slno);
            if (entity == null) throw new KeyNotFoundException("CfnSerialamendment not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSerialamendmentDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnSerialamendment>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
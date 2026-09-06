using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class EinvMainService : IEinvMainService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public EinvMainService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EinvMainDto>> GetAllAsync()
        {
            var entities = await _context.Set<EinvMain>().ToListAsync();
            return _mapper.Map<IEnumerable<EinvMainDto>>(entities);
        }

        public async Task<EinvMainDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<EinvMain>().FindAsync(id);
            return _mapper.Map<EinvMainDto?>(entity);
        }

        public async Task<EinvMainDto> CreateAsync(EinvMainDto dto)
        {
            var entity = _mapper.Map<EinvMain>(dto);
            _context.Set<EinvMain>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EinvMainDto>(entity);
        }

        public async Task<EinvMainDto> UpdateAsync(EinvMainDto dto)
        {
            var entity = await _context.Set<EinvMain>().FindAsync(dto.Slno);
            if (entity == null) throw new KeyNotFoundException("EinvMain not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EinvMainDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<EinvMain>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
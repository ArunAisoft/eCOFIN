using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class InvMainService : IInvMainService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public InvMainService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvMainDto>> GetAllAsync()
        {
            var entities = await _context.Set<InvMain>().ToListAsync();
            return _mapper.Map<IEnumerable<InvMainDto>>(entities);
        }

        public async Task<InvMainDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<InvMain>().FindAsync(id);
            return _mapper.Map<InvMainDto?>(entity);
        }

        public async Task<InvMainDto> CreateAsync(InvMainDto dto)
        {
            var entity = _mapper.Map<InvMain>(dto);
            _context.Set<InvMain>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<InvMainDto>(entity);
        }

        public async Task<InvMainDto> UpdateAsync(InvMainDto dto)
        {
            var entity = await _context.Set<InvMain>().FindAsync(dto.Slno);
            if (entity == null) throw new KeyNotFoundException("InvMain not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<InvMainDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<InvMain>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnQuerytaskService : ICfnQuerytaskService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnQuerytaskService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnQuerytaskDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnQuerytask>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnQuerytaskDto>>(entities);
        }

        public async Task<CfnQuerytaskDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnQuerytask>().FindAsync(id);
            return _mapper.Map<CfnQuerytaskDto?>(entity);
        }

        public async Task<CfnQuerytaskDto> CreateAsync(CfnQuerytaskDto dto)
        {
            var entity = _mapper.Map<CfnQuerytask>(dto);
            _context.Set<CfnQuerytask>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnQuerytaskDto>(entity);
        }

        public async Task<CfnQuerytaskDto> UpdateAsync(CfnQuerytaskDto dto)
        {
            var entity = await _context.Set<CfnQuerytask>().FindAsync(dto.Taskfullname);
            if (entity == null) throw new KeyNotFoundException("CfnQuerytask not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnQuerytaskDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnQuerytask>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
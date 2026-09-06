using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTaskService : ICfnTaskService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTaskService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTaskDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTask>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTaskDto>>(entities);
        }

        public async Task<CfnTaskDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTask>().FindAsync(id);
            return _mapper.Map<CfnTaskDto?>(entity);
        }

        public async Task<CfnTaskDto> CreateAsync(CfnTaskDto dto)
        {
            var entity = _mapper.Map<CfnTask>(dto);
            _context.Set<CfnTask>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTaskDto>(entity);
        }

        public async Task<CfnTaskDto> UpdateAsync(CfnTaskDto dto)
        {
            var entity = await _context.Set<CfnTask>().FindAsync(dto.Taskid);
            if (entity == null) throw new KeyNotFoundException("CfnTask not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTaskDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTask>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
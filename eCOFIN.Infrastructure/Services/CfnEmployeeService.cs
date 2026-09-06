using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnEmployeeService : ICfnEmployeeService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnEmployeeService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnEmployeeDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnEmployee>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnEmployeeDto>>(entities);
        }

        public async Task<CfnEmployeeDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnEmployee>().FindAsync(id);
            return _mapper.Map<CfnEmployeeDto?>(entity);
        }

        public async Task<CfnEmployeeDto> CreateAsync(CfnEmployeeDto dto)
        {
            var entity = _mapper.Map<CfnEmployee>(dto);
            _context.Set<CfnEmployee>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnEmployeeDto>(entity);
        }

        public async Task<CfnEmployeeDto> UpdateAsync(CfnEmployeeDto dto)
        {
            var entity = await _context.Set<CfnEmployee>().FindAsync(dto.Employeecode);
            if (entity == null) throw new KeyNotFoundException("CfnEmployee not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnEmployeeDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnEmployee>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
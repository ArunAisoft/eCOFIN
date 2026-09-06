using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnDepartmentService : ICfnDepartmentService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnDepartmentService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnDepartmentDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnDepartment>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnDepartmentDto>>(entities);
        }

        public async Task<CfnDepartmentDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnDepartment>().FindAsync(id);
            return _mapper.Map<CfnDepartmentDto?>(entity);
        }

        public async Task<CfnDepartmentDto> CreateAsync(CfnDepartmentDto dto)
        {
            var entity = _mapper.Map<CfnDepartment>(dto);
            _context.Set<CfnDepartment>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnDepartmentDto>(entity);
        }

        public async Task<CfnDepartmentDto> UpdateAsync(CfnDepartmentDto dto)
        {
            var entity = await _context.Set<CfnDepartment>().FindAsync(dto.Departmentcode);
            if (entity == null) throw new KeyNotFoundException("CfnDepartment not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnDepartmentDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnDepartment>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
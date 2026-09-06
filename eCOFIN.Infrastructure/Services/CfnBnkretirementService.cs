using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBnkretirementService : ICfnBnkretirementService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBnkretirementService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBnkretirementDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBnkretirement>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBnkretirementDto>>(entities);
        }

        public async Task<CfnBnkretirementDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBnkretirement>().FindAsync(id);
            return _mapper.Map<CfnBnkretirementDto?>(entity);
        }

        public async Task<CfnBnkretirementDto> CreateAsync(CfnBnkretirementDto dto)
        {
            var entity = _mapper.Map<CfnBnkretirement>(dto);
            _context.Set<CfnBnkretirement>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBnkretirementDto>(entity);
        }

        public async Task<CfnBnkretirementDto> UpdateAsync(CfnBnkretirementDto dto)
        {
            var entity = await _context.Set<CfnBnkretirement>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBnkretirement not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBnkretirementDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBnkretirement>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
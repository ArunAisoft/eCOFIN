using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCfparameterService : ICfnCfparameterService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCfparameterService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCfparameterDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCfparameter>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCfparameterDto>>(entities);
        }

        public async Task<CfnCfparameterDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCfparameter>().FindAsync(id);
            return _mapper.Map<CfnCfparameterDto?>(entity);
        }

        public async Task<CfnCfparameterDto> CreateAsync(CfnCfparameterDto dto)
        {
            var entity = _mapper.Map<CfnCfparameter>(dto);
            _context.Set<CfnCfparameter>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCfparameterDto>(entity);
        }

        public async Task<CfnCfparameterDto> UpdateAsync(CfnCfparameterDto dto)
        {
            var entity = await _context.Set<CfnCfparameter>().FindAsync(dto.Parametergroup);
            if (entity == null) throw new KeyNotFoundException("CfnCfparameter not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCfparameterDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCfparameter>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
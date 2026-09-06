using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCompanyService : ICfnCompanyService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCompanyService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCompanyDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCompany>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCompanyDto>>(entities);
        }

        public async Task<CfnCompanyDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCompany>().FindAsync(id);
            return _mapper.Map<CfnCompanyDto?>(entity);
        }

        public async Task<CfnCompanyDto> CreateAsync(CfnCompanyDto dto)
        {
            var entity = _mapper.Map<CfnCompany>(dto);
            _context.Set<CfnCompany>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCompanyDto>(entity);
        }

        public async Task<CfnCompanyDto> UpdateAsync(CfnCompanyDto dto)
        {
            var entity = await _context.Set<CfnCompany>().FindAsync(dto.Companycode);
            if (entity == null) throw new KeyNotFoundException("CfnCompany not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCompanyDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCompany>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
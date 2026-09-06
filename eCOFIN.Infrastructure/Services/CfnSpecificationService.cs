using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnSpecificationService : ICfnSpecificationService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnSpecificationService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnSpecificationDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnSpecification>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnSpecificationDto>>(entities);
        }

        public async Task<CfnSpecificationDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnSpecification>().FindAsync(id);
            return _mapper.Map<CfnSpecificationDto?>(entity);
        }

        public async Task<CfnSpecificationDto> CreateAsync(CfnSpecificationDto dto)
        {
            var entity = _mapper.Map<CfnSpecification>(dto);
            _context.Set<CfnSpecification>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSpecificationDto>(entity);
        }

        public async Task<CfnSpecificationDto> UpdateAsync(CfnSpecificationDto dto)
        {
            var entity = await _context.Set<CfnSpecification>().FindAsync(dto.Specificationcode);
            if (entity == null) throw new KeyNotFoundException("CfnSpecification not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSpecificationDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnSpecification>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
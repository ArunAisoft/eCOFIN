using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class AccVatTypeMasterService : IAccVatTypeMasterService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public AccVatTypeMasterService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccVatTypeMasterDto>> GetAllAsync()
        {
            var entities = await _context.Set<AccVatTypeMaster>().ToListAsync();
            return _mapper.Map<IEnumerable<AccVatTypeMasterDto>>(entities);
        }

        public async Task<AccVatTypeMasterDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<AccVatTypeMaster>().FindAsync(id);
            return _mapper.Map<AccVatTypeMasterDto?>(entity);
        }

        public async Task<AccVatTypeMasterDto> CreateAsync(AccVatTypeMasterDto dto)
        {
            var entity = _mapper.Map<AccVatTypeMaster>(dto);
            _context.Set<AccVatTypeMaster>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccVatTypeMasterDto>(entity);
        }

        public async Task<AccVatTypeMasterDto> UpdateAsync(AccVatTypeMasterDto dto)
        {
            var entity = await _context.Set<AccVatTypeMaster>().FindAsync(dto.SlNo);
            if (entity == null) throw new KeyNotFoundException("AccVatTypeMaster not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccVatTypeMasterDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<AccVatTypeMaster>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
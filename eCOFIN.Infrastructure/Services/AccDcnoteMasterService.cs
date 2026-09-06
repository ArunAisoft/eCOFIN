using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class AccDcnoteMasterService : IAccDcnoteMasterService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public AccDcnoteMasterService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccDcnoteMasterDto>> GetAllAsync()
        {
            var entities = await _context.Set<AccDcnoteMaster>().ToListAsync();
            return _mapper.Map<IEnumerable<AccDcnoteMasterDto>>(entities);
        }

        public async Task<AccDcnoteMasterDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<AccDcnoteMaster>().FindAsync(id);
            return _mapper.Map<AccDcnoteMasterDto?>(entity);
        }

        public async Task<AccDcnoteMasterDto> CreateAsync(AccDcnoteMasterDto dto)
        {
            var entity = _mapper.Map<AccDcnoteMaster>(dto);
            _context.Set<AccDcnoteMaster>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccDcnoteMasterDto>(entity);
        }

        public async Task<AccDcnoteMasterDto> UpdateAsync(AccDcnoteMasterDto dto)
        {
            var entity = await _context.Set<AccDcnoteMaster>().FindAsync(dto.DcnoteNo);
            if (entity == null) throw new KeyNotFoundException("AccDcnoteMaster not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccDcnoteMasterDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<AccDcnoteMaster>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
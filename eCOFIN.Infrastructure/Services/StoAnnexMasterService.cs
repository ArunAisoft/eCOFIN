using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class StoAnnexMasterService : IStoAnnexMasterService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public StoAnnexMasterService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoAnnexMasterDto>> GetAllAsync()
        {
            var entities = await _context.Set<StoAnnexMaster>().ToListAsync();
            return _mapper.Map<IEnumerable<StoAnnexMasterDto>>(entities);
        }

        public async Task<StoAnnexMasterDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<StoAnnexMaster>().FindAsync(id);
            return _mapper.Map<StoAnnexMasterDto?>(entity);
        }

        public async Task<StoAnnexMasterDto> CreateAsync(StoAnnexMasterDto dto)
        {
            var entity = _mapper.Map<StoAnnexMaster>(dto);
            _context.Set<StoAnnexMaster>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<StoAnnexMasterDto>(entity);
        }

        public async Task<StoAnnexMasterDto> UpdateAsync(StoAnnexMasterDto dto)
        {
            var entity = await _context.Set<StoAnnexMaster>().FindAsync(dto.AnnNo);
            if (entity == null) throw new KeyNotFoundException("StoAnnexMaster not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<StoAnnexMasterDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<StoAnnexMaster>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
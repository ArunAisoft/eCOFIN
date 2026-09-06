using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankdatumService : ICfnBankdatumService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankdatumService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankdatumDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBankdatum>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankdatumDto>>(entities);
        }

        public async Task<CfnBankdatumDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnBankdatum>().FindAsync(id);
            return _mapper.Map<CfnBankdatumDto?>(entity);
        }

        public async Task<CfnBankdatumDto> CreateAsync(CfnBankdatumDto dto)
        {
            var entity = _mapper.Map<CfnBankdatum>(dto);
            _context.Set<CfnBankdatum>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankdatumDto>(entity);
        }

        public async Task<CfnBankdatumDto> UpdateAsync(CfnBankdatumDto dto)
        {
            var entity = await _context.Set<CfnBankdatum>().FindAsync(dto.Serialnumber);
            if (entity == null) throw new KeyNotFoundException("CfnBankdatum not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankdatumDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnBankdatum>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
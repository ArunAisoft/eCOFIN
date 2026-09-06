using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnCashbookService : ICfnCashbookService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnCashbookService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnCashbookDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnCashbook>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnCashbookDto>>(entities);
        }

        public async Task<CfnCashbookDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnCashbook>().FindAsync(id);
            return _mapper.Map<CfnCashbookDto?>(entity);
        }

        public async Task<CfnCashbookDto> CreateAsync(CfnCashbookDto dto)
        {
            var entity = _mapper.Map<CfnCashbook>(dto);
            _context.Set<CfnCashbook>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCashbookDto>(entity);
        }

        public async Task<CfnCashbookDto> UpdateAsync(CfnCashbookDto dto)
        {
            var entity = await _context.Set<CfnCashbook>().FindAsync(dto.Cashcontrolaccount);
            if (entity == null) throw new KeyNotFoundException("CfnCashbook not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnCashbookDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnCashbook>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
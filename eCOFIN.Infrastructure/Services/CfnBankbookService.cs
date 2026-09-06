using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankbookService : ICfnBankbookService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankbookService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankbookDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBankbook>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankbookDto>>(entities);
        }

        public async Task<CfnBankbookDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBankbook>().FindAsync(id);
            return _mapper.Map<CfnBankbookDto?>(entity);
        }

        public async Task<CfnBankbookDto> CreateAsync(CfnBankbookDto dto)
        {
            var entity = _mapper.Map<CfnBankbook>(dto);
            _context.Set<CfnBankbook>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankbookDto>(entity);
        }

        public async Task<CfnBankbookDto> UpdateAsync(CfnBankbookDto dto)
        {
            var entity = await _context.Set<CfnBankbook>().FindAsync(dto.Bankcontrolaccount);
            if (entity == null) throw new KeyNotFoundException("CfnBankbook not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankbookDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBankbook>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
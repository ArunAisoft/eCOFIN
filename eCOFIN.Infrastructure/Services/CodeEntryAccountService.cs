using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CodeEntryAccountService : ICodeEntryAccountService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CodeEntryAccountService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CodeEntryAccountDto>> GetAllAsync()
        {
            var entities = await _context.Set<CodeEntryAccount>().ToListAsync();
            return _mapper.Map<IEnumerable<CodeEntryAccountDto>>(entities);
        }

        public async Task<CodeEntryAccountDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CodeEntryAccount>().FindAsync(id);
            return _mapper.Map<CodeEntryAccountDto?>(entity);
        }

        public async Task<CodeEntryAccountDto> CreateAsync(CodeEntryAccountDto dto)
        {
            var entity = _mapper.Map<CodeEntryAccount>(dto);
            _context.Set<CodeEntryAccount>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CodeEntryAccountDto>(entity);
        }

        public async Task<CodeEntryAccountDto> UpdateAsync(CodeEntryAccountDto dto)
        {
            var entity = await _context.Set<CodeEntryAccount>().FindAsync(dto.AccCode);
            if (entity == null) throw new KeyNotFoundException("CodeEntryAccount not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CodeEntryAccountDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CodeEntryAccount>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
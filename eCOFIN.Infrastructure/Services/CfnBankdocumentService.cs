using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnBankdocumentService : ICfnBankdocumentService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnBankdocumentService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnBankdocumentDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnBankdocument>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnBankdocumentDto>>(entities);
        }

        public async Task<CfnBankdocumentDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnBankdocument>().FindAsync(id);
            return _mapper.Map<CfnBankdocumentDto?>(entity);
        }

        public async Task<CfnBankdocumentDto> CreateAsync(CfnBankdocumentDto dto)
        {
            var entity = _mapper.Map<CfnBankdocument>(dto);
            _context.Set<CfnBankdocument>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankdocumentDto>(entity);
        }

        public async Task<CfnBankdocumentDto> UpdateAsync(CfnBankdocumentDto dto)
        {
            var entity = await _context.Set<CfnBankdocument>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnBankdocument not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnBankdocumentDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnBankdocument>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
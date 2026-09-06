using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvvchrtypeService : ICfnInvvchrtypeService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvvchrtypeService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvvchrtypeDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvvchrtype>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvvchrtypeDto>>(entities);
        }

        public async Task<CfnInvvchrtypeDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInvvchrtype>().FindAsync(id);
            return _mapper.Map<CfnInvvchrtypeDto?>(entity);
        }

        public async Task<CfnInvvchrtypeDto> CreateAsync(CfnInvvchrtypeDto dto)
        {
            var entity = _mapper.Map<CfnInvvchrtype>(dto);
            _context.Set<CfnInvvchrtype>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvvchrtypeDto>(entity);
        }

        public async Task<CfnInvvchrtypeDto> UpdateAsync(CfnInvvchrtypeDto dto)
        {
            var entity = await _context.Set<CfnInvvchrtype>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnInvvchrtype not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvvchrtypeDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInvvchrtype>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
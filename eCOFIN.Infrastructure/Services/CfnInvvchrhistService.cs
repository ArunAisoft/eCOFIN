using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnInvvchrhistService : ICfnInvvchrhistService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnInvvchrhistService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnInvvchrhistDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnInvvchrhist>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnInvvchrhistDto>>(entities);
        }

        public async Task<CfnInvvchrhistDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnInvvchrhist>().FindAsync(id);
            return _mapper.Map<CfnInvvchrhistDto?>(entity);
        }

        public async Task<CfnInvvchrhistDto> CreateAsync(CfnInvvchrhistDto dto)
        {
            var entity = _mapper.Map<CfnInvvchrhist>(dto);
            _context.Set<CfnInvvchrhist>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvvchrhistDto>(entity);
        }

        public async Task<CfnInvvchrhistDto> UpdateAsync(CfnInvvchrhistDto dto)
        {
            var entity = await _context.Set<CfnInvvchrhist>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnInvvchrhist not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnInvvchrhistDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnInvvchrhist>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
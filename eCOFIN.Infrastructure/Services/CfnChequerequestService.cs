using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnChequerequestService : ICfnChequerequestService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnChequerequestService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnChequerequestDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnChequerequest>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnChequerequestDto>>(entities);
        }

        public async Task<CfnChequerequestDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnChequerequest>().FindAsync(id);
            return _mapper.Map<CfnChequerequestDto?>(entity);
        }

        public async Task<CfnChequerequestDto> CreateAsync(CfnChequerequestDto dto)
        {
            var entity = _mapper.Map<CfnChequerequest>(dto);
            _context.Set<CfnChequerequest>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnChequerequestDto>(entity);
        }

        public async Task<CfnChequerequestDto> UpdateAsync(CfnChequerequestDto dto)
        {
            var entity = await _context.Set<CfnChequerequest>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnChequerequest not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnChequerequestDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnChequerequest>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
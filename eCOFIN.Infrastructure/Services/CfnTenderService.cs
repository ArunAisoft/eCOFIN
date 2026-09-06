using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTenderService : ICfnTenderService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTenderService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTenderDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTender>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTenderDto>>(entities);
        }

        public async Task<CfnTenderDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTender>().FindAsync(id);
            return _mapper.Map<CfnTenderDto?>(entity);
        }

        public async Task<CfnTenderDto> CreateAsync(CfnTenderDto dto)
        {
            var entity = _mapper.Map<CfnTender>(dto);
            _context.Set<CfnTender>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTenderDto>(entity);
        }

        public async Task<CfnTenderDto> UpdateAsync(CfnTenderDto dto)
        {
            var entity = await _context.Set<CfnTender>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnTender not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTenderDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTender>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
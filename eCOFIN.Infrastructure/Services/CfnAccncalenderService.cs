using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAccncalenderService : ICfnAccncalenderService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAccncalenderService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAccncalenderDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAccncalender>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAccncalenderDto>>(entities);
        }

        public async Task<CfnAccncalenderDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnAccncalender>().FindAsync(id);
            return _mapper.Map<CfnAccncalenderDto?>(entity);
        }

        public async Task<CfnAccncalenderDto> CreateAsync(CfnAccncalenderDto dto)
        {
            var entity = _mapper.Map<CfnAccncalender>(dto);
            _context.Set<CfnAccncalender>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAccncalenderDto>(entity);
        }

        public async Task<CfnAccncalenderDto> UpdateAsync(CfnAccncalenderDto dto)
        {
            var entity = await _context.Set<CfnAccncalender>().FindAsync(dto.Accperiod);
            if (entity == null) throw new KeyNotFoundException("CfnAccncalender not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAccncalenderDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnAccncalender>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnTravelsancService : ICfnTravelsancService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnTravelsancService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnTravelsancDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnTravelsanc>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnTravelsancDto>>(entities);
        }

        public async Task<CfnTravelsancDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnTravelsanc>().FindAsync(id);
            return _mapper.Map<CfnTravelsancDto?>(entity);
        }

        public async Task<CfnTravelsancDto> CreateAsync(CfnTravelsancDto dto)
        {
            var entity = _mapper.Map<CfnTravelsanc>(dto);
            _context.Set<CfnTravelsanc>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTravelsancDto>(entity);
        }

        public async Task<CfnTravelsancDto> UpdateAsync(CfnTravelsancDto dto)
        {
            var entity = await _context.Set<CfnTravelsanc>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnTravelsanc not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnTravelsancDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnTravelsanc>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
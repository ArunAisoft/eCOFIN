using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class AccCostPieceTabService : IAccCostPieceTabService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public AccCostPieceTabService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccCostPieceTabDto>> GetAllAsync()
        {
            var entities = await _context.Set<AccCostPieceTab>().ToListAsync();
            return _mapper.Map<IEnumerable<AccCostPieceTabDto>>(entities);
        }

        public async Task<AccCostPieceTabDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<AccCostPieceTab>().FindAsync(id);
            return _mapper.Map<AccCostPieceTabDto?>(entity);
        }

        public async Task<AccCostPieceTabDto> CreateAsync(AccCostPieceTabDto dto)
        {
            var entity = _mapper.Map<AccCostPieceTab>(dto);
            _context.Set<AccCostPieceTab>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccCostPieceTabDto>(entity);
        }

        public async Task<AccCostPieceTabDto> UpdateAsync(AccCostPieceTabDto dto)
        {
            var entity = await _context.Set<AccCostPieceTab>().FindAsync(dto.RcNo);
            if (entity == null) throw new KeyNotFoundException("AccCostPieceTab not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccCostPieceTabDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<AccCostPieceTab>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
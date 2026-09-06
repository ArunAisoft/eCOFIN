using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnAmtInWordService : ICfnAmtInWordService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnAmtInWordService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnAmtInWordDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnAmtInWord>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnAmtInWordDto>>(entities);
        }

        public async Task<CfnAmtInWordDto?> GetByIdAsync(decimal id)
        {
            var entity = await _context.Set<CfnAmtInWord>().FindAsync(id);
            return _mapper.Map<CfnAmtInWordDto?>(entity);
        }

        public async Task<CfnAmtInWordDto> CreateAsync(CfnAmtInWordDto dto)
        {
            var entity = _mapper.Map<CfnAmtInWord>(dto);
            _context.Set<CfnAmtInWord>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAmtInWordDto>(entity);
        }

        public async Task<CfnAmtInWordDto> UpdateAsync(CfnAmtInWordDto dto)
        {
            var entity = await _context.Set<CfnAmtInWord>().FindAsync(dto.Value);
            if (entity == null) throw new KeyNotFoundException("CfnAmtInWord not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnAmtInWordDto>(entity);
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var entity = await _context.Set<CfnAmtInWord>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
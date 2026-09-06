using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnDealerService : ICfnDealerService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnDealerService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnDealerDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnDealer>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnDealerDto>>(entities);
        }

        public async Task<CfnDealerDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnDealer>().FindAsync(id);
            return _mapper.Map<CfnDealerDto?>(entity);
        }

        public async Task<CfnDealerDto> CreateAsync(CfnDealerDto dto)
        {
            var entity = _mapper.Map<CfnDealer>(dto);
            _context.Set<CfnDealer>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnDealerDto>(entity);
        }

        public async Task<CfnDealerDto> UpdateAsync(CfnDealerDto dto)
        {
            var entity = await _context.Set<CfnDealer>().FindAsync(dto.Dealercode);
            if (entity == null) throw new KeyNotFoundException("CfnDealer not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnDealerDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnDealer>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
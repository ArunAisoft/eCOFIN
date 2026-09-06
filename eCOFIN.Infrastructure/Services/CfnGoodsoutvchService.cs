using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnGoodsoutvchService : ICfnGoodsoutvchService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnGoodsoutvchService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnGoodsoutvchDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnGoodsoutvch>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnGoodsoutvchDto>>(entities);
        }

        public async Task<CfnGoodsoutvchDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnGoodsoutvch>().FindAsync(id);
            return _mapper.Map<CfnGoodsoutvchDto?>(entity);
        }

        public async Task<CfnGoodsoutvchDto> CreateAsync(CfnGoodsoutvchDto dto)
        {
            var entity = _mapper.Map<CfnGoodsoutvch>(dto);
            _context.Set<CfnGoodsoutvch>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGoodsoutvchDto>(entity);
        }

        public async Task<CfnGoodsoutvchDto> UpdateAsync(CfnGoodsoutvchDto dto)
        {
            var entity = await _context.Set<CfnGoodsoutvch>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnGoodsoutvch not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnGoodsoutvchDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnGoodsoutvch>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
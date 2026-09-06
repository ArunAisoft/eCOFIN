using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnSalesindentService : ICfnSalesindentService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnSalesindentService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnSalesindentDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnSalesindent>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnSalesindentDto>>(entities);
        }

        public async Task<CfnSalesindentDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnSalesindent>().FindAsync(id);
            return _mapper.Map<CfnSalesindentDto?>(entity);
        }

        public async Task<CfnSalesindentDto> CreateAsync(CfnSalesindentDto dto)
        {
            var entity = _mapper.Map<CfnSalesindent>(dto);
            _context.Set<CfnSalesindent>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSalesindentDto>(entity);
        }

        public async Task<CfnSalesindentDto> UpdateAsync(CfnSalesindentDto dto)
        {
            var entity = await _context.Set<CfnSalesindent>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnSalesindent not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnSalesindentDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnSalesindent>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
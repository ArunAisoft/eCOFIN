using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnRequisitionService : ICfnRequisitionService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnRequisitionService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnRequisitionDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnRequisition>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnRequisitionDto>>(entities);
        }

        public async Task<CfnRequisitionDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnRequisition>().FindAsync(id);
            return _mapper.Map<CfnRequisitionDto?>(entity);
        }

        public async Task<CfnRequisitionDto> CreateAsync(CfnRequisitionDto dto)
        {
            var entity = _mapper.Map<CfnRequisition>(dto);
            _context.Set<CfnRequisition>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRequisitionDto>(entity);
        }

        public async Task<CfnRequisitionDto> UpdateAsync(CfnRequisitionDto dto)
        {
            var entity = await _context.Set<CfnRequisition>().FindAsync(dto.CtrlOnholdno);
            if (entity == null) throw new KeyNotFoundException("CfnRequisition not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnRequisitionDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnRequisition>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
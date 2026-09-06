using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CfnReportcontrolService : ICfnReportcontrolService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CfnReportcontrolService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CfnReportcontrolDto>> GetAllAsync()
        {
            var entities = await _context.Set<CfnReportcontrol>().ToListAsync();
            return _mapper.Map<IEnumerable<CfnReportcontrolDto>>(entities);
        }

        public async Task<CfnReportcontrolDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CfnReportcontrol>().FindAsync(id);
            return _mapper.Map<CfnReportcontrolDto?>(entity);
        }

        public async Task<CfnReportcontrolDto> CreateAsync(CfnReportcontrolDto dto)
        {
            var entity = _mapper.Map<CfnReportcontrol>(dto);
            _context.Set<CfnReportcontrol>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnReportcontrolDto>(entity);
        }

        public async Task<CfnReportcontrolDto> UpdateAsync(CfnReportcontrolDto dto)
        {
            var entity = await _context.Set<CfnReportcontrol>().FindAsync(dto.Taskid);
            if (entity == null) throw new KeyNotFoundException("CfnReportcontrol not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CfnReportcontrolDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CfnReportcontrol>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
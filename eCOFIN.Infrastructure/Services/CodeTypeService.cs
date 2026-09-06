using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class CodeTypeService : ICodeTypeService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public CodeTypeService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CodeTypeDto>> GetAllAsync()
        {
            var entities = await _context.Set<CodeType>().ToListAsync();
            return _mapper.Map<IEnumerable<CodeTypeDto>>(entities);
        }

        public async Task<CodeTypeDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<CodeType>().FindAsync(id);
            return _mapper.Map<CodeTypeDto?>(entity);
        }

        public async Task<CodeTypeDto> CreateAsync(CodeTypeDto dto)
        {
            var entity = _mapper.Map<CodeType>(dto);
            _context.Set<CodeType>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CodeTypeDto>(entity);
        }

        public async Task<CodeTypeDto> UpdateAsync(CodeTypeDto dto)
        {
            var entity = await _context.Set<CodeType>().FindAsync(dto.ArticleNo);
            if (entity == null) throw new KeyNotFoundException("CodeType not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CodeTypeDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<CodeType>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
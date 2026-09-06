using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eCOFIN.Infrastructure.Context;

using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.Infrastructure.Services
{
    public class PersonelService : IPersonelService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMapper _mapper;

        public PersonelService(BilzFinDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonelDto>> GetAllAsync()
        {
            var entities = await _context.Set<Personel>().ToListAsync();
            return _mapper.Map<IEnumerable<PersonelDto>>(entities);
        }

        public async Task<PersonelDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Set<Personel>().FindAsync(id);
            return _mapper.Map<PersonelDto?>(entity);
        }

        public async Task<PersonelDto> CreateAsync(PersonelDto dto)
        {
            var entity = _mapper.Map<Personel>(dto);
            _context.Set<Personel>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonelDto>(entity);
        }

        public async Task<PersonelDto> UpdateAsync(PersonelDto dto)
        {
            var entity = await _context.Set<Personel>().FindAsync(dto.EmpNo);
            if (entity == null) throw new KeyNotFoundException("Personel not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonelDto>(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<Personel>().FindAsync(id);
            if (entity == null) return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Backend.Models;
using Backend.Models.Domain;

namespace Backend.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ManufacturerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DomainManufacturer>> GetManufacturers(int pageNumber, int pageSize, string? sortBy = "Name", string? sortOrder = "asc", string? filter = null)
        {
            var query = _context.Manufacturers.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(m => m.Name.Contains(filter));
            }

            query = sortBy.ToLower() switch
            {
                "name" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(m => m.Name) : query.OrderBy(m => m.Name),
                _ => query
            };

            var manufacturers = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DomainManufacturer>>(manufacturers);
        }

        public async Task<DomainManufacturer> GetManufacturerById(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            return _mapper.Map<DomainManufacturer>(manufacturer);
        }

        public async Task<DomainManufacturer> AddManufacturer(DomainManufacturer domainManufacturer)
        {
            var manufacturer = _mapper.Map<Manufacturer>(domainManufacturer);
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            return _mapper.Map<DomainManufacturer>(manufacturer);
        }

        public async Task<DomainManufacturer> UpdateManufacturer(DomainManufacturer domainManufacturer)
        {
            var manufacturer = _mapper.Map<Manufacturer>(domainManufacturer);
            _context.Manufacturers.Update(manufacturer);
            await _context.SaveChangesAsync();
            return _mapper.Map<DomainManufacturer>(manufacturer);
        }

        public async Task DeleteManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
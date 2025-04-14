using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Domain;

namespace Backend.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<DomainManufacturer>> GetManufacturers(int pageNumber, int pageSize, string? sortBy = null, string? sortOrder = null, string? filter = null);
        Task<DomainManufacturer> GetManufacturerById(int id);
        Task<DomainManufacturer> AddManufacturer(DomainManufacturer manufacturer);
        Task<DomainManufacturer> UpdateManufacturer(DomainManufacturer manufacturer);
        Task DeleteManufacturer(int id);
    }
}
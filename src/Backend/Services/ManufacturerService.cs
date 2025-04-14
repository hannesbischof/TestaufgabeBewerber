using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Domain;
using Backend.Repositories;

namespace Backend.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<IEnumerable<DomainManufacturer>> GetManufacturers(int pageNumber, int pageSize, string? sortBy = null, string? sortOrder = null, string? filter = null)
        {
            return await _manufacturerRepository.GetManufacturers(pageNumber, pageSize, sortBy, sortOrder, filter);
        }

        public async Task<DomainManufacturer> GetManufacturerById(int id)
        {
            return await _manufacturerRepository.GetManufacturerById(id);
        }

        public async Task<DomainManufacturer> AddManufacturer(DomainManufacturer manufacturer)
        {
            ValidateManufacturer(manufacturer);
            return await _manufacturerRepository.AddManufacturer(manufacturer);
        }

        public async Task<DomainManufacturer> UpdateManufacturer(DomainManufacturer manufacturer)
        {
            ValidateManufacturer(manufacturer);
            return await _manufacturerRepository.UpdateManufacturer(manufacturer);
        }

        public async Task DeleteManufacturer(int id)
        {
            await _manufacturerRepository.DeleteManufacturer(id);
        }

        private void ValidateManufacturer(DomainManufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(manufacturer.Name) || manufacturer.Name.Length > 100)
            {
                throw new ArgumentException("Manufacturer name must not be empty and must not exceed 100 characters.");
            }
            if (string.IsNullOrWhiteSpace(manufacturer.Description) || manufacturer.Description.Length > 500)
            {
                throw new ArgumentException("Manufacturer description must not be empty and must not exceed 500 characters.");
            }
        }
    }
}
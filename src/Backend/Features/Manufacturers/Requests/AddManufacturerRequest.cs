using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Manufacturers.Requests
{
    public class AddManufacturerRequest : IRequest<DomainManufacturer>
    {
        public DomainManufacturer Manufacturer { get; }
        public AddManufacturerRequest(DomainManufacturer manufacturer)
        {
            Manufacturer = manufacturer;
        }
    }

    public class AddManufacturerRequestHandler : IRequestHandler<AddManufacturerRequest, DomainManufacturer>
    {
        private readonly IManufacturerService _manufacturerService;

        public AddManufacturerRequestHandler(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public async Task<DomainManufacturer> Handle(AddManufacturerRequest request)
        {
            return await _manufacturerService.AddManufacturer(request.Manufacturer);
        }
    }
}
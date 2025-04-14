using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Manufacturers.Requests
{
    public class UpdateManufacturerRequest : IRequest<DomainManufacturer>
    {
        public DomainManufacturer Manufacturer { get; }

        public UpdateManufacturerRequest(DomainManufacturer manufacturer)
        {
            Manufacturer = manufacturer;
        }
    }

    public class UpdateManufacturerRequestHandler : IRequestHandler<UpdateManufacturerRequest, DomainManufacturer>
    {
        private readonly IManufacturerService _manufacturerService;

        public UpdateManufacturerRequestHandler(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public async Task<DomainManufacturer> Handle(UpdateManufacturerRequest request)
        {
            return await _manufacturerService.UpdateManufacturer(request.Manufacturer);
        }
    }
}
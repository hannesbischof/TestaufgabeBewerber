using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Manufacturers.Requests
{
    public class GetManufacturerByIdRequest : IRequest<DomainManufacturer?>
    {
        public int Id { get; }

        public GetManufacturerByIdRequest(int id)
        {
            Id = id;
        }
    }

    public class GetManufacturerByIdRequestHandler : IRequestHandler<GetManufacturerByIdRequest, DomainManufacturer?>
    {
        private readonly IManufacturerService _manufacturerService;

        public GetManufacturerByIdRequestHandler(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public async Task<DomainManufacturer?> Handle(GetManufacturerByIdRequest request)
        {
            return await _manufacturerService.GetManufacturerById(request.Id);
        }
    }
}
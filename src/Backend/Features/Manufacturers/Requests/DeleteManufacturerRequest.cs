using Backend.Mediator;
using Backend.Services;

namespace Backend.Features.Manufacturers.Requests
{
    public class DeleteManufacturerRequest : IRequest<Unit>
    {
        public int Id { get; }

        public DeleteManufacturerRequest(int id)
        {
            Id = id;
        }
    }

    public class DeleteManufacturerRequestHandler : IRequestHandler<DeleteManufacturerRequest, Unit>
    {
        private readonly IManufacturerService _manufacturerService;

        public DeleteManufacturerRequestHandler(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public async Task<Unit> Handle(DeleteManufacturerRequest request)
        {
            await _manufacturerService.DeleteManufacturer(request.Id);
            return Unit.Value;
        }
    }
}
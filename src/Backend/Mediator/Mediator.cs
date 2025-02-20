using Microsoft.Extensions.DependencyInjection;

namespace Backend.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Handler not found for request type {request.GetType()}");

            var method = handlerType.GetMethod("Handle");
            var result = await (Task<TResponse>)method.Invoke(handler, new object[] { request });
            return result;
        }

        public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("Handle");
                await (Task)method.Invoke(handler, new object[] { notification });
            }
        }
    }

    public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {
        Task Handle(TNotification notification);
    }
}

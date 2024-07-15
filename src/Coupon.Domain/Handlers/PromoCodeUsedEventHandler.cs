namespace Coupon.Domain.Handlers
{
    public class PromoCodeUsedEventHandler : INotificationHandler<PromoCodeUsedEvent>, IHandler
    {
        private readonly ILogger<PromoCodeUsedEventHandler> _logger;

        public PromoCodeUsedEventHandler(ILogger<PromoCodeUsedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PromoCodeUsedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Promo code used: {notification.PromoCode.Code}");
            return Task.CompletedTask;
        }
    }
}


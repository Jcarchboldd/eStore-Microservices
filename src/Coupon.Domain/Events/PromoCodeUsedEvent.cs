namespace Coupon.Domain.Events
{
    public class PromoCodeUsedEvent : INotification
    {
        public PromoCode PromoCode { get; }

        public PromoCodeUsedEvent(PromoCode promoCode)
        {
            PromoCode = promoCode;
        }
    }
}


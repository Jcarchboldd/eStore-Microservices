namespace Coupon.Domain.Entities
{
    public class PromoCode : DomainEntity
    {
        public string Code { get; private set; }
        public decimal Discount { get; private set; }
        public int Quantity { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }

        public PromoCode(string code, decimal discount, int quantity, DateTime expirationDate)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Discount = discount > 0 ? discount : throw new ArgumentOutOfRangeException(nameof(discount));
            Quantity = quantity >= 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity));
            ExpirationDate = expirationDate > DateTime.UtcNow ? expirationDate : throw new ArgumentOutOfRangeException(nameof(expirationDate));
            Active = true;
        }

        public void UsePromoCode()
        {
            if (Quantity <= 0)
            {
                throw new InvalidOperationException("Cannot use promo code with zero quantity.");
            }

            Quantity--;
            if (Quantity == 0)
            {
                Deactivate();
            }

            AddDomainEvent(new PromoCodeUsedEvent(this));
        }

        private void Deactivate()
        {
            Active = false;
        }
    }
}
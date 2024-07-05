namespace Coupon.Application.DTOs
{
    public class PromoCodeDTO
    {
        public required string Code { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }
    }
}


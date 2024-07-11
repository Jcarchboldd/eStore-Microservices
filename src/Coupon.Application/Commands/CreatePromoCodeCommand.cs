namespace Coupon.Application.Commands
{
    public class CreatePromoCodeCommand : IRequest<bool>
    {
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public CreatePromoCodeCommand(string code, decimal discount, int quantity, DateTime expirationDate)
        {
            Code = code;
            Discount = discount;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }
    }
}
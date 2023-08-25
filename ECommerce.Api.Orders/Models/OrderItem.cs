namespace ECommerce.Api.Orders.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qauntity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

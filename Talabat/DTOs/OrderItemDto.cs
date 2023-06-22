namespace Talabat.DTOs
{
    public class OrderItemDto
    {
        public int PrductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
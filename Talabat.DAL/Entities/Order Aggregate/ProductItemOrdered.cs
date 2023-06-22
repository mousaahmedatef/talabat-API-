namespace Talabat.DAL.Entities.Order_Aggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {

        }
        public ProductItemOrdered(int prductId, string productName, string pictureUrl)
        {
            PrductId = prductId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int PrductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
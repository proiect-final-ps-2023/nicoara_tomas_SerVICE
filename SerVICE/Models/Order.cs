namespace SerVICE.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ServiceId { get; set; }
        public int SellerID { get; set; }
        public int BuyerId { get; set; }
    }
}

namespace WebApplication3.Entities
{
    public class Shipping
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int adderess { get; set; }
        public int ShippingStatusId { get; set; }
        public  ShippingStatus ShippingStatus { get; set; }
         public DateTime CreatredAt { get; set; }
         public DateTime DeliveredAt { get; set; }
    }
}

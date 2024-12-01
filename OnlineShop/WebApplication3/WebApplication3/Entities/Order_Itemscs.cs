namespace WebApplication3.Entities
{
    public class Order_Items
    {
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }
        public DateTime CreatredAt { get; set; }
    }
}

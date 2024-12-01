namespace WebApplication3.Entities
{
    public class Payments
    {
        public int Id { get; set; }
        public int amount { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod {  get; set; }
        public int PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatredAt { get; set; }
    }
}

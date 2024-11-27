namespace CarRentalSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int RentDays { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime RentDate { get; set; }


        public User User { get; set; }
        public Car Car { get; set; }

    }
}

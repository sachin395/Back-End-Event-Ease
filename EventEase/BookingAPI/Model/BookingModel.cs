using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Model
{
    public class BookingModel
    {

        public string InvoiceId { get; set; } = Guid.NewGuid().ToString();
        public int UserId { get; set; }
        public int EventId { get; set; }
        [Required]
        public int NoOfTicketsBooked { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

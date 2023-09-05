using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class DTO
    {
        [DisplayName("Id")]
        public int BookingId { get; set; }
        [DisplayName("Date")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public List<int>? RoomId { get; set; }

        public int NoOfPerson { get; set; }
        [DisplayName("Rooms")]
        public int TotalRooms { get; set; }
        [DisplayName("Amount")]
        public decimal DepositAmount { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }

        public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    }
}

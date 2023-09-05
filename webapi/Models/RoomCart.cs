using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class RoomCart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string Title { get; set; } = null!;

    public int RoomId { get; set; }

    public decimal PricePerDay { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }
}

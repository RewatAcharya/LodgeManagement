using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class BookingDetail
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Room? Room { get; set; }
}

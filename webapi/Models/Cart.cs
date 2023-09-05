using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime? TimeStamp { get; set; }

    public virtual Room? Room { get; set; } 

    public virtual UserList? User { get; set; }
}

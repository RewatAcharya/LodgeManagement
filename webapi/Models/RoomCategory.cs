using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class RoomCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Rate { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}

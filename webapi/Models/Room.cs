using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImagePath { get; set; }

    public int? Capacity { get; set; }

    public int? NoOfBed { get; set; }

    public decimal PricePerDay { get; set; }

    public string? Status { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Cart? Cart { get; set; }

    public virtual RoomCategory? Category { get; set; } 
}

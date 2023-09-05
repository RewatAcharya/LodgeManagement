using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class RoomInvoice
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public string FullName { get; set; } = null!;

    public int BookingId { get; set; }

    public string Title { get; set; } = null!;

    public decimal PricePerDay { get; set; }

    public int? NoOfDays { get; set; }

    public decimal DepositAmount { get; set; }

    public decimal? TotalPrice { get; set; }
}

using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class FoodDetail
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public DateTime? Date { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? AmountPaid { get; set; }

    public int? FoodId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Food? Food { get; set; }
}

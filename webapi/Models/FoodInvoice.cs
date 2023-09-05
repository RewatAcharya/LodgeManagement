using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class FoodInvoice
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int BookingId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int? Quantity { get; set; }

    public decimal? AmountPaid { get; set; }

    public decimal? TotalPrice { get; set; }
}

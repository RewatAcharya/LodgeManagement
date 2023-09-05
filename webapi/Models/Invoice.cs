using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime InvoiceDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int BillingBy { get; set; }

    public int BookingId { get; set; }

    public virtual UserList? BillingByNavigation { get; set; } 

    public virtual Booking? Booking { get; set; } 
}

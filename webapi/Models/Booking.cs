using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime? ActualCheckInDate { get; set; }

    public DateTime? ActualCheckOutDate { get; set; }

    public int NoOfPerson { get; set; }

    public int TotalRooms { get; set; }

    public decimal DepositAmount { get; set; }

    public int UserId { get; set; }

    public int? CheckInBy { get; set; }

    public int? CheckOutBy { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual UserList? CheckInByNavigation { get; set; }

    public virtual UserList? CheckOutByNavigation { get; set; }

    public virtual ICollection<FoodDetail> FoodDetails { get; set; } = new List<FoodDetail>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual UserList? User { get; set; } 
}

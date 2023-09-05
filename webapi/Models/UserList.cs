using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class UserList
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Address { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Booking> BookingCheckInByNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingCheckOutByNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingUsers { get; set; } = new List<Booking>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}

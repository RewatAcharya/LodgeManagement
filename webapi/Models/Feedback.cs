using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Message { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public int Read { get; set; }
}

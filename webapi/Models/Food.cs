using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Category { get; set; } = null!;

    public string? ImagePath { get; set; }

    public virtual ICollection<FoodDetail> FoodDetails { get; set; } = new List<FoodDetail>();
}

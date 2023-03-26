using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class ModelClock
{
    public int Id { get; set; }

    public string Model { get; set; } = null!;

    public string? Manufacturer { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}

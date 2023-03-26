using System;
using System.Collections.Generic;

namespace TimeFixer.Database;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string NameStatus { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}

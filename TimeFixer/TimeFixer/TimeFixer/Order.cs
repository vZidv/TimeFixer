using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class Order
{
    public int Id { get; set; }

    public int IdClient { get; set; }

    public int IdClock { get; set; }

    public DateTime DateReceiving { get; set; }

    public DateTime DateReturn { get; set; }

    public int IdOrderStatus { get; set; }

    public string? Problem { get; set; }

    public decimal Cost { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ModelClock IdClockNavigation { get; set; } = null!;

    public virtual OrderStatus IdOrderStatusNavigation { get; set; } = null!;
}

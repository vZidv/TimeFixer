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

    public int OrderStatus { get; set; }

    public decimal Cost { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ModelClock IdClockNavigation { get; set; } = null!;

    public virtual OrderStatus OrderStatusNavigation { get; set; } = null!;
}

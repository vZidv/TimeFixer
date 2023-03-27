using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? IdHowDidFindUs { get; set; }

    public virtual HowDidFindU? IdHowDidFindUsNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}

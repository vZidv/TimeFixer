using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class HowDidFindU
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}

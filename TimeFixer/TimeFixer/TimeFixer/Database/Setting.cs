using System;
using System.Collections.Generic;

namespace TimeFixer.Database;

public partial class Setting
{
    public int Id { get; set; }

    public int Theme { get; set; }

    public int Status { get; set; }

    public virtual UserStatus StatusNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}

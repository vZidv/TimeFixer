using System;
using System.Collections.Generic;

namespace TimeFixer.Database;

public partial class UserStatus
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Setting> Settings { get; } = new List<Setting>();
}

using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class UserSetting
{
    public int Id { get; set; }

    public int? Theme { get; set; }

    public int IdStatus { get; set; }

    public virtual UserStatus IdStatusNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}

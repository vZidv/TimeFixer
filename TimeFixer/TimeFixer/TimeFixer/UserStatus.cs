using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class UserStatus
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<UserSetting> UserSettings { get; } = new List<UserSetting>();
}

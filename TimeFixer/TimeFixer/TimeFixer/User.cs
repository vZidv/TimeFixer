using System;
using System.Collections.Generic;

namespace TimeFixer;

public partial class User
{
    public int Ld { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdSetting { get; set; }

    public virtual UserSetting? IdSettingNavigation { get; set; }
}

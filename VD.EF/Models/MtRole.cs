using System;
using System.Collections.Generic;

namespace VD.EF.Models;

public partial class MtRole
{
    public long Id { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<MtAdmin> MtAdmins { get; set; } = new List<MtAdmin>();

    public virtual ICollection<PRolePermission> PRolePermissions { get; set; } = new List<PRolePermission>();
}

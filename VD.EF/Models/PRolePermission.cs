using System;
using System.Collections.Generic;

namespace VD.EF.Models;

public partial class PRolePermission
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public long PermissionId { get; set; }

    public virtual MtPermission Permission { get; set; } = null!;

    public virtual MtRole Role { get; set; } = null!;
}

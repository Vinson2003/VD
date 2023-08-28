using System;
using System.Collections.Generic;

namespace VD.EF.Models;

public partial class MtPermission
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public string Display { get; set; } = null!;

    public int Seq { get; set; }

    public int SubSeq { get; set; }

    public virtual ICollection<PRolePermission> PRolePermissions { get; set; } = new List<PRolePermission>();
}

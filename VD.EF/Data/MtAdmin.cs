using System;
using System.Collections.Generic;

namespace VD.EF.Data;

public partial class MtAdmin
{
    public long Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Created { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? Updated { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PasswordSalt { get; set; }

    public string Status { get; set; } = null!;

    public long RoleId { get; set; }

    public virtual MtRole Role { get; set; } = null!;
}

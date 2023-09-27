using System;
using System.Collections.Generic;

namespace VD.EF.Data;

public partial class MtBrand
{
    public long Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Created { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? Updated { get; set; }

    public bool FlgDeleted { get; set; }

    public string Name { get; set; } = null!;

    public string? BrandPicture { get; set; }

    public virtual ICollection<PTransaction> PTransactions { get; set; } = new List<PTransaction>();
}

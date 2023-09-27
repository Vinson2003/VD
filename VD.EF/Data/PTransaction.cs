using System;
using System.Collections.Generic;

namespace VD.EF.Data;

public partial class PTransaction
{
    public long Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Created { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? Updated { get; set; }

    public bool FlgDeleted { get; set; }

    public DateTime Date { get; set; }

    public long BrandId { get; set; }

    public string? Result { get; set; }

    public virtual MtBrand Brand { get; set; } = null!;
}

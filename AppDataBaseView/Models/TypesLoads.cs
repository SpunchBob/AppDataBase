using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class TypesLoads
{
    public int LoadTypeCode { get; set; }

    public string Name { get; set; } = null!;

    public int AutoTypeCode { get; set; }

    public string Describe { get; set; } = null!;

    public virtual TypesAuto AutoTypeCodeNavigation { get; set; } = null!;

    public virtual ICollection<Loads> Loads { get; set; } = new List<Loads>();
}

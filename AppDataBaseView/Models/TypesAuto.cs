using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class TypesAuto
{
    public int AutoTypeCode { get; set; }

    public string Name { get; set; } = null!;

    public string? Describe { get; set; }

    public virtual ICollection<TypesLoads> TypesLoads { get; set; } = new List<TypesLoads>();
}

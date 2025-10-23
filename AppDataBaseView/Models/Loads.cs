using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class Loads
{
    public int LoadCode { get; set; }

    public string Name { get; set; } = null!;

    public int LoadTypeCode { get; set; }

    public string ExpDate { get; set; } = null!;

    public string? Describe { get; set; }

    public virtual ICollection<Flights> Flights { get; set; } = new List<Flights>();

    public virtual TypesLoads LoadTypeCodeNavigation { get; set; } = null!;
}

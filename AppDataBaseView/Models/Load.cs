using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class Load
{
    public int LoadCode { get; set; }

    public string Name { get; set; } = null!;

    public int LoadTypeCode { get; set; }

    public string ExpDate { get; set; } = null!;

    public string? Describe { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual TypesLoad LoadTypeCodeNavigation { get; set; } = null!;
}

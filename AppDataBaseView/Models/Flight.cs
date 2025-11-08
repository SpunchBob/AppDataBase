using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class Flight
{
    public int FlightCode { get; set; }

    public string Customer { get; set; } = null!;

    public string From { get; set; } = null!;

    public string Where { get; set; } = null!;

    public string? SendDate { get; set; }

    public string? AriveData { get; set; }

    public int LoadCode { get; set; }

    public int? Price { get; set; } = null!;

    public string? IsBought { get; set; }

    public string? IsRefund { get; set; }

    public int EmployeeCode { get; set; }

    public virtual Employee EmployeeCodeNavigation { get; set; } = null!;

    public virtual Load LoadCodeNavigation { get; set; } = null!;
}

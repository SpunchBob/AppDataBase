using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class Flights
{
    public int FlightCode { get; set; }

    public string Customer { get; set; } = null!;

    public string From { get; set; } = null!;

    public string Where { get; set; } = null!;

    public string SendDate { get; set; } = null!;

    public string AriveData { get; set; } = null!;

    public int LoadCode { get; set; }

    public string Price { get; set; } = null!;

    public string? IsBought { get; set; }

    public string? IsRefund { get; set; }

    public int EmployeeCode { get; set; }

    public virtual Employees EmployeeCodeNavigation { get; set; } = null!;

    public virtual Loads LoadCodeNavigation { get; set; } = null!;
}

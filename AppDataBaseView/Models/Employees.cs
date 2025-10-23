using System;
using System.Collections.Generic;

namespace AppDataBaseView.Models;

public partial class Employees
{
    public int EmployeeCode { get; set; }

    public string Fcs { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? Addres { get; set; }

    public string? Phonenumber { get; set; }

    public string Passport { get; set; } = null!;

    public int? Position { get; set; }

    public virtual ICollection<Flights> Flights { get; set; } = new List<Flights>();
}

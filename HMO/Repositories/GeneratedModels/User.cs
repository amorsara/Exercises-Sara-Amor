using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.GeneratedModels;

public partial class User
{
    public string Userid { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int Housenumber { get; set; }

    public DateTime Dateofbirth { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    [JsonIgnore]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    [JsonIgnore]
    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}

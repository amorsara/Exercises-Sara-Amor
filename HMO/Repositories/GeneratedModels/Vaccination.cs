using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.GeneratedModels;

public partial class Vaccination
{
    public int Codevaccination { get; set; }

    public string Userid { get; set; } = null!;

    public DateTime Datevaccination { get; set; }

    public string Manufacturer { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}

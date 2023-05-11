using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.GeneratedModels;

public partial class Patient
{
    public int Codepatient { get; set; }

    public string Userid { get; set; } = null!;

    public DateTime Datepositive { get; set; }

    public DateTime Datenegative { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}

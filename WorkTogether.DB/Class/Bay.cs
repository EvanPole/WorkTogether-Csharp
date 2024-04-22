using System;
using System.Collections.Generic;

namespace WorkTogether.DB.Class;

public partial class Bay
{
    public ulong Id { get; set; }

    public string BayName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

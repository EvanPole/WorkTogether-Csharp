using System;
using System.Collections.Generic;

namespace WorkTogether.DB.Class;

public partial class Status
{
    public ulong Id { get; set; }

    public ulong RackId { get; set; }

    public ulong BayId { get; set; }

    public DateTime EndDate { get; set; }

    public string Info { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

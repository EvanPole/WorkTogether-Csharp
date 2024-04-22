using System;
using System.Collections.Generic;

namespace WorkTogether.DB.Class;

public partial class Rack
{
    public ulong Id { get; set; }

    public ulong BayId { get; set; }

    public int RackId { get; set; }

    public ulong? UserId { get; set; }

    public string? RackName { get; set; }

    public string? RackColor { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

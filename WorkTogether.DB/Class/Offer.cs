using System;
using System.Collections.Generic;

namespace WorkTogether.DB.Class;

public partial class Offer
{
    public ulong Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int RackQty { get; set; }

    public double Price { get; set; }

    public double Discount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

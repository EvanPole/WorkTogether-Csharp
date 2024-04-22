using System;
using System.Collections.Generic;

namespace WorkTogether.DB.Class;

public partial class Order
{
    public ulong Id { get; set; }

    public ulong UserId  { get; set; }

    public ulong RackId { get; set; }

    public ulong BayId { get; set; }

    public double Price { get; set; }

    public int Discount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

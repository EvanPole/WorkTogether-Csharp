using System;
using System.Collections.Generic;

namespace WorkTogether.DB.Class;

public partial class User
{
    public ulong Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? EmailVerifiedAt { get; set; }

    public string Address { get; set; } = null!;

    public int Postalcode { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int Accesslevel { get; set; }

    public string Password { get; set; } = null!;

    public string? RememberToken { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

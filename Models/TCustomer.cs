using System;
using System.Collections.Generic;

namespace EFCoreWebApp.Models;

public partial class TCustomer
{
    public int CustId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModifed { get; set; }

    public virtual ICollection<TAccount> TAccounts { get; set; } = new List<TAccount>();
}

using System;
using System.Collections.Generic;

namespace EFCoreWebApp.Models;

public partial class TAccount
{
    public int AccountId { get; set; }

    public int? CustId { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModifed { get; set; }

    public string AccountName { get; set; }

    public virtual TCustomer Cust { get; set; }
}

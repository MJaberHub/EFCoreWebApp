namespace EFCoreWebApp.Models;

public partial class TBankList
{
    public int BankId { get; set; }

    public string NameEn { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public int? StatusId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}

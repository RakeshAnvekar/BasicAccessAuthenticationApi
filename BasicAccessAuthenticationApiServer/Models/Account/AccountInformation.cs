namespace BasicAccessAuthenticationApiServer.Models.Account;

public sealed class AccountInformation
{
    public int?  AccountId { get; set; }
    public string? AccountHolderName { get; set; } = string.Empty;
    public string?  AccountHolderAddress { get; set; } = string.Empty;
    public string?   AcountHolderEmailId { get; set; } = string.Empty;
    public string? PanNumber { get; set; } = string.Empty;
    public string? AdharNumber { get; set; } = string.Empty;
    public string? Gender { get; set; } = string.Empty;
}

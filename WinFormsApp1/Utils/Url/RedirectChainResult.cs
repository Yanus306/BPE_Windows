namespace WinFormsApp1.Utils.Url;

public class RedirectChainResult {
    public List<RedirectStep> Steps { get; set; } = new();
    public bool Suspicious { get; set; }
    public string? SuspiciousReason { get; set; }
}
namespace WinFormsApp1.Utils.Url;

public class RedirectStep {
    public string Url { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public long ResponseTimeMs { get; set; }
    public bool IsDomainChanged { get; set; }
    public bool IsSslValid { get; set; }
}
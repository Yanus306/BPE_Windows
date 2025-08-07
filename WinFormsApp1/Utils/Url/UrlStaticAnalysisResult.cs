namespace WinFormsApp1.Utils.Url;

public class UrlStaticAnalysisResult {
    public string Url { get; set; } = string.Empty;
    public int Length { get; set; }
    public bool IsIpAddress { get; set; }
    public bool HasAtSymbol { get; set; }
    public bool HasSuspiciousKeywords { get; set; }
    public string? Domain { get; set; }
}
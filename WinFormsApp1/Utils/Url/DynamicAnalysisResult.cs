namespace WinFormsApp1.Utils.Url;

public class DynamicAnalysisResult {
    public string FinalUrl { get; set; } = string.Empty;
    public string PageTitle { get; set; } = string.Empty;
    public bool HasLoginForm { get; set; }
    public bool HasSuspiciousKeyword { get; set; }
}
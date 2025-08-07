namespace WinFormsApp1.Utils.Mail;

public class MailHeaderAuthResult {
    public bool SpfPass { get; set; }
    public string SpfResult { get; set; } = "";
    public string SpfExplanation { get; set; } = "";

    public bool DkimPass { get; set; }
    public string DkimResult { get; set; } = "";
    public string DkimDomain { get; set; } = "";

    public bool DmarcPass { get; set; }
    public string DmarcResult { get; set; } = "";
    public string DmarcPolicy { get; set; } = "";

    public bool OverallPass => SpfPass && DkimPass && DmarcPass;
    public string OverallStatus => OverallPass ? "✅ 인증 성공" : "❌ 인증 실패";
}
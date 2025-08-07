using MimeKit;

namespace WinFormsApp1.Core.Mail;

public class MailContent {
    public string? From;
    public string? Subject;
    public string? Body;
    public DateTime Date;
    public Dictionary<string, string>? Headers;

    public MailContent(MimeMessage message) {
        From = message.From.ToString();
        Subject = message.Subject;
        Body = message.TextBody ?? message.HtmlBody;
        Date = message.Date.UtcDateTime;

        Headers = new Dictionary<string, string>();
        foreach(Header? header in message.Headers) Headers[header.Field] = header.Value;
    }
}
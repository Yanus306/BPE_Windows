namespace WinFormsApp1.Core.Mail.Handler;

public class NaverMailHandler : ApiMailHandler {
    public override void SendMail(MailContent mailContent) {
        throw new NotImplementedException();
    }

    public override Task SendMailAsync(MailContent mailContent) {
        throw new NotImplementedException();
    }

    public override List<MailContent> ReadMail(int limit) {
        throw new NotImplementedException();
    }

    public override Task<List<MailContent>> ReadMailAsync(int limit) {
        throw new NotImplementedException();
    }
}
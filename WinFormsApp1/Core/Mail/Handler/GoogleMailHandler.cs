namespace WinFormsApp1.Core.Mail.Handler;

public class GoogleMailHandler : ApiMailHandler {
    public override void SendMail(Mail mail) {
        throw new NotImplementedException();
    }
    
    public override Task SendMailAsync(Mail mail) {
        throw new NotImplementedException();
    }
    
    public override List<Mail> ReadMail(int limit) {
        throw new NotImplementedException();
    }
    
    public override Task<List<Mail>> ReadMailAsync(int limit) {
        throw new NotImplementedException();
    }
}
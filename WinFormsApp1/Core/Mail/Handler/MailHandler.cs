namespace WinFormsApp1.Core.Mail.Handler;

public abstract class MailHandler {
    public abstract void SendMail(MailContent mailContent);
    public abstract Task SendMailAsync(MailContent mailContent);
    public abstract List<MailContent> ReadMail(int limit);
    public abstract Task<List<MailContent>> ReadMailAsync(int limit);

    public virtual void OnMailArrived(MailContent mailContent) {
        throw new NotImplementedException();
    }

    public virtual void OnMailDeleted(MailContent mailContent) {
        throw new NotImplementedException();
    }
}
namespace WinFormsApp1.Core.Mail.Handler;

public abstract class MailHandler {
    public abstract void SendMail(Mail mail);
    public abstract Task SendMailAsync(Mail mail);
    public abstract List<Mail> ReadMail(int limit);
    public abstract Task<List<Mail>> ReadMailAsync(int limit);

    public virtual void OnMailArrived(Mail mail) {
        throw new NotImplementedException();
    }
    
    public virtual void OnMailDeleted(Mail mail) {
        throw new NotImplementedException();
    }
}
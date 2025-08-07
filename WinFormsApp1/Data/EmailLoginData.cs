namespace WinFormsApp1.Data;

public class EmailLoginData : AccessableLoginData {
    public string Id;
    public string Password;
    public string ImapServer;
    public string SmtpServer;

    public EmailLoginData(string id, string password, string imapServer, string smtpServer) {
        Id = id;
        Password = password;
        ImapServer = imapServer;
        SmtpServer = smtpServer;
    }
}
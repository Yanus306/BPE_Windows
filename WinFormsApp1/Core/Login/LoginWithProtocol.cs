using MailKit.Net.Smtp;
using WinFormsApp1.Data;
using WinFormsApp1.Core.Mail.Handler;

namespace WinFormsApp1.Core.Login;

public class LoginWithProtocol {
    public static async Task<ProtocolMailHandler> Login(string smtp, string imap, string id, string password) {
        using (SmtpClient smtpClient = new()) {
            try {
                await smtpClient.ConnectAsync(smtp, 587);
            } catch (Exception e) {
                throw new LoginException("Failed to connect to SMTP server", e);
            }
            try {
                await smtpClient.AuthenticateAsync(id, password);
            } catch (Exception e) {
                throw new LoginException("Failed to authenticate with SMTP server", e);
            }
            finally {
                await smtpClient.DisconnectAsync(true);
            }
        }
        return await ProtocolMailHandler.CreateProtocolMailHandler(new EmailLoginData(id, password, imap, smtp));
    }
}
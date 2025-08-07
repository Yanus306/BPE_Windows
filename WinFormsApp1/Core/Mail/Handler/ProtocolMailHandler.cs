using MailKit.Net.Imap;
using MimeKit;
using WinFormsApp1.Core.Login;
using WinFormsApp1.Data;
using WinFormsApp1.Core.Login;

namespace WinFormsApp1.Core.Mail.Handler;

public class ProtocolMailHandler : MailHandler, IDisposable, IAsyncDisposable {
    public EmailLoginData LoginData;
    public ImapClient ImapClient;
    private Task? _idleTask;
    private int _currentCount;
    
    private ProtocolMailHandler(EmailLoginData loginData) {
        LoginData = loginData;
        ImapClient = new ImapClient();
    }
    
    public static async Task<ProtocolMailHandler> CreateProtocolMailHandler(EmailLoginData loginData) {
        ProtocolMailHandler handler = new(loginData);
        try {
            await handler.ImapClient.ConnectAsync(loginData.ImapServer, 993);
        } catch (Exception e) {
            throw new LoginException("Failed to connect to IMAP server", e);
        }
        try {
            await handler.ImapClient.AuthenticateAsync(loginData.Id, loginData.Password);
        } catch (Exception e) {
            throw new LoginException("Failed to authenticate with IMAP server", e);
        } finally {
            await handler.ImapClient.DisconnectAsync(true);
        }
        handler._idleTask = Task.Run(handler.RunIdleTask);
        handler.ImapClient.Inbox.CountChanged += handler.OnCountChanged;
        return handler;
    }
    
    private void OnCountChanged(object? sender, EventArgs e) {
        int newCount = ImapClient.Inbox.Count;
        if (newCount > _currentCount) {
            for (int i = _currentCount; i < newCount; i++) {
                MimeMessage? message = ImapClient.Inbox.GetMessage(i);
                OnMailArrived(new Mail(message));
            }
            _currentCount = newCount;
        }
    }
    
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

    private async Task RunIdleTask() {
        await ImapClient.IdleAsync(new CancellationTokenSource(TimeSpan.FromMinutes(10)).Token);
        _idleTask = RunIdleTask();
    }

    ~ProtocolMailHandler() => Dispose();

    public void Dispose() {
        _idleTask?.Dispose();
        ImapClient.Disconnect(true);
        ImapClient.Dispose();
        GC.SuppressFinalize(this);
    }
    
    public async ValueTask DisposeAsync() {
        _idleTask?.Dispose();
        await ImapClient.DisconnectAsync(true);
        ImapClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
using MailKit;
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
            await handler.ImapClient.DisconnectAsync(true);
            throw new LoginException("Failed to authenticate with IMAP server", e);
        }
        await handler.ImapClient.Inbox.OpenAsync(FolderAccess.ReadOnly);
        handler._idleTask = Task.Run(handler.RunIdleTask);
        handler.ImapClient.Inbox.CountChanged += handler.OnCountChanged;
        return handler;
    }
    
    private void OnCountChanged(object? sender, EventArgs e) {
        int newCount = ImapClient.Inbox.Count;
        if (newCount > _currentCount) {
            for (int i = _currentCount; i < newCount; i++) {
                MimeMessage? message = ImapClient.Inbox.GetMessage(i);
                OnMailArrived(new MailContent(message));
            }
            _currentCount = newCount;
        }
    }
    
    public override void SendMail(MailContent mailContent) {
        throw new NotImplementedException();
    }
    
    public override Task SendMailAsync(MailContent mailContent) {
        throw new NotImplementedException();
    }
    
    public override List<MailContent> ReadMail(int limit) {
        List<MailContent> mails = [];
        for(int i = Math.Max(0, ImapClient.Inbox.Count - limit); i < ImapClient.Inbox.Count; i++) {
            MimeMessage? message = ImapClient.Inbox.GetMessage(i);
            if(message != null) mails.Add(new MailContent(message));
        }
        return mails;
    }
    
    public override async Task<List<MailContent>> ReadMailAsync(int limit) {
        List<MailContent> mails = [];
        for(int i = Math.Max(0, ImapClient.Inbox.Count - limit); i < ImapClient.Inbox.Count; i++) {
            MimeMessage? message = await ImapClient.Inbox.GetMessageAsync(i);
            if(message != null) mails.Add(new MailContent(message));
        }
        return mails;
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
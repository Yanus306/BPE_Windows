using System;
using System.Windows.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class InboxForm : Form
    {
        public InboxForm()
        {
            InitializeComponent();
            buttonRefresh.Click += LoadInbox;
            listViewInbox.DoubleClick += OpenSelectedMail;
            LoadInbox(this, EventArgs.Empty);
        }

        private void LoadInbox(object? sender, EventArgs e)
        {
            var service = new EmailService();
            var messages = service.FetchInbox(UserSession.Email ?? "", UserSession.Password ?? "");

            listViewInbox.Items.Clear();
            foreach (var msg in messages)
            {
                var item = new ListViewItem(new[] { msg.From ?? "", msg.Subject ?? "", msg.Date.ToString() });
                item.Tag = msg;
                listViewInbox.Items.Add(item);
            }
        }

        private void OpenSelectedMail(object? sender, EventArgs e)
        {
            if (listViewInbox.SelectedItems.Count == 0) return;
            var message = listViewInbox.SelectedItems[0].Tag as EmailMessage;
            if (message != null)
            {
                new MailDetailForm(message).ShowDialog();
            }
        }

        private void buttonRefresh_Click(object? sender, EventArgs e)
        {

        }
    }
}

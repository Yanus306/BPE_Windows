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

            // 이벤트 핸들러 연결
            buttonRefresh.Click += LoadInbox;
            listViewInbox.DoubleClick += OpenSelectedMail;

            // 초기 메일 로딩
            LoadInbox(this, EventArgs.Empty);
        }

        private void LoadInbox(object? sender, EventArgs e)
        {
            // 로그인 정보 유효성 확인
            if (string.IsNullOrWhiteSpace(UserSession.Email) || string.IsNullOrWhiteSpace(UserSession.Password))
            {
                MessageBox.Show("이메일 또는 비밀번호가 비어 있습니다. 먼저 로그인해주세요.", "인증 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var service = new EmailService();
                var messages = service.FetchInbox(UserSession.Email, UserSession.Password);

                listViewInbox.Items.Clear();

                foreach (var msg in messages)
                {
                    var item = new ListViewItem(new[] { msg.From ?? "", msg.Subject ?? "", msg.Date.ToString() });
                    item.Tag = msg;
                    listViewInbox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메일을 불러오는 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}

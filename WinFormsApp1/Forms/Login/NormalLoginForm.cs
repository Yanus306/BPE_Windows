using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using WinFormsApp1.Designs;

namespace WinFormsApp1.Forms
{
    public partial class NormalLoginForm : Form
    {
        private SiticoneTextBox passBox = null!;
        private SiticoneTextBox smtpBox = null!;
        private SiticoneTextBox imapBox = null!;
        private SiticoneTextBox emailBox = null!;
        private SiticoneButton loginBtn = null!;


        public NormalLoginForm()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "일반 로그인";

            var container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            NormalLoginFormDesign.Build(
                container,
                out smtpBox,
                out imapBox,
                out emailBox,
                out passBox,
                out loginBtn
            );

            this.Controls.Add(container);
        }

        private void SetupEvents()
        {
            // [기존 코드: 입력값 확인용 메시지박스]
            /*
            loginBtn.Click += (s, e) =>
            {
                MessageBox.Show(
                    $"SMTP: {smtpBox.Text}\nIMAP: {imapBox.Text}\n이메일: {emailBox.Text}\n비밀번호: {passBox.Text}",
                    "입력값 확인");
            };
            */

            // [새 코드: 입력값이 모두 채워지면 InboxForm으로 이동]
            loginBtn.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(smtpBox.Text) ||
                    string.IsNullOrWhiteSpace(imapBox.Text) ||
                    string.IsNullOrWhiteSpace(emailBox.Text) ||
                    string.IsNullOrWhiteSpace(passBox.Text))
                {
                    MessageBox.Show("모든 정보를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // TODO: 필요시 UserSession 등에 값 저장
                var inboxForm = new InboxForm();
                inboxForm.ShowDialog();
            };
        }
    }
}
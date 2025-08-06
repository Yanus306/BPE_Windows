using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Forms
{
    public class NormalLoginForm : Form
    {
        private TextBox smtpBox;
        private TextBox imapBox;
        private TextBox emailBox;
        private TextBox passBox;
        private Button loginBtn;

        public NormalLoginForm()
        {
            BuildUI();
        }

        private void BuildUI()
        {
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "일반 로그인";

            var smtpLabel = new Label
            {
                Text = "SMTP 서버:",
                Location = new Point(30, 30),
                Size = new Size(100, 20)
            };
            smtpBox = new TextBox
            {
                Location = new Point(140, 30),
                Size = new Size(200, 23),
                PlaceholderText = "smtp.example.com"
            };

            var imapLabel = new Label
            {
                Text = "IMAP 서버:",
                Location = new Point(30, 70),
                Size = new Size(100, 20)
            };
            imapBox = new TextBox
            {
                Location = new Point(140, 70),
                Size = new Size(200, 23),
                PlaceholderText = "imap.example.com"
            };

            var emailLabel = new Label
            {
                Text = "이메일:",
                Location = new Point(30, 110),
                Size = new Size(100, 20)
            };
            emailBox = new TextBox
            {
                Location = new Point(140, 110),
                Size = new Size(200, 23),
                PlaceholderText = "user@example.com"
            };

            var passLabel = new Label
            {
                Text = "비밀번호:",
                Location = new Point(30, 150),
                Size = new Size(100, 20)
            };
            passBox = new TextBox
            {
                Location = new Point(140, 150),
                Size = new Size(200, 23),
                UseSystemPasswordChar = true,
                PlaceholderText = "비밀번호"
            };

            loginBtn = new Button
            {
                Text = "로그인",
                Location = new Point(140, 200),
                Size = new Size(200, 35)
            };

            loginBtn.Click += LoginBtn_Click;

            this.Controls.AddRange(new Control[]
            {
                smtpLabel, smtpBox,
                imapLabel, imapBox,
                emailLabel, emailBox,
                passLabel, passBox,
                loginBtn
            });
        }

        private void LoginBtn_Click(object? sender, EventArgs e)
        {
            // 여기에 실제 로그인 로직 구현
            MessageBox.Show(
                $"SMTP: {smtpBox.Text}\nIMAP: {imapBox.Text}\n이메일: {emailBox.Text}\n비밀번호: {passBox.Text}",
                "입력값 확인"
            );
            // 실제로는 이 값들을 이용해 인증 시도
        }
    }
}
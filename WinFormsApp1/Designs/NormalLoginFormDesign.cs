// NormalLoginFormDesign.cs
using Siticone.Desktop.UI.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Designs
{
    public static class NormalLoginFormDesign
    {
        public static void Build(
            Control container,
            out SiticoneTextBox smtpBox,
            out SiticoneTextBox imapBox,
            out SiticoneTextBox emailBox,
            out SiticoneTextBox passBox,
            out SiticoneButton loginBtn)
        {
            var smtpLabel = new Label
            {
                Text = "SMTP 서버:",
                Location = new Point(30, 30),
                Size = new Size(100, 20)
            };
            smtpBox = new SiticoneTextBox
            {
                PlaceholderText = "smtp.example.com",
                Location = new Point(140, 30),
                Size = new Size(200, 30)
            };

            var imapLabel = new Label
            {
                Text = "IMAP 서버:",
                Location = new Point(30, 75),
                Size = new Size(100, 20)
            };
            imapBox = new SiticoneTextBox
            {
                PlaceholderText = "imap.example.com",
                Location = new Point(140, 75),
                Size = new Size(200, 30)
            };

            var emailLabel = new Label
            {
                Text = "이메일:",
                Location = new Point(30, 120),
                Size = new Size(100, 20)
            };
            emailBox = new SiticoneTextBox
            {
                PlaceholderText = "user@example.com",
                Location = new Point(140, 120),
                Size = new Size(200, 30)
            };

            var passLabel = new Label
            {
                Text = "비밀번호:",
                Location = new Point(30, 165),
                Size = new Size(100, 20)
            };
            passBox = new SiticoneTextBox
            {
                PlaceholderText = "비밀번호",
                Location = new Point(140, 165),
                Size = new Size(200, 30),
                UseSystemPasswordChar = true
            };

            loginBtn = new SiticoneButton
            {
                Text = "로그인",
                Location = new Point(140, 215),
                Size = new Size(200, 40),
                FillColor = Color.FromArgb(94, 148, 255),
                ForeColor = Color.White
            };

            container.Controls.AddRange(new Control[]
            {
                smtpLabel, smtpBox,
                imapLabel, imapBox,
                emailLabel, emailBox,
                passLabel, passBox,
                loginBtn
            });
        }
    }
}
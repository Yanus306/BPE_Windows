// LoginFormDesign.cs (Siticone 최신 방식 적용)
using Siticone.Desktop.UI.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Designs
{
    public static class LoginFormDesign
    {
        public static void Build(
            Control container,
            out SiticoneTextBox emailBox,
            out SiticoneTextBox passBox,
            out SiticoneButton loginBtn,
            out SiticoneButton googleBtn,
            out SiticoneButton naverBtn,
            out SiticoneButton helpBtn)
        {
            var titleLabel = new Label
            {
                Text = "Login to Your Account",
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Size = new Size(400, 50),
                Location = new Point(20, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            emailBox = new SiticoneTextBox
            {
                PlaceholderText = "이메일을 입력하세요",
                Size = new Size(360, 40),
                Location = new Point(40, 90),
                Cursor = Cursors.IBeam
            };

            passBox = new SiticoneTextBox
            {
                PlaceholderText = "비밀번호를 입력하세요",
                Size = new Size(360, 40),
                Location = new Point(40, 150),
                PasswordChar = '●',
                Cursor = Cursors.IBeam
            };

            loginBtn = new SiticoneButton
            {
                Text = "Login",
                Size = new Size(360, 45),
                Location = new Point(40, 210),
                FillColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White
            };

            var socialDivider = new Label
            {
                Text = "─────────── or sign in with ───────────",
                ForeColor = Color.Gray,
                Size = new Size(360, 20),
                Location = new Point(40, 270),
                TextAlign = ContentAlignment.MiddleCenter
            };

            googleBtn = new SiticoneButton
            {
                Text = "Continue with Google",
                Size = new Size(360, 40),
                Location = new Point(40, 310),
                FillColor = Color.Red,
                ForeColor = Color.White
            };

            naverBtn = new SiticoneButton
            {
                Text = "Continue with Naver",
                Size = new Size(360, 40),
                Location = new Point(40, 360),
                FillColor = Color.Green,
                ForeColor = Color.White
            };

            helpBtn = new SiticoneButton
            {
                Text = "Need help with OAuth setup?",
                Size = new Size(360, 35),
                Location = new Point(40, 415),
                FillColor = Color.Gray,
                ForeColor = Color.White
            };

            container.Controls.AddRange(new Control[]
            {
                titleLabel,
                emailBox,
                passBox,
                loginBtn,
                socialDivider,
                googleBtn,
                naverBtn,
                helpBtn
            });
        }
    }
}
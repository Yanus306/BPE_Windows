using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Forms;
using WinFormsApp1.Services;

namespace WinFormsApp1.Designs
{
    public static class LoginFormDesign
    {
        public static void Build(
            Control container, HopeForm innerForm,
            out HopeTextBox emailBox,
            out HopeTextBox passBox,
            out HopeButton loginBtn,
            out HopeButton googleBtn,
            out HopeButton naverBtn,
            out HopeButton helpBtn)
        {
            innerForm.Size = new Size(420, 580);
            innerForm.BackColor = Color.White;
            innerForm.ControlBox = false;

            var titleLabel = new Label
            {
                Text = "Login to Your Account",
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Size = new Size(400, 50),
                Location = new Point(10, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            emailBox = new HopeTextBox
            {
                ForeColor = Color.Gray,
                Size = new Size(360, 40),
                Location = new Point(30, 90),
                Text = "이메일을 입력하세요"
            };

            passBox = new HopeTextBox
            {
                ForeColor = Color.Gray,
                Size = new Size(360, 40),
                Location = new Point(30, 150),
                Text = "비밀번호를 입력하세요",
                UseSystemPasswordChar = true
            };

            loginBtn = new HopeButton
            {
                Text = "Login",
                Size = new Size(360, 45),
                Location = new Point(30, 210),
                ForeColor = Color.White
            };

            var socialDivider = new Label
            {
                Text = "────────────── or sign in with ──────────────",
                ForeColor = Color.Gray,
                Size = new Size(360, 20),
                Location = new Point(30, 270),
                TextAlign = ContentAlignment.MiddleCenter
            };

            googleBtn = new HopeButton
            {
                Text = "Continue with Google",
                Size = new Size(360, 40),
                Location = new Point(30, 310),
                ForeColor = Color.White
            };

            naverBtn = new HopeButton
            {
                Text = "Continue with Naver",
                Size = new Size(360, 40),
                Location = new Point(30, 360),
                ForeColor = Color.White
            };

            helpBtn = new HopeButton
            {
                Text = "Need help with OAuth setup?",
                Size = new Size(360, 35),
                Location = new Point(30, 415),
                ForeColor = Color.White
            };

            // 컨테이너 폼에 추가
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

            // 이벤트 핸들러
            googleBtn.Click += (s, e) => OAuthService.HandleGoogleLogin();
            naverBtn.Click += (s, e) => OAuthService.HandleNaverLogin();
            helpBtn.Click += (s, e) => new OAuthSetupForm().ShowDialog();
        }
    }
}

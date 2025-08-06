// LoginForm.cs (.NET 8 + ReaLTaiizor 연결 - HopeForm 포함 Wrapper 개선)
using ReaLTaiizor.Forms;
using ReaLTaiizor.Controls;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Designs;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class LoginForm : Form
    {
        private HopeTextBox emailBox;
        private HopeTextBox passBox;
        private HopeButton loginBtn;
        private HopeButton googleBtn;
        private HopeButton naverBtn;
        private HopeButton helpBtn;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(440, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Modern Login";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // HopeForm을 감싸는 Panel (System.Windows.Forms.Panel 명시)
            System.Windows.Forms.Panel hopeContainer = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // HopeForm은 Panel에 Dock 시켜 내부 컨트롤만 보여주도록 구성
            HopeForm innerForm = new HopeForm();
            LoginFormDesign.Build(
                hopeContainer,
                innerForm,
                out emailBox,
                out passBox,
                out loginBtn,
                out googleBtn,
                out naverBtn,
                out helpBtn);

            // HopeForm의 컨트롤을 Panel에 직접 구성하고, Panel만 메인 Form에 추가
            this.Controls.Add(hopeContainer);

            // 이벤트 연결
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            loginBtn.Click += (s, e) => HandleLogin();
            googleBtn.Click += (s, e) => OAuthService.HandleGoogleLogin();
            naverBtn.Click += (s, e) => OAuthService.HandleNaverLogin();
            helpBtn.Click += (s, e) => new OAuthSetupForm().ShowDialog();
        }

        private void HandleLogin()
        {
            string email = emailBox.Text.Trim();
            string password = passBox.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("이메일과 비밀번호를 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: 로그인 로직 연결
            MessageBox.Show($"로그인 시도: {email}", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
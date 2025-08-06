// LoginForm.cs (.NET 8 + Siticone 최신 방식 적용 - 중복 제거 및 구조화)
using Siticone.Desktop.UI.WinForms;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Designs;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class LoginForm : Form
    {
        private SiticoneTextBox emailBox;
        private SiticoneTextBox passBox;
        private SiticoneButton loginBtn;
        private SiticoneButton googleBtn;
        private SiticoneButton naverBtn;
        private SiticoneButton helpBtn;

        private SiticoneBorderlessForm borderless;

        public LoginForm()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(440, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Modern Login";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            borderless = new SiticoneBorderlessForm
            {
                ContainerControl = this
            };

            var container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            LoginFormDesign.Build(
                container,
                out emailBox,
                out passBox,
                out loginBtn,
                out googleBtn,
                out naverBtn,
                out helpBtn);

            this.Controls.Add(container);
        }

        private void SetupEventHandlers()
        {
            loginBtn.Click += (_, _) => HandleLogin();
            googleBtn.Click += (_, _) => OAuthService.HandleGoogleLogin();
            naverBtn.Click += (_, _) => OAuthService.HandleNaverLogin();
            helpBtn.Click += (_, _) => new OAuthSetupForm().ShowDialog();
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

            // TODO: 로그인 처리 로직 연결 예정
            MessageBox.Show($"로그인 시도: {email}", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
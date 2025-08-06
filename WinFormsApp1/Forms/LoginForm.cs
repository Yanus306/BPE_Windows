// LoginForm.cs (.NET 8 + Siticone 최신 방식 적용 - 중복 제거 및 구조화)
using Siticone.Desktop.UI.WinForms;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Designs;
using WinFormsApp1.Services;
using NormalLoginForm = WinFormsApp1.Forms.NormalLoginForm;

namespace WinFormsApp1.Forms
{
    public partial class LoginForm : Form
    {
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
                out loginBtn,
                out googleBtn,
                out naverBtn,
                out helpBtn);

            this.Controls.Add(container);
        }

        private void SetupEventHandlers()
        {
            loginBtn.Click += (_, _) => NormalLoginForm();
            googleBtn.Click += (_, _) => OAuthService.HandleGoogleLogin();
            naverBtn.Click += (_, _) => OAuthService.HandleNaverLogin();
            helpBtn.Click += (_, _) => new OAuthSetupForm().ShowDialog();
        }
    }
}
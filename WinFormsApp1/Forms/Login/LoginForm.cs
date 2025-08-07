// LoginForm.cs (.NET 8 + Siticone 최신 방식 적용 - 중복 제거 및 구조화)
using Siticone.Desktop.UI.WinForms;
using WinFormsApp1.Designs;

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
            loginBtn.Click += (_, _) => {
                var normalLoginForm = new NormalLoginForm();
                normalLoginForm.Show();
            };
            googleBtn.Click += (_, _) => throw new NotImplementedException();
            naverBtn.Click += (_, _) => throw new NotImplementedException();
            helpBtn.Click += (_, _) => new OAuthSetupForm().ShowDialog();
        }
    }
}
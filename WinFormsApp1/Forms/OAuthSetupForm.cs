// OAuthSetupForm.cs (Siticone 기반 - 최신 스타일 적용 및 오류 수정)
using Siticone.Desktop.UI.WinForms;
using Siticone.Desktop.UI;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Utils;

namespace WinFormsApp1.Forms
{
    public partial class OAuthSetupForm : Form
    {
        private SiticoneTabControl tabControl;
        private SiticoneButton closeButton;
        private SiticoneBorderlessForm borderless;

        public OAuthSetupForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Size = new Size(600, 500);
            this.Text = "OAuth 설정 가이드";
            this.StartPosition = FormStartPosition.CenterParent;

            borderless = new SiticoneBorderlessForm();
            borderless.ContainerControl = this;

            tabControl = new SiticoneTabControl
            {
                Dock = DockStyle.Top,
                Font = new Font("맑은 고딕", 9),
                Height = 420
            };

            AddTab("Google OAuth", OAuthConfig.SetupGuide.GetGoogleSetupGuide());
            AddTab("Naver OAuth", OAuthConfig.SetupGuide.GetNaverSetupGuide());
            AddTab("설정 파일", GetConfigFileContent());

            closeButton = new SiticoneButton
            {
                Text = "닫기",
                Size = new Size(100, 35),
                Anchor = AnchorStyles.Bottom,
                FillColor = Color.Gray,
                ForeColor = Color.White
            };

            closeButton.Click += (sender, e) => this.Close();

            this.Load += (s, e) =>
            {
                closeButton.Location = new Point((this.ClientSize.Width - closeButton.Width) / 2, 440);
            };

            this.Controls.Add(tabControl);
            this.Controls.Add(closeButton);
            this.ResumeLayout(false);
        }

        private void AddTab(string title, string content)
        {
            var tab = new TabPage(title);
            var textBox = new SiticoneTextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = content,
                Dock = DockStyle.Fill,
                Font = title.Contains("설정") ? new Font("Consolas", 9) : new Font("맑은 고딕", 9)
            };
            tab.Controls.Add(textBox);
            tabControl.TabPages.Add(tab);
        }

        private string GetConfigFileContent()
        {
            return string.Join("\n", new string[]
            {
                "// OAuthConfig.cs 파일에서 설정값을 변경하세요",
                "",
                "// namespace WinFormsApp1.Utils",
                "// {",
                "//     public static class OAuthConfig",
                "//     {",
                "//         public static class Google",
                "//         {",
                "//             public const string ClientId = \"YOUR_GOOGLE_CLIENT_ID\";",
                "//             public const string ClientSecret = \"YOUR_GOOGLE_CLIENT_SECRET\";",
                "//         }",
                "",
                "//         public static class Naver",
                "//         {",
                "//             public const string ClientId = \"YOUR_NAVER_CLIENT_ID\";",
                "//             public const string ClientSecret = \"YOUR_NAVER_CLIENT_SECRET\";",
                "//         }",
                "//     }",
                "// }",
                "",
                "// 주의사항:",
                "// 1. ClientId와 ClientSecret은 각 서비스에서 발급받은 값을 사용하세요.",
                "// 2. 실제 배포 시에는 환경 변수나 암호화된 설정 파일을 사용하세요.",
                "// 3. ClientSecret은 절대 공개 저장소에 업로드하지 마세요."
            });
        }
    }
}

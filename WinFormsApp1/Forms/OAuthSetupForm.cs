using MetroFramework.Forms;
using MetroFramework.Controls;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Utils;

namespace WinFormsApp1.Forms
{
    public partial class OAuthSetupForm : MetroForm
    {
        public OAuthSetupForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "OAuth 설정 가이드";
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Style = MetroFramework.MetroColorStyle.Blue;

            // 탭 컨트롤 생성
            var tabControl = new MetroFramework.Controls.MetroTabControl
            {
                Location = new Point(20, 20),
                Size = new Size(560, 430),
                Dock = DockStyle.Fill
            };

            // Google 설정 탭
            var googleTab = new MetroFramework.Controls.MetroTabPage
            {
                Text = "Google OAuth",
                UseStyleColors = true
            };

            var googleTextBox = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(520, 350),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = OAuthConfig.SetupGuide.GetGoogleSetupGuide(),
                Font = new Font("맑은 고딕", 9)
            };

            googleTab.Controls.Add(googleTextBox);

            // Naver 설정 탭
            var naverTab = new MetroFramework.Controls.MetroTabPage
            {
                Text = "Naver OAuth",
                UseStyleColors = true
            };

            var naverTextBox = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(520, 350),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = OAuthConfig.SetupGuide.GetNaverSetupGuide(),
                Font = new Font("맑은 고딕", 9)
            };

            naverTab.Controls.Add(naverTextBox);

            // 설정 파일 탭
            var configTab = new MetroFramework.Controls.MetroTabPage
            {
                Text = "설정 파일",
                UseStyleColors = true
            };

            var configTextBox = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(520, 350),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = GetConfigFileContent(),
                Font = new Font("Consolas", 9)
            };

            configTab.Controls.Add(configTextBox);

            // 탭들을 탭 컨트롤에 추가
            tabControl.TabPages.Add(googleTab);
            tabControl.TabPages.Add(naverTab);
            tabControl.TabPages.Add(configTab);

            // 닫기 버튼
            var closeButton = new MetroFramework.Controls.MetroButton
            {
                Text = "닫기",
                Location = new Point(250, 460),
                Size = new Size(100, 35),
                UseStyleColors = true
            };
            closeButton.Click += (s, e) => this.Close();

            // 컨트롤들을 폼에 추가
            this.Controls.Add(tabControl);
            this.Controls.Add(closeButton);
        }

        private string GetConfigFileContent()
        {
            return @"// OAuthConfig.cs 파일에서 설정값을 변경하세요

namespace WinFormsApp1.Utils
{
    public static class OAuthConfig
    {
        public static class Google
        {
            public const string ClientId = ""YOUR_GOOGLE_CLIENT_ID"";
            public const string ClientSecret = ""YOUR_GOOGLE_CLIENT_SECRET"";
            // 다른 설정들은 기본값 사용
        }

        public static class Naver
        {
            public const string ClientId = ""YOUR_NAVER_CLIENT_ID"";
            public const string ClientSecret = ""YOUR_NAVER_CLIENT_SECRET"";
            // 다른 설정들은 기본값 사용
        }
    }
}

주의사항:
1. ClientId와 ClientSecret은 각 서비스에서 발급받은 값을 사용하세요
2. 실제 배포 시에는 환경 변수나 암호화된 설정 파일을 사용하세요
3. ClientSecret은 절대 공개 저장소에 업로드하지 마세요";
        }
    }
} 
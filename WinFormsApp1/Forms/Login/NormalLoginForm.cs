using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using WinFormsApp1.Designs;
using WinFormsApp1.Common;

namespace WinFormsApp1.Forms
{
    public partial class NormalLoginForm : Form
    {
        private SiticoneTextBox passBox = null!;
        private SiticoneTextBox smtpBox = null!;
        private SiticoneTextBox imapBox = null!;
        private SiticoneTextBox emailBox = null!;
        private SiticoneButton loginBtn = null!;


        public NormalLoginForm()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "일반 로그인";

            var container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            NormalLoginFormDesign.Build(
                container,
                out smtpBox,
                out imapBox,
                out emailBox,
                out passBox,
                out loginBtn
            );

            this.Controls.Add(container);
        }

        private void SetupEvents()
        {
            loginBtn.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(smtpBox.Text) ||
                    string.IsNullOrWhiteSpace(imapBox.Text) ||
                    string.IsNullOrWhiteSpace(emailBox.Text) ||
                    string.IsNullOrWhiteSpace(passBox.Text))
                {
                    MessageBox.Show("모든 정보를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                loginBtn.Enabled = false;

                // ✅ 로딩 오버레이 표시
                using (var loading = new LoadingForm(this, "로그인 중입니다..."))
                {
                    loading.Show();
                    loading.Refresh(); // 강제로 그리기

                    await Task.Delay(1500); // 실제 로그인 처리 위치
                                            // TODO: EmailService.LoginAsync(...) 등 백엔드 호출 넣을 수 있음

                    loading.Close(); // 완료 후 닫기
                }

                loginBtn.Enabled = true;

                var inboxForm = new InboxForm();
                inboxForm.ShowDialog();
            };
        }

    }
}
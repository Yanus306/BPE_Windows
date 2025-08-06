using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using WinFormsApp1.Designs;

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
            loginBtn.Click += (s, e) =>
            {
                MessageBox.Show(
                    $"SMTP: {smtpBox.Text}\nIMAP: {imapBox.Text}\n이메일: {emailBox.Text}\n비밀번호: {passBox.Text}",
                    "입력값 확인");
            };
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using Siticone.Desktop.UI.WinForms.Enums;

namespace WinFormsApp1.Common
{
    public class LoadingForm: Form
    {
        private SiticoneProgressIndicator spinner;
        private Label messageLabel;

        public LoadingForm(Form owner, string message = "처리 중입니다...")
        {
            // ✔ 전체 화면 덮기
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = owner.Bounds;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.BackColor = Color.Black;
            this.Opacity = 0.45;
            this.Owner = owner;

            // ✔ 중앙 카드 스타일 컨테이너
            var container = new Panel
            {
                Size = new Size(220, 120),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Region = Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, 220, 120, 20, 20)), // 라운드 처리
            };

            // ✔ 가운데 정렬
            container.Location = new Point(
                (this.Width - container.Width) / 2,
                (this.Height - container.Height) / 2
            );

            // ✔ Spinner
            spinner = new SiticoneProgressIndicator
            {
                Location = new Point(75, 15),
                Size = new Size(70, 70),
                ProgressColor = Color.FromArgb(94, 148, 255),
                CircleSize = 1.4f,
                ShadowDecoration = { Mode = ShadowMode.Circle },
            };
            spinner.Start();

            // ✔ 메시지 라벨
            messageLabel = new Label
            {
                Text = message,
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("맑은 고딕", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            container.Controls.Add(spinner);
            container.Controls.Add(messageLabel);
            this.Controls.Add(container);
        }

        public void SetMessage(string msg) => messageLabel.Text = msg;

        // ✔ 라운딩 처리용 네이티브 메서드
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
                );
                
        }
    }
}

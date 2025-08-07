using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace WinFormsApp1.Forms.Login {
    public class GoogleLoginForm : Form {
        // TODO: 아래 값을 본인 Google API Console에서 발급받은 값으로 변경하세요.
        private const string ClientId = "YOUR_GOOGLE_CLIENT_ID";
        private const string ClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
        private const string RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        private const string Scope = "https://mail.google.com/";

        private Button startGoogleAuthBtn;

        public GoogleLoginForm() {
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Google 로그인";

            var infoLabel = new Label {
                Text = "Google OAuth2 인증을 시작하려면 아래 버튼을 누르세요.",
                Location = new Point(30, 30),
                Size = new Size(340, 30)
            };

            startGoogleAuthBtn = new Button {
                Text = "Google 인증 시작",
                Location = new Point(100, 80),
                Size = new Size(200, 40)
            };
            startGoogleAuthBtn.Click += StartGoogleAuthBtn_Click;

            this.Controls.Add(infoLabel);
            this.Controls.Add(startGoogleAuthBtn);
        }

        private void StartGoogleAuthBtn_Click(object? sender, EventArgs e) {
            string authUrl =
                $"https://accounts.google.com/o/oauth2/v2/auth?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope={Scope}&access_type=offline";
            Process.Start(new ProcessStartInfo { FileName = authUrl, UseShellExecute = true });
            ShowCodeInputDialog();
        }

        private void ShowCodeInputDialog() {
            var inputForm = new Form {
                Text = "인증 코드 입력",
                Size = new Size(400, 180),
                StartPosition = FormStartPosition.CenterParent
            };
            var label = new Label { Text = "브라우저에서 받은 인증 코드를 입력하세요:", Location = new Point(20, 20), Size = new Size(340, 20) };
            var textBox = new TextBox { Location = new Point(20, 50), Size = new Size(340, 23) };
            var okBtn = new Button { Text = "확인", Location = new Point(270, 90), Size = new Size(90, 30) };
            var statusLabel = new Label { Text = "", Location = new Point(20, 120), Size = new Size(340, 20), ForeColor = Color.Red };
            okBtn.Click += async (s, e) =>
            {
                okBtn.Enabled = false;
                statusLabel.Text = "인증 중...";
                var code = textBox.Text.Trim();
                if(string.IsNullOrWhiteSpace(code)) {
                    statusLabel.Text = "코드를 입력하세요.";
                    okBtn.Enabled = true;
                    return;
                }
                var result = await ExchangeCodeForToken(code);
                if(result.success) {
                    MessageBox.Show("Google 로그인 성공!\nAccess Token: " + result.accessToken, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inputForm.Close();
                } else {
                    statusLabel.Text = "로그인 실패: " + result.error;
                    okBtn.Enabled = true;
                }
            };
            inputForm.Controls.Add(label);
            inputForm.Controls.Add(textBox);
            inputForm.Controls.Add(okBtn);
            inputForm.Controls.Add(statusLabel);
            inputForm.ShowDialog(this);
        }

        private async Task<(bool success, string? accessToken, string? error)> ExchangeCodeForToken(string code) {
            try {
                using var client = new HttpClient();
                var content = new StringContent($"code={code}&client_id={ClientId}&client_secret={ClientSecret}&redirect_uri={RedirectUri}&grant_type=authorization_code",
                    Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
                var body = await response.Content.ReadAsStringAsync();
                if(!response.IsSuccessStatusCode) {
                    return (false, null, $"HTTP 오류: {response.StatusCode}\n{body}");
                }
                using var doc = JsonDocument.Parse(body);
                if(doc.RootElement.TryGetProperty("access_token", out var tokenElem)) {
                    return (true, tokenElem.GetString(), null);
                } else if(doc.RootElement.TryGetProperty("error", out var errElem)) {
                    return (false, null, errElem.GetString());
                } else {
                    return (false, null, "알 수 없는 응답: " + body);
                }
            } catch (Exception ex) {
                return (false, null, ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Utils;
using WinFormsApp1.Utils.Mail;

namespace WinFormsApp1.Forms
{
    public partial class MailDetailForm : Form
    {
        private EmailMessage _message;
        private MailHeaderAuthResult? _authResult;

        public MailDetailForm(EmailMessage message)
        {
            InitializeComponent();
            _message = message;
            
            // 기본 메일 정보 표시
            labelSubject.Text = "제목: " + message.Subject;
            
            // 발신자 정보 파싱 및 표시
            var senderInfo = WinFormsApp1.Utils.MailSenderParser.Parse(message.From ?? "");
            labelFrom.Text = $"보낸이: {senderInfo.DisplayName ?? "(없음)"} <{senderInfo.Email ?? "?"}>\n도메인: {senderInfo.Domain ?? "?"}";
            
            textBoxBody.Text = message.Body;
            
            // 이벤트 핸들러 설정
            buttonViewAuthDetails.Click += ButtonViewAuthDetails_Click;
            
            // 메일 헤더 인증 분석 실행
            AnalyzeMailHeaders();
        }

        private void AnalyzeMailHeaders()
        {
            try
            {
                // 실제 메일 헤더 정보 사용
                var headers = _message.Headers ?? new Dictionary<string, string>();
                
                // 헤더가 없는 경우 시뮬레이션 데이터 사용 (테스트용)
                if (headers.Count == 0)
                {
                    headers = new Dictionary<string, string>
                    {
                        { "Received-SPF", "pass (google.com: domain of sender@example.com designates 192.168.1.1 as permitted sender)" },
                        { "DKIM-Signature", "v=1; a=rsa-sha256; d=example.com; s=selector; c=relaxed/relaxed; t=1234567890; bh=...; h=From:To:Subject:Date; b=..." },
                        { "X-DMARC-Result", "pass (p=quarantine sp=quarantine pct=100 adkim=r subdomainPolicy=quarantine)" }
                    };
                }

                _authResult = MailHeaderAuthAnalyzer.AnalyzeHeaders(headers);
                
                // 인증 상태 표시
                UpdateAuthStatus();
            }
            catch (Exception ex)
            {
                labelAuthStatus.Text = "❌ 분석 오류";
                labelAuthStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void UpdateAuthStatus()
        {
            if (_authResult?.OverallPass == true)
            {
                labelAuthStatus.Text = "✅ 인증 성공";
                labelAuthStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                labelAuthStatus.Text = "❌ 인증 실패";
                labelAuthStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void ButtonViewAuthDetails_Click(object? sender, EventArgs e)
        {
            if (_authResult != null)
            {
                var details = MailHeaderAuthAnalyzer.GetDetailedReport(_authResult);
                var recommendation = MailHeaderAuthAnalyzer.GetSecurityRecommendation(_authResult);
                
                var fullReport = details + "\n" + recommendation;
                
                MessageBox.Show(fullReport, "메일 인증 상세 분석", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

using System;
using System.Windows.Forms;
using WinFormsApp1.Models;

namespace WinFormsApp1.Forms
{
    public partial class MailDetailForm : Form
    {
        public MailDetailForm(EmailMessage message)
        {
            InitializeComponent();
            labelSubject.Text = "제목: " + message.Subject;
            // 기존 코드: 단순 From 표시
            // labelFrom.Text = "보낸이: " + message.From;

            // 새 코드: MailSenderParser로 파싱하여 상세 정보 표시
            var senderInfo = Utils.MailSenderParser.Parse(message.From ?? "");
            labelFrom.Text = $"보낸이: {senderInfo.DisplayName ?? "(없음)"} <{senderInfo.Email ?? "?"}>\n도메인: {senderInfo.Domain ?? "?"}";
            textBoxBody.Text = message.Body;
        }
    }
}

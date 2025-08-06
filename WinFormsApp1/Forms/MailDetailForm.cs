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
            labelFrom.Text = "보낸이: " + message.From;
            textBoxBody.Text = message.Body;
        }
    }
}

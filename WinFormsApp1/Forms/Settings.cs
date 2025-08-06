using System;
using System.Windows.Forms;
using WinFormsApp1.Models;

namespace WinFormsApp1.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBoxAccount.Text = UserSession.Email;
            buttonSave.Click += (s, e) => MessageBox.Show("저장 완료");
        }
    }
}

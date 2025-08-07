namespace WinFormsApp1.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            buttonSave.Click += (s, e) => MessageBox.Show("저장 완료");
        }
    }
}

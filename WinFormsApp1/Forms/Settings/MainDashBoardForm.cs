namespace WinFormsApp1.Forms.Settings {
    public partial class MainDashboardForm : Form {
        public MainDashboardForm() {
            InitializeComponent();
            buttonInbox.Click += (s, e) => throw new NotImplementedException();
            buttonCompose.Click += (s, e) => MessageBox.Show("새 메일 작성 기능은 아직 구현되지 않았습니다.");
        }
    }
}
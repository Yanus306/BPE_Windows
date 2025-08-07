using WinFormsApp1.Core.Mail;
using WinFormsApp1.Core.Mail.Handler;

namespace WinFormsApp1.Forms.Mail {
    public partial class InboxForm : Form {
        private readonly MailHandler _mailHandler;

        public InboxForm(MailHandler handler) {
            _mailHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            InitializeComponent();
            SetupListView(); // ✅ 리스트뷰 세팅 추가

            // 이벤트 핸들러 연결
            buttonRefresh.Click += LoadInbox;
            listViewInbox.DoubleClick += OpenSelectedMail;

            // 초기 메일 로딩
            LoadInbox(this, EventArgs.Empty);
        }

        // ✅ 리스트 뷰 구조 정의
        private void SetupListView() {
            listViewInbox.View = View.Details;
            listViewInbox.FullRowSelect = true;
            listViewInbox.GridLines = true;

            listViewInbox.Columns.Clear();
            listViewInbox.Columns.Add("보낸 사람", 200);
            listViewInbox.Columns.Add("제목", 400);
            listViewInbox.Columns.Add("날짜", 150);
        }

        private void LoadInbox(object? sender, EventArgs e) {
            try {
                List<MailContent> messages = _mailHandler.ReadMail(100);

                listViewInbox.Items.Clear();

                foreach(MailContent msg in messages) {
                    var item = new ListViewItem(
                        new[] {
                            msg.From ?? "",
                            msg.Subject ?? "",
                            msg.Date.ToString("yyyy-MM-dd HH:mm"),
                        }
                    ) {
                        Tag = msg,
                    };
                    listViewInbox.Items.Add(item);
                }
            } catch (Exception ex) {
                MessageBox.Show(
                    $"메일을 불러오는 중 오류가 발생했습니다:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void OpenSelectedMail(object? sender, EventArgs e) {
            if(listViewInbox.SelectedItems.Count == 0)
                return;

            if(listViewInbox.SelectedItems[0].Tag is MailContent message) {
                new MailDetailForm(message).ShowDialog();
            }
        }

        private void buttonRefresh_Click(object? sender, EventArgs e) {
            throw new NotImplementedException();
        }
    }
}
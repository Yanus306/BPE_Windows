using System;
using System.Windows.Forms;
using WinFormsApp1.Forms;
using WinFormsApp1.Forms.Login;

namespace WinFormsApp1 {
    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // �Ϲ� Form �ȿ� HopeForm�� ���Խ��Ѽ� ����
            Application.Run(new LoginForm());
        }
    }
}
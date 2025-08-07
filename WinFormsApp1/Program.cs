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

            // 일반 Form 안에 HopeForm을 포함시켜서 실행
            Application.Run(new LoginForm());
        }
    }
}
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormsApp1.Forms
{
    public partial class LoginForm : Form
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Siticone UI 및 사용자 정의 디자인 적용: InitializeComponent 사용 안 함
        // UI 구성은 LoginForm.cs 또는 LoginFormDesign.cs에서 수동 정의함
    }
}
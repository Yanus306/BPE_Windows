namespace WinFormsApp1.Forms
{
    partial class InboxForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewInbox = new System.Windows.Forms.ListView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewInbox
            // 
            this.listViewInbox.Location = new System.Drawing.Point(12, 12);
            this.listViewInbox.Name = "listViewInbox";
            this.listViewInbox.Size = new System.Drawing.Size(560, 350);
            this.listViewInbox.TabIndex = 0;
            this.listViewInbox.UseCompatibleStateImageBehavior = false;
            this.listViewInbox.View = System.Windows.Forms.View.Details;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(480, 370);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(90, 30);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "새로고침";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += this.buttonRefresh_Click;
            // 
            // InboxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.listViewInbox);
            this.Name = "InboxForm";
            this.Text = "받은 편지함";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewInbox;
        private System.Windows.Forms.Button buttonRefresh;
    }
}
namespace WinFormsApp1.Forms
{
    partial class MainDashboardForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxMails = new System.Windows.Forms.ListBox();
            this.panelMailDetail = new System.Windows.Forms.Panel();
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.buttonCompose = new System.Windows.Forms.Button();
            this.buttonInbox = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelMailDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxMails);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelMailDetail);
            this.splitContainer1.Size = new System.Drawing.Size(800, 410);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxMails
            // 
            this.listBoxMails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMails.FormattingEnabled = true;
            this.listBoxMails.ItemHeight = 15;
            this.listBoxMails.Location = new System.Drawing.Point(0, 0);
            this.listBoxMails.Name = "listBoxMails";
            this.listBoxMails.Size = new System.Drawing.Size(250, 410);
            this.listBoxMails.TabIndex = 0;
            // 
            // panelMailDetail
            // 
            this.panelMailDetail.Controls.Add(this.labelSubject);
            this.panelMailDetail.Controls.Add(this.labelFrom);
            this.panelMailDetail.Controls.Add(this.textBoxBody);
            this.panelMailDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMailDetail.Location = new System.Drawing.Point(0, 0);
            this.panelMailDetail.Name = "panelMailDetail";
            this.panelMailDetail.Size = new System.Drawing.Size(546, 410);
            this.panelMailDetail.TabIndex = 0;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSubject.Location = new System.Drawing.Point(10, 10);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(60, 21);
            this.labelSubject.TabIndex = 0;
            this.labelSubject.Text = "제목: ";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(10, 40);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(47, 15);
            this.labelFrom.TabIndex = 1;
            this.labelFrom.Text = "보낸이: ";
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(10, 70);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.ReadOnly = true;
            this.textBoxBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBody.Size = new System.Drawing.Size(520, 320);
            this.textBoxBody.TabIndex = 2;
            // 
            // buttonCompose
            // 
            this.buttonCompose.Location = new System.Drawing.Point(100, 5);
            this.buttonCompose.Name = "buttonCompose";
            this.buttonCompose.Size = new System.Drawing.Size(90, 30);
            this.buttonCompose.TabIndex = 1;
            this.buttonCompose.Text = "새 메일";
            this.buttonCompose.UseVisualStyleBackColor = true;
            // 
            // buttonInbox
            // 
            this.buttonInbox.Location = new System.Drawing.Point(10, 5);
            this.buttonInbox.Name = "buttonInbox";
            this.buttonInbox.Size = new System.Drawing.Size(80, 30);
            this.buttonInbox.TabIndex = 2;
            this.buttonInbox.Text = "받은편지함";
            this.buttonInbox.UseVisualStyleBackColor = true;
            // 
            // MainDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonCompose);
            this.Controls.Add(this.buttonInbox);
            this.Name = "MainDashboardForm";
            this.Text = "Gmail 메일 클라이언트";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelMailDetail.ResumeLayout(false);
            this.panelMailDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxMails;
        private System.Windows.Forms.Panel panelMailDetail;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.Button buttonCompose;
        private System.Windows.Forms.Button buttonInbox;
    }
}
namespace WinFormsApp1.Forms
{
    partial class MailDetailForm
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
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.labelAuthStatus = new System.Windows.Forms.Label();
            this.buttonViewAuthDetails = new System.Windows.Forms.Button();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSubject.Location = new System.Drawing.Point(20, 20);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(60, 21);
            this.labelSubject.TabIndex = 0;
            this.labelSubject.Text = "제목: ";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(20, 50);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(47, 15);
            this.labelFrom.TabIndex = 1;
            this.labelFrom.Text = "보낸이: ";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.labelAuthStatus);
            this.groupBoxAuth.Controls.Add(this.buttonViewAuthDetails);
            this.groupBoxAuth.Location = new System.Drawing.Point(20, 80);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Size = new System.Drawing.Size(560, 80);
            this.groupBoxAuth.TabIndex = 3;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "🔐 메일 인증 분석";
            // 
            // labelAuthStatus
            // 
            this.labelAuthStatus.AutoSize = true;
            this.labelAuthStatus.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAuthStatus.Location = new System.Drawing.Point(15, 25);
            this.labelAuthStatus.Name = "labelAuthStatus";
            this.labelAuthStatus.Size = new System.Drawing.Size(120, 17);
            this.labelAuthStatus.TabIndex = 0;
            this.labelAuthStatus.Text = "분석 중...";
            // 
            // buttonViewAuthDetails
            // 
            this.buttonViewAuthDetails.Location = new System.Drawing.Point(15, 45);
            this.buttonViewAuthDetails.Name = "buttonViewAuthDetails";
            this.buttonViewAuthDetails.Size = new System.Drawing.Size(120, 25);
            this.buttonViewAuthDetails.TabIndex = 1;
            this.buttonViewAuthDetails.Text = "상세 분석 보기";
            this.buttonViewAuthDetails.UseVisualStyleBackColor = true;
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(20, 170);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.ReadOnly = true;
            this.textBoxBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBody.Size = new System.Drawing.Size(560, 230);
            this.textBoxBody.TabIndex = 2;
            // 
            // MailDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 420);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.labelSubject);
            this.Name = "MailDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "메일 상세보기";
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.Label labelAuthStatus;
        private System.Windows.Forms.Button buttonViewAuthDetails;
    }
} 
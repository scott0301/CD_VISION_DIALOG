namespace UC_LogView
{
    partial class UC_LOG_VIEWER
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.RICH_MESSAGE = new System.Windows.Forms.RichTextBox();
            this.BTN_CLEAR = new System.Windows.Forms.Button();
            this.CHK_00_ALL = new System.Windows.Forms.CheckBox();
            this.CHK_01_PWR = new System.Windows.Forms.CheckBox();
            this.CHK_02_RECP = new System.Windows.Forms.CheckBox();
            this.CHK_03_PARAM = new System.Windows.Forms.CheckBox();
            this.CHK_04_IMAGE = new System.Windows.Forms.CheckBox();
            this.CHK_05_MEAS = new System.Windows.Forms.CheckBox();
            this.CHK_06_COMM = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // RICH_MESSAGE
            // 
            this.RICH_MESSAGE.BackColor = System.Drawing.Color.White;
            this.RICH_MESSAGE.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RICH_MESSAGE.ForeColor = System.Drawing.Color.Black;
            this.RICH_MESSAGE.Location = new System.Drawing.Point(3, 3);
            this.RICH_MESSAGE.Name = "RICH_MESSAGE";
            this.RICH_MESSAGE.Size = new System.Drawing.Size(540, 125);
            this.RICH_MESSAGE.TabIndex = 0;
            this.RICH_MESSAGE.Text = "";
            // 
            // BTN_CLEAR
            // 
            this.BTN_CLEAR.BackgroundImage = global::UC_LOG_VIEW.Properties.Resources.Trash_empty;
            this.BTN_CLEAR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CLEAR.ForeColor = System.Drawing.Color.Black;
            this.BTN_CLEAR.Location = new System.Drawing.Point(498, 131);
            this.BTN_CLEAR.Name = "BTN_CLEAR";
            this.BTN_CLEAR.Size = new System.Drawing.Size(45, 45);
            this.BTN_CLEAR.TabIndex = 1;
            this.BTN_CLEAR.UseVisualStyleBackColor = true;
            this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
            // 
            // CHK_00_ALL
            // 
            this.CHK_00_ALL.AutoSize = true;
            this.CHK_00_ALL.Checked = true;
            this.CHK_00_ALL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_00_ALL.ForeColor = System.Drawing.Color.Black;
            this.CHK_00_ALL.Location = new System.Drawing.Point(3, 131);
            this.CHK_00_ALL.Name = "CHK_00_ALL";
            this.CHK_00_ALL.Size = new System.Drawing.Size(51, 18);
            this.CHK_00_ALL.TabIndex = 2;
            this.CHK_00_ALL.Text = "ALL";
            this.CHK_00_ALL.UseVisualStyleBackColor = true;
            this.CHK_00_ALL.CheckedChanged += new System.EventHandler(this.CHK_ALL_CheckedChanged);
            // 
            // CHK_01_PWR
            // 
            this.CHK_01_PWR.AutoSize = true;
            this.CHK_01_PWR.ForeColor = System.Drawing.Color.Black;
            this.CHK_01_PWR.Location = new System.Drawing.Point(70, 131);
            this.CHK_01_PWR.Name = "CHK_01_PWR";
            this.CHK_01_PWR.Size = new System.Drawing.Size(58, 18);
            this.CHK_01_PWR.TabIndex = 2;
            this.CHK_01_PWR.Text = "PWR";
            this.CHK_01_PWR.UseVisualStyleBackColor = true;
            this.CHK_01_PWR.CheckedChanged += new System.EventHandler(this.CHK_LOG_Contents_Changed);
            // 
            // CHK_02_RECP
            // 
            this.CHK_02_RECP.AutoSize = true;
            this.CHK_02_RECP.ForeColor = System.Drawing.Color.Black;
            this.CHK_02_RECP.Location = new System.Drawing.Point(3, 158);
            this.CHK_02_RECP.Name = "CHK_02_RECP";
            this.CHK_02_RECP.Size = new System.Drawing.Size(61, 18);
            this.CHK_02_RECP.TabIndex = 2;
            this.CHK_02_RECP.Text = "RECP";
            this.CHK_02_RECP.UseVisualStyleBackColor = true;
            this.CHK_02_RECP.CheckedChanged += new System.EventHandler(this.CHK_LOG_Contents_Changed);
            // 
            // CHK_03_PARAM
            // 
            this.CHK_03_PARAM.AutoSize = true;
            this.CHK_03_PARAM.ForeColor = System.Drawing.Color.Black;
            this.CHK_03_PARAM.Location = new System.Drawing.Point(70, 158);
            this.CHK_03_PARAM.Name = "CHK_03_PARAM";
            this.CHK_03_PARAM.Size = new System.Drawing.Size(73, 18);
            this.CHK_03_PARAM.TabIndex = 2;
            this.CHK_03_PARAM.Text = "PARAM";
            this.CHK_03_PARAM.UseVisualStyleBackColor = true;
            this.CHK_03_PARAM.CheckedChanged += new System.EventHandler(this.CHK_LOG_Contents_Changed);
            // 
            // CHK_04_IMAGE
            // 
            this.CHK_04_IMAGE.AutoSize = true;
            this.CHK_04_IMAGE.ForeColor = System.Drawing.Color.Black;
            this.CHK_04_IMAGE.Location = new System.Drawing.Point(149, 158);
            this.CHK_04_IMAGE.Name = "CHK_04_IMAGE";
            this.CHK_04_IMAGE.Size = new System.Drawing.Size(70, 18);
            this.CHK_04_IMAGE.TabIndex = 2;
            this.CHK_04_IMAGE.Text = "IMAGE";
            this.CHK_04_IMAGE.UseVisualStyleBackColor = true;
            this.CHK_04_IMAGE.CheckedChanged += new System.EventHandler(this.CHK_LOG_Contents_Changed);
            // 
            // CHK_05_MEAS
            // 
            this.CHK_05_MEAS.AutoSize = true;
            this.CHK_05_MEAS.ForeColor = System.Drawing.Color.Black;
            this.CHK_05_MEAS.Location = new System.Drawing.Point(225, 158);
            this.CHK_05_MEAS.Name = "CHK_05_MEAS";
            this.CHK_05_MEAS.Size = new System.Drawing.Size(63, 18);
            this.CHK_05_MEAS.TabIndex = 2;
            this.CHK_05_MEAS.Text = "MEAS";
            this.CHK_05_MEAS.UseVisualStyleBackColor = true;
            this.CHK_05_MEAS.CheckedChanged += new System.EventHandler(this.CHK_LOG_Contents_Changed);
            // 
            // CHK_06_COMM
            // 
            this.CHK_06_COMM.AutoSize = true;
            this.CHK_06_COMM.ForeColor = System.Drawing.Color.Black;
            this.CHK_06_COMM.Location = new System.Drawing.Point(294, 158);
            this.CHK_06_COMM.Name = "CHK_06_COMM";
            this.CHK_06_COMM.Size = new System.Drawing.Size(68, 18);
            this.CHK_06_COMM.TabIndex = 2;
            this.CHK_06_COMM.Text = "COMM";
            this.CHK_06_COMM.UseVisualStyleBackColor = true;
            this.CHK_06_COMM.CheckedChanged += new System.EventHandler(this.CHK_LOG_Contents_Changed);
            // 
            // UC_LOG_VIEWER
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CHK_06_COMM);
            this.Controls.Add(this.CHK_05_MEAS);
            this.Controls.Add(this.CHK_04_IMAGE);
            this.Controls.Add(this.CHK_03_PARAM);
            this.Controls.Add(this.CHK_02_RECP);
            this.Controls.Add(this.CHK_01_PWR);
            this.Controls.Add(this.CHK_00_ALL);
            this.Controls.Add(this.BTN_CLEAR);
            this.Controls.Add(this.RICH_MESSAGE);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.LimeGreen;
            this.Name = "UC_LOG_VIEWER";
            this.Size = new System.Drawing.Size(548, 180);
            this.Load += new System.EventHandler(this.UC_LOG_VIEWER_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.UC_LOG_VIEWER_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.UC_LOG_VIEWER_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RICH_MESSAGE;
        private System.Windows.Forms.Button BTN_CLEAR;
        private System.Windows.Forms.CheckBox CHK_00_ALL;
        private System.Windows.Forms.CheckBox CHK_01_PWR;
        private System.Windows.Forms.CheckBox CHK_02_RECP;
        private System.Windows.Forms.CheckBox CHK_03_PARAM;
        private System.Windows.Forms.CheckBox CHK_04_IMAGE;
        private System.Windows.Forms.CheckBox CHK_05_MEAS;
        private System.Windows.Forms.CheckBox CHK_06_COMM;
    }
}

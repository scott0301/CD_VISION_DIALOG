namespace CD_VISION_DIALOG
{
    partial class Dlg_HistoryM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dlg_HistoryM));
            this.TXT_HISTORY_PREV_RECP = new System.Windows.Forms.TextBox();
            this.TXT_HISTORY_PREV_IMAGE = new System.Windows.Forms.TextBox();
            this.TXT_HISTORY_CURR_RECP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LV_HISTORY = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RICH_HISTORY_MSG = new System.Windows.Forms.RichTextBox();
            this.uc_view_history = new CD_View.UC_CD_VIEWER();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BTN_HISTORY_EXPERIMENT_SET = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BTN_CLOSE = new System.Windows.Forms.Button();
            this.BTN_UPDATE_HISTORY = new System.Windows.Forms.Button();
            this.BTN_OPEN_HISTORY_FOLDER = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TXT_HISTORY_PREV_RECP
            // 
            this.TXT_HISTORY_PREV_RECP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_HISTORY_PREV_RECP.ForeColor = System.Drawing.Color.White;
            this.TXT_HISTORY_PREV_RECP.Location = new System.Drawing.Point(108, 86);
            this.TXT_HISTORY_PREV_RECP.Name = "TXT_HISTORY_PREV_RECP";
            this.TXT_HISTORY_PREV_RECP.Size = new System.Drawing.Size(372, 22);
            this.TXT_HISTORY_PREV_RECP.TabIndex = 51;
            // 
            // TXT_HISTORY_PREV_IMAGE
            // 
            this.TXT_HISTORY_PREV_IMAGE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_HISTORY_PREV_IMAGE.ForeColor = System.Drawing.Color.White;
            this.TXT_HISTORY_PREV_IMAGE.Location = new System.Drawing.Point(108, 22);
            this.TXT_HISTORY_PREV_IMAGE.Name = "TXT_HISTORY_PREV_IMAGE";
            this.TXT_HISTORY_PREV_IMAGE.Size = new System.Drawing.Size(372, 22);
            this.TXT_HISTORY_PREV_IMAGE.TabIndex = 52;
            // 
            // TXT_HISTORY_CURR_RECP
            // 
            this.TXT_HISTORY_CURR_RECP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_HISTORY_CURR_RECP.ForeColor = System.Drawing.Color.White;
            this.TXT_HISTORY_CURR_RECP.Location = new System.Drawing.Point(108, 53);
            this.TXT_HISTORY_CURR_RECP.Name = "TXT_HISTORY_CURR_RECP";
            this.TXT_HISTORY_CURR_RECP.Size = new System.Drawing.Size(372, 22);
            this.TXT_HISTORY_CURR_RECP.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 14);
            this.label2.TabIndex = 48;
            this.label2.Text = "PREV_RECP :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 14);
            this.label3.TabIndex = 49;
            this.label3.Text = "PREV_IMG   :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 50;
            this.label1.Text = "CURR_RECP :";
            // 
            // LV_HISTORY
            // 
            this.LV_HISTORY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.LV_HISTORY.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.LV_HISTORY.ForeColor = System.Drawing.Color.White;
            this.LV_HISTORY.FullRowSelect = true;
            this.LV_HISTORY.GridLines = true;
            this.LV_HISTORY.Location = new System.Drawing.Point(632, 39);
            this.LV_HISTORY.MultiSelect = false;
            this.LV_HISTORY.Name = "LV_HISTORY";
            this.LV_HISTORY.Size = new System.Drawing.Size(578, 225);
            this.LV_HISTORY.TabIndex = 44;
            this.LV_HISTORY.UseCompatibleStateImageBehavior = false;
            this.LV_HISTORY.View = System.Windows.Forms.View.Details;
            this.LV_HISTORY.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_HISTORY_ColumnClick);
            this.LV_HISTORY.SelectedIndexChanged += new System.EventHandler(this.LV_HISTORY_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IDX";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "DATE";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 134;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DATA";
            this.columnHeader4.Width = 365;
            // 
            // RICH_HISTORY_MSG
            // 
            this.RICH_HISTORY_MSG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.RICH_HISTORY_MSG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RICH_HISTORY_MSG.ForeColor = System.Drawing.Color.White;
            this.RICH_HISTORY_MSG.Location = new System.Drawing.Point(632, 358);
            this.RICH_HISTORY_MSG.Name = "RICH_HISTORY_MSG";
            this.RICH_HISTORY_MSG.Size = new System.Drawing.Size(578, 155);
            this.RICH_HISTORY_MSG.TabIndex = 45;
            this.RICH_HISTORY_MSG.Text = "";
            // 
            // uc_view_history
            // 
            this.uc_view_history.AllowDrop = true;
            this.uc_view_history.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uc_view_history.BOOL_DRAW_FOCUS_ROI = true;
            this.uc_view_history.BOOL_DRAW_PTRN_ROI = true;
            this.uc_view_history.BOOL_TEACHING_ACTIVATION = false;
            this.uc_view_history.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_view_history.ForeColor = System.Drawing.Color.Lime;
            this.uc_view_history.Location = new System.Drawing.Point(13, 39);
            this.uc_view_history.Margin = new System.Windows.Forms.Padding(4);
            this.uc_view_history.Name = "uc_view_history";
            this.uc_view_history.PT_FIGURE_TO_DRAW = ((System.Drawing.PointF)(resources.GetObject("uc_view_history.PT_FIGURE_TO_DRAW")));
            this.uc_view_history.ROI_INDEX = -1;
            this.uc_view_history.Size = new System.Drawing.Size(600, 700);
            this.uc_view_history.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.uc_view_history);
            this.panel1.Controls.Add(this.BTN_CLOSE);
            this.panel1.Controls.Add(this.RICH_HISTORY_MSG);
            this.panel1.Controls.Add(this.LV_HISTORY);
            this.panel1.Controls.Add(this.BTN_UPDATE_HISTORY);
            this.panel1.Controls.Add(this.BTN_OPEN_HISTORY_FOLDER);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1217, 752);
            this.panel1.TabIndex = 58;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1152, 726);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 14);
            this.label7.TabIndex = 77;
            this.label7.Text = "CLOSE";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(1150, 333);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 14);
            this.label16.TabIndex = 75;
            this.label16.Text = "FOLDER";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(1084, 333);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 14);
            this.label17.TabIndex = 76;
            this.label17.Text = "UPDATE";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TXT_HISTORY_PREV_IMAGE);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TXT_HISTORY_CURR_RECP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TXT_HISTORY_PREV_RECP);
            this.groupBox1.Controls.Add(this.BTN_HISTORY_EXPERIMENT_SET);
            this.groupBox1.Location = new System.Drawing.Point(629, 533);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 124);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(495, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 14);
            this.label8.TabIndex = 77;
            this.label8.Text = "SET RECP";
            // 
            // BTN_HISTORY_EXPERIMENT_SET
            // 
            this.BTN_HISTORY_EXPERIMENT_SET.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.replace;
            this.BTN_HISTORY_EXPERIMENT_SET.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_HISTORY_EXPERIMENT_SET.Location = new System.Drawing.Point(500, 25);
            this.BTN_HISTORY_EXPERIMENT_SET.Name = "BTN_HISTORY_EXPERIMENT_SET";
            this.BTN_HISTORY_EXPERIMENT_SET.Size = new System.Drawing.Size(60, 60);
            this.BTN_HISTORY_EXPERIMENT_SET.TabIndex = 55;
            this.BTN_HISTORY_EXPERIMENT_SET.UseVisualStyleBackColor = true;
            this.BTN_HISTORY_EXPERIMENT_SET.Click += new System.EventHandler(this.BTN_HISTORY_EXPERIMENT_SET_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label6.Location = new System.Drawing.Point(15, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 14);
            this.label6.TabIndex = 71;
            this.label6.Text = ":- HISTORY VIEW";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label4.Location = new System.Drawing.Point(629, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 14);
            this.label4.TabIndex = 71;
            this.label4.Text = ":- PREVIOUS HISTORY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label5.Location = new System.Drawing.Point(629, 516);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 14);
            this.label5.TabIndex = 71;
            this.label5.Text = ":- RECIPE INFO";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label9.Location = new System.Drawing.Point(636, 336);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(164, 14);
            this.label9.TabIndex = 71;
            this.label9.Text = ":- INSPECTION RESULT";
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_CLOSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CLOSE.Location = new System.Drawing.Point(1147, 663);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(60, 60);
            this.BTN_CLOSE.TabIndex = 57;
            this.BTN_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_PTRN_APPLY_Click);
            // 
            // BTN_UPDATE_HISTORY
            // 
            this.BTN_UPDATE_HISTORY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_UPDATE_HISTORY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_UPDATE_HISTORY.Location = new System.Drawing.Point(1084, 270);
            this.BTN_UPDATE_HISTORY.Name = "BTN_UPDATE_HISTORY";
            this.BTN_UPDATE_HISTORY.Size = new System.Drawing.Size(60, 60);
            this.BTN_UPDATE_HISTORY.TabIndex = 47;
            this.BTN_UPDATE_HISTORY.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_HISTORY.Click += new System.EventHandler(this.BTN_UPDATE_HISTORY_Click);
            // 
            // BTN_OPEN_HISTORY_FOLDER
            // 
            this.BTN_OPEN_HISTORY_FOLDER.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.open_folder;
            this.BTN_OPEN_HISTORY_FOLDER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_OPEN_HISTORY_FOLDER.Location = new System.Drawing.Point(1150, 270);
            this.BTN_OPEN_HISTORY_FOLDER.Name = "BTN_OPEN_HISTORY_FOLDER";
            this.BTN_OPEN_HISTORY_FOLDER.Size = new System.Drawing.Size(60, 60);
            this.BTN_OPEN_HISTORY_FOLDER.TabIndex = 46;
            this.BTN_OPEN_HISTORY_FOLDER.UseVisualStyleBackColor = true;
            this.BTN_OPEN_HISTORY_FOLDER.Click += new System.EventHandler(this.BTN_OPEN_HISTORY_FOLDER_Click);
            // 
            // Dlg_HistoryM
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(1233, 767);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.Name = "Dlg_HistoryM";
            this.Text = "MEASUREMENT HISTORY";
            this.Load += new System.EventHandler(this.Dlg_History_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_HISTORY_EXPERIMENT_SET;
        private System.Windows.Forms.TextBox TXT_HISTORY_PREV_RECP;
        private System.Windows.Forms.TextBox TXT_HISTORY_PREV_IMAGE;
        private System.Windows.Forms.TextBox TXT_HISTORY_CURR_RECP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_OPEN_HISTORY_FOLDER;
        private System.Windows.Forms.Button BTN_UPDATE_HISTORY;
        private System.Windows.Forms.ListView LV_HISTORY;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.RichTextBox RICH_HISTORY_MSG;
        private CD_View.UC_CD_VIEWER uc_view_history;
        private System.Windows.Forms.Button BTN_CLOSE;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
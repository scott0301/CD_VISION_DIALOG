namespace CD_VISION_DIALOG
{
    partial class Dlg_HistoryP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dlg_HistoryP));
            this.uc_view_history = new CD_View.UC_CD_VIEWER();
            this.LV_HISTORY = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXT_PATH_HISTORY_MATCHING = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BTN_CLOSE = new System.Windows.Forms.Button();
            this.BTN_OPEN_HISTORY_FOLDER = new System.Windows.Forms.Button();
            this.BTN_UPDATE_HISTORY = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uc_view_history
            // 
            this.uc_view_history.AllowDrop = true;
            this.uc_view_history.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uc_view_history.BOOL_TEACHING_ACTIVATION = false;
            this.uc_view_history.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_view_history.ForeColor = System.Drawing.Color.Lime;
            this.uc_view_history.Location = new System.Drawing.Point(7, 35);
            this.uc_view_history.Margin = new System.Windows.Forms.Padding(4);
            this.uc_view_history.Name = "uc_view_history";
            this.uc_view_history.PT_FIGURE_TO_DRAW = ((System.Drawing.PointF)(resources.GetObject("uc_view_history.PT_FIGURE_TO_DRAW")));
            this.uc_view_history.ROI_INDEX = -1;
            this.uc_view_history.Size = new System.Drawing.Size(600, 700);
            this.uc_view_history.TabIndex = 57;
            // 
            // LV_HISTORY
            // 
            this.LV_HISTORY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.LV_HISTORY.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.LV_HISTORY.ForeColor = System.Drawing.Color.White;
            this.LV_HISTORY.FullRowSelect = true;
            this.LV_HISTORY.GridLines = true;
            this.LV_HISTORY.Location = new System.Drawing.Point(618, 33);
            this.LV_HISTORY.MultiSelect = false;
            this.LV_HISTORY.Name = "LV_HISTORY";
            this.LV_HISTORY.Size = new System.Drawing.Size(578, 559);
            this.LV_HISTORY.TabIndex = 58;
            this.LV_HISTORY.UseCompatibleStateImageBehavior = false;
            this.LV_HISTORY.View = System.Windows.Forms.View.Details;
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
            this.columnHeader4.Width = 488;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TXT_PATH_HISTORY_MATCHING);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.BTN_CLOSE);
            this.panel1.Controls.Add(this.uc_view_history);
            this.panel1.Controls.Add(this.BTN_OPEN_HISTORY_FOLDER);
            this.panel1.Controls.Add(this.BTN_UPDATE_HISTORY);
            this.panel1.Controls.Add(this.LV_HISTORY);
            this.panel1.Location = new System.Drawing.Point(7, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 739);
            this.panel1.TabIndex = 62;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // TXT_PATH_HISTORY_MATCHING
            // 
            this.TXT_PATH_HISTORY_MATCHING.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PATH_HISTORY_MATCHING.ForeColor = System.Drawing.Color.White;
            this.TXT_PATH_HISTORY_MATCHING.Location = new System.Drawing.Point(684, 598);
            this.TXT_PATH_HISTORY_MATCHING.Name = "TXT_PATH_HISTORY_MATCHING";
            this.TXT_PATH_HISTORY_MATCHING.Size = new System.Drawing.Size(506, 22);
            this.TXT_PATH_HISTORY_MATCHING.TabIndex = 80;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(626, 601);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 14);
            this.label10.TabIndex = 79;
            this.label10.Text = "PATH :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(684, 718);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 14);
            this.label3.TabIndex = 78;
            this.label3.Text = "FOLDER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(617, 716);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 78;
            this.label2.Text = "UPDATE";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(1135, 717);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 14);
            this.label17.TabIndex = 78;
            this.label17.Text = "CLOSE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(622, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 14);
            this.label1.TabIndex = 72;
            this.label1.Text = ":- PREVIOUSE HISTORY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label6.Location = new System.Drawing.Point(12, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 14);
            this.label6.TabIndex = 72;
            this.label6.Text = ":- HISTORY VIEW";
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_CLOSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CLOSE.Location = new System.Drawing.Point(1130, 653);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(60, 60);
            this.BTN_CLOSE.TabIndex = 61;
            this.BTN_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_CLOSE_Click);
            // 
            // BTN_OPEN_HISTORY_FOLDER
            // 
            this.BTN_OPEN_HISTORY_FOLDER.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.open_folder;
            this.BTN_OPEN_HISTORY_FOLDER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_OPEN_HISTORY_FOLDER.Location = new System.Drawing.Point(684, 653);
            this.BTN_OPEN_HISTORY_FOLDER.Name = "BTN_OPEN_HISTORY_FOLDER";
            this.BTN_OPEN_HISTORY_FOLDER.Size = new System.Drawing.Size(60, 60);
            this.BTN_OPEN_HISTORY_FOLDER.TabIndex = 59;
            this.BTN_OPEN_HISTORY_FOLDER.UseVisualStyleBackColor = true;
            this.BTN_OPEN_HISTORY_FOLDER.Click += new System.EventHandler(this.BTN_OPEN_HISTORY_FOLDER_Click);
            // 
            // BTN_UPDATE_HISTORY
            // 
            this.BTN_UPDATE_HISTORY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_UPDATE_HISTORY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_UPDATE_HISTORY.Location = new System.Drawing.Point(618, 653);
            this.BTN_UPDATE_HISTORY.Name = "BTN_UPDATE_HISTORY";
            this.BTN_UPDATE_HISTORY.Size = new System.Drawing.Size(60, 60);
            this.BTN_UPDATE_HISTORY.TabIndex = 60;
            this.BTN_UPDATE_HISTORY.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_HISTORY.Click += new System.EventHandler(this.BTN_UPDATE_HISTORY_Click);
            // 
            // Dlg_HistoryP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(1218, 765);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Dlg_HistoryP";
            this.Text = "PATTERN MATCHING FAILED - HISTORY";
            this.Load += new System.EventHandler(this.Dlg_HistoryP_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CD_View.UC_CD_VIEWER uc_view_history;
        private System.Windows.Forms.Button BTN_OPEN_HISTORY_FOLDER;
        private System.Windows.Forms.Button BTN_UPDATE_HISTORY;
        private System.Windows.Forms.ListView LV_HISTORY;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button BTN_CLOSE;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TXT_PATH_HISTORY_MATCHING;
        private System.Windows.Forms.Label label10;
    }
}
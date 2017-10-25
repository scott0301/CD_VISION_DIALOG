namespace CD_VISION_DIALOG
{
    partial class Dlg_Spc
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
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LV_HISTORY = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BTN_UPDATE_HISTORY = new System.Windows.Forms.Button();
            this.BTN_OPEN_HISTORY_FOLDER = new System.Windows.Forms.Button();
            this.dgview = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTN_CLOSE = new System.Windows.Forms.Button();
            this.BTN_CLEAR = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).BeginInit();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(1346, 604);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 16);
            this.label16.TabIndex = 83;
            this.label16.Text = "FOLDER";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(1274, 604);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 16);
            this.label17.TabIndex = 84;
            this.label17.Text = "UPDATE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label4.Location = new System.Drawing.Point(786, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 16);
            this.label4.TabIndex = 82;
            this.label4.Text = ":- PREVIOUS HISTORY";
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
            this.LV_HISTORY.Location = new System.Drawing.Point(981, 31);
            this.LV_HISTORY.Margin = new System.Windows.Forms.Padding(4);
            this.LV_HISTORY.MultiSelect = false;
            this.LV_HISTORY.Name = "LV_HISTORY";
            this.LV_HISTORY.Size = new System.Drawing.Size(492, 501);
            this.LV_HISTORY.TabIndex = 77;
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
            this.columnHeader5.Width = 156;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "TIME";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 238;
            // 
            // BTN_UPDATE_HISTORY
            // 
            this.BTN_UPDATE_HISTORY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_UPDATE_HISTORY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_UPDATE_HISTORY.Location = new System.Drawing.Point(1277, 540);
            this.BTN_UPDATE_HISTORY.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_UPDATE_HISTORY.Name = "BTN_UPDATE_HISTORY";
            this.BTN_UPDATE_HISTORY.Size = new System.Drawing.Size(60, 60);
            this.BTN_UPDATE_HISTORY.TabIndex = 79;
            this.BTN_UPDATE_HISTORY.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_HISTORY.Click += new System.EventHandler(this.BTN_UPDATE_HISTORY_Click);
            // 
            // BTN_OPEN_HISTORY_FOLDER
            // 
            this.BTN_OPEN_HISTORY_FOLDER.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.open_folder;
            this.BTN_OPEN_HISTORY_FOLDER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_OPEN_HISTORY_FOLDER.Location = new System.Drawing.Point(1345, 540);
            this.BTN_OPEN_HISTORY_FOLDER.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_OPEN_HISTORY_FOLDER.Name = "BTN_OPEN_HISTORY_FOLDER";
            this.BTN_OPEN_HISTORY_FOLDER.Size = new System.Drawing.Size(60, 60);
            this.BTN_OPEN_HISTORY_FOLDER.TabIndex = 78;
            this.BTN_OPEN_HISTORY_FOLDER.UseVisualStyleBackColor = true;
            this.BTN_OPEN_HISTORY_FOLDER.Click += new System.EventHandler(this.BTN_OPEN_HISTORY_FOLDER_Click);
            // 
            // dgview
            // 
            this.dgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgview.Location = new System.Drawing.Point(13, 31);
            this.dgview.Margin = new System.Windows.Forms.Padding(4);
            this.dgview.Name = "dgview";
            this.dgview.RowTemplate.Height = 23;
            this.dgview.Size = new System.Drawing.Size(960, 501);
            this.dgview.TabIndex = 85;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 82;
            this.label1.Text = ":- DATA VIEW";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1417, 604);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 87;
            this.label2.Text = "CLOSE";
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_CLOSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CLOSE.Location = new System.Drawing.Point(1412, 540);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(60, 60);
            this.BTN_CLOSE.TabIndex = 86;
            this.BTN_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_CLOSE_Click);
            // 
            // BTN_CLEAR
            // 
            this.BTN_CLEAR.Location = new System.Drawing.Point(13, 539);
            this.BTN_CLEAR.Name = "BTN_CLEAR";
            this.BTN_CLEAR.Size = new System.Drawing.Size(130, 35);
            this.BTN_CLEAR.TabIndex = 88;
            this.BTN_CLEAR.Text = "Clear";
            this.BTN_CLEAR.UseVisualStyleBackColor = true;
            this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
            // 
            // Dlg_Spc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(1486, 627);
            this.Controls.Add(this.BTN_CLEAR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTN_CLOSE);
            this.Controls.Add(this.dgview);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LV_HISTORY);
            this.Controls.Add(this.BTN_UPDATE_HISTORY);
            this.Controls.Add(this.BTN_OPEN_HISTORY_FOLDER);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dlg_Spc";
            this.Text = "Dlg_Spc";
            this.Load += new System.EventHandler(this.Dlg_Spc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView LV_HISTORY;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button BTN_UPDATE_HISTORY;
        private System.Windows.Forms.Button BTN_OPEN_HISTORY_FOLDER;
        private System.Windows.Forms.DataGridView dgview;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTN_CLOSE;
        private System.Windows.Forms.Button BTN_CLEAR;
    }
}
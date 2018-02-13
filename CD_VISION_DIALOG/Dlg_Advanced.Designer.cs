namespace CD_VISION_DIALOG
{
    partial class Dlg_Advanced
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
            this.CHK_SHOW_IMAGE_PROCESSING = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_APPLY = new System.Windows.Forms.Button();
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET = new System.Windows.Forms.CheckBox();
            this.CHK_USE_SAVE_MANUAL_GRAB = new System.Windows.Forms.CheckBox();
            this.CHK_USE_SAVE_FOCUS_REGION = new System.Windows.Forms.CheckBox();
            this.TXT_SAVE_IMAGE_SET_PATH = new System.Windows.Forms.TextBox();
            this.BTN_SET_SAVE_IMAGE_PATH = new System.Windows.Forms.Button();
            this.CHK_USE_SAVE_INPUT_IMAGE = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT = new System.Windows.Forms.CheckBox();
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CHK_SHOW_IMAGE_PROCESSING
            // 
            this.CHK_SHOW_IMAGE_PROCESSING.AutoSize = true;
            this.CHK_SHOW_IMAGE_PROCESSING.Location = new System.Drawing.Point(18, 271);
            this.CHK_SHOW_IMAGE_PROCESSING.Name = "CHK_SHOW_IMAGE_PROCESSING";
            this.CHK_SHOW_IMAGE_PROCESSING.Size = new System.Drawing.Size(222, 18);
            this.CHK_SHOW_IMAGE_PROCESSING.TabIndex = 0;
            this.CHK_SHOW_IMAGE_PROCESSING.Text = "SHOW_IMAGE_PROCESSING";
            this.CHK_SHOW_IMAGE_PROCESSING.UseVisualStyleBackColor = true;
            this.CHK_SHOW_IMAGE_PROCESSING.CheckedChanged += new System.EventHandler(this.CHK_SHOW_IMAGE_PROCESSING_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(577, 367);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 85;
            this.label1.Text = "APPLY";
            // 
            // BTN_APPLY
            // 
            this.BTN_APPLY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_ok;
            this.BTN_APPLY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_APPLY.Location = new System.Drawing.Point(569, 307);
            this.BTN_APPLY.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_APPLY.Name = "BTN_APPLY";
            this.BTN_APPLY.Size = new System.Drawing.Size(60, 60);
            this.BTN_APPLY.TabIndex = 84;
            this.BTN_APPLY.UseVisualStyleBackColor = true;
            this.BTN_APPLY.Click += new System.EventHandler(this.BTN_APPLY_Click);
            // 
            // CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET
            // 
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.AutoSize = true;
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.ForeColor = System.Drawing.Color.LimeGreen;
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.Location = new System.Drawing.Point(14, 100);
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.Name = "CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET";
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.Size = new System.Drawing.Size(328, 18);
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.TabIndex = 0;
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.Text = "BOOL_USE_SAVE_SEQUENTIAL_IMAGE_SET";
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.UseVisualStyleBackColor = true;
            this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET.CheckedChanged += new System.EventHandler(this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET_CheckedChanged);
            // 
            // CHK_USE_SAVE_MANUAL_GRAB
            // 
            this.CHK_USE_SAVE_MANUAL_GRAB.AutoSize = true;
            this.CHK_USE_SAVE_MANUAL_GRAB.ForeColor = System.Drawing.Color.LimeGreen;
            this.CHK_USE_SAVE_MANUAL_GRAB.Location = new System.Drawing.Point(14, 21);
            this.CHK_USE_SAVE_MANUAL_GRAB.Name = "CHK_USE_SAVE_MANUAL_GRAB";
            this.CHK_USE_SAVE_MANUAL_GRAB.Size = new System.Drawing.Size(257, 18);
            this.CHK_USE_SAVE_MANUAL_GRAB.TabIndex = 0;
            this.CHK_USE_SAVE_MANUAL_GRAB.Text = "BOOL_USE_SAVE_MANUAL_GRAB";
            this.CHK_USE_SAVE_MANUAL_GRAB.UseVisualStyleBackColor = true;
            this.CHK_USE_SAVE_MANUAL_GRAB.CheckedChanged += new System.EventHandler(this.CHK_USE_SAVE_MANUAL_GRAB_CheckedChanged);
            // 
            // CHK_USE_SAVE_FOCUS_REGION
            // 
            this.CHK_USE_SAVE_FOCUS_REGION.AutoSize = true;
            this.CHK_USE_SAVE_FOCUS_REGION.ForeColor = System.Drawing.Color.LimeGreen;
            this.CHK_USE_SAVE_FOCUS_REGION.Location = new System.Drawing.Point(14, 47);
            this.CHK_USE_SAVE_FOCUS_REGION.Name = "CHK_USE_SAVE_FOCUS_REGION";
            this.CHK_USE_SAVE_FOCUS_REGION.Size = new System.Drawing.Size(264, 18);
            this.CHK_USE_SAVE_FOCUS_REGION.TabIndex = 0;
            this.CHK_USE_SAVE_FOCUS_REGION.Text = "BOOL_USE_SAVE_FOCUS_REGION";
            this.CHK_USE_SAVE_FOCUS_REGION.UseVisualStyleBackColor = true;
            this.CHK_USE_SAVE_FOCUS_REGION.CheckedChanged += new System.EventHandler(this.CHK_USE_SAVE_FOCUS_REGION_CheckedChanged);
            // 
            // TXT_SAVE_IMAGE_SET_PATH
            // 
            this.TXT_SAVE_IMAGE_SET_PATH.Location = new System.Drawing.Point(33, 124);
            this.TXT_SAVE_IMAGE_SET_PATH.Name = "TXT_SAVE_IMAGE_SET_PATH";
            this.TXT_SAVE_IMAGE_SET_PATH.Size = new System.Drawing.Size(483, 22);
            this.TXT_SAVE_IMAGE_SET_PATH.TabIndex = 86;
            // 
            // BTN_SET_SAVE_IMAGE_PATH
            // 
            this.BTN_SET_SAVE_IMAGE_PATH.ForeColor = System.Drawing.Color.Black;
            this.BTN_SET_SAVE_IMAGE_PATH.Location = new System.Drawing.Point(519, 124);
            this.BTN_SET_SAVE_IMAGE_PATH.Name = "BTN_SET_SAVE_IMAGE_PATH";
            this.BTN_SET_SAVE_IMAGE_PATH.Size = new System.Drawing.Size(92, 23);
            this.BTN_SET_SAVE_IMAGE_PATH.TabIndex = 87;
            this.BTN_SET_SAVE_IMAGE_PATH.Text = "SET_PATH";
            this.BTN_SET_SAVE_IMAGE_PATH.UseVisualStyleBackColor = true;
            this.BTN_SET_SAVE_IMAGE_PATH.Click += new System.EventHandler(this.BTN_SET_SAVE_IMAGE_PATH_Click);
            // 
            // CHK_USE_SAVE_INPUT_IMAGE
            // 
            this.CHK_USE_SAVE_INPUT_IMAGE.AutoSize = true;
            this.CHK_USE_SAVE_INPUT_IMAGE.ForeColor = System.Drawing.Color.LimeGreen;
            this.CHK_USE_SAVE_INPUT_IMAGE.Location = new System.Drawing.Point(14, 76);
            this.CHK_USE_SAVE_INPUT_IMAGE.Name = "CHK_USE_SAVE_INPUT_IMAGE";
            this.CHK_USE_SAVE_INPUT_IMAGE.Size = new System.Drawing.Size(250, 18);
            this.CHK_USE_SAVE_INPUT_IMAGE.TabIndex = 0;
            this.CHK_USE_SAVE_INPUT_IMAGE.Text = "BOOL_USE_SAVE_INPUT_IMAGE";
            this.CHK_USE_SAVE_INPUT_IMAGE.UseVisualStyleBackColor = true;
            this.CHK_USE_SAVE_INPUT_IMAGE.CheckedChanged += new System.EventHandler(this.CHK_USE_SAVE_INPUT_IMAGE_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CHK_USE_SAVE_INPUT_IMAGE);
            this.groupBox1.Controls.Add(this.BTN_SET_SAVE_IMAGE_PATH);
            this.groupBox1.Controls.Add(this.CHK_USE_SAVE_MANUAL_GRAB);
            this.groupBox1.Controls.Add(this.TXT_SAVE_IMAGE_SET_PATH);
            this.groupBox1.Controls.Add(this.CHK_USE_SAVE_FOCUS_REGION);
            this.groupBox1.Controls.Add(this.CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET);
            this.groupBox1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 160);
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IMAGE SAVE OPTIONS";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CHK_USE_LEAVE_HISTORY_MEASUREMENT);
            this.groupBox2.Controls.Add(this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN);
            this.groupBox2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.groupBox2.Location = new System.Drawing.Point(12, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 80);
            this.groupBox2.TabIndex = 89;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HISTORY";
            // 
            // CHK_USE_LEAVE_HISTORY_MEASUREMENT
            // 
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.AutoSize = true;
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.ForeColor = System.Drawing.Color.LimeGreen;
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.Location = new System.Drawing.Point(6, 50);
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.Name = "CHK_USE_LEAVE_HISTORY_MEASUREMENT";
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.Size = new System.Drawing.Size(334, 18);
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.TabIndex = 0;
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.Text = "BOOL_USE_LEAVE_HISTORY_MEASUREMENT";
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.UseVisualStyleBackColor = true;
            this.CHK_USE_LEAVE_HISTORY_MEASUREMENT.CheckedChanged += new System.EventHandler(this.CHK_USE_LEAVE_HISTORY_MEASUREMENT_CheckedChanged);
            // 
            // CHK_USE_LEAVE_HISTORY_ERROR_PTRN
            // 
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.AutoSize = true;
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.ForeColor = System.Drawing.Color.LimeGreen;
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.Location = new System.Drawing.Point(6, 21);
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.Name = "CHK_USE_LEAVE_HISTORY_ERROR_PTRN";
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.Size = new System.Drawing.Size(324, 18);
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.TabIndex = 0;
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.Text = "BOOL_USE_LEAVE_HISTORY_ERROR_PTRN";
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.UseVisualStyleBackColor = true;
            this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN.CheckedChanged += new System.EventHandler(this.CHK_USE_LEAVE_HISTORY_ERROR_PTRN_CheckedChanged);
            // 
            // Dlg_Advanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(651, 394);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_APPLY);
            this.Controls.Add(this.CHK_SHOW_IMAGE_PROCESSING);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Name = "Dlg_Advanced";
            this.Text = "Advanced Experimental Options";
            this.Load += new System.EventHandler(this.Dlg_Hacker_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHK_SHOW_IMAGE_PROCESSING;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_APPLY;
        private System.Windows.Forms.CheckBox CHK_USE_SAVE_SEQUENTIAL_IMAGE_SET;
        private System.Windows.Forms.CheckBox CHK_USE_SAVE_MANUAL_GRAB;
        private System.Windows.Forms.CheckBox CHK_USE_SAVE_FOCUS_REGION;
        private System.Windows.Forms.TextBox TXT_SAVE_IMAGE_SET_PATH;
        private System.Windows.Forms.Button BTN_SET_SAVE_IMAGE_PATH;
        private System.Windows.Forms.CheckBox CHK_USE_SAVE_INPUT_IMAGE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox CHK_USE_LEAVE_HISTORY_ERROR_PTRN;
        private System.Windows.Forms.CheckBox CHK_USE_LEAVE_HISTORY_MEASUREMENT;
    }
}
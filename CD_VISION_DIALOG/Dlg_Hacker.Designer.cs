namespace CD_VISION_DIALOG
{
    partial class Dlg_Hacker
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
            this.button1 = new System.Windows.Forms.Button();
            this.CHK_OFF_DETECT_CROSS = new System.Windows.Forms.CheckBox();
            this.BTN_INIT_LOOP_COUNTER = new System.Windows.Forms.Button();
            this.TXT_LOOP_COUNTER = new System.Windows.Forms.TextBox();
            this.CHK_USE_LOOP_COUNTER = new System.Windows.Forms.CheckBox();
            this.CHK_USE_GRAB_SAVE = new System.Windows.Forms.CheckBox();
            this.CHK_USE_FOCUS_IMAGE_SAVE = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CHK_SHOW_IMAGE_PROCESSING
            // 
            this.CHK_SHOW_IMAGE_PROCESSING.AutoSize = true;
            this.CHK_SHOW_IMAGE_PROCESSING.Location = new System.Drawing.Point(23, 33);
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
            this.label1.Location = new System.Drawing.Point(833, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 85;
            this.label1.Text = "APPLY";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_ok;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(827, 274);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 60);
            this.button1.TabIndex = 84;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CHK_OFF_DETECT_CROSS
            // 
            this.CHK_OFF_DETECT_CROSS.AutoSize = true;
            this.CHK_OFF_DETECT_CROSS.Location = new System.Drawing.Point(23, 57);
            this.CHK_OFF_DETECT_CROSS.Name = "CHK_OFF_DETECT_CROSS";
            this.CHK_OFF_DETECT_CROSS.Size = new System.Drawing.Size(176, 18);
            this.CHK_OFF_DETECT_CROSS.TabIndex = 0;
            this.CHK_OFF_DETECT_CROSS.Text = "SHOW DETECT CROSS";
            this.CHK_OFF_DETECT_CROSS.UseVisualStyleBackColor = true;
            this.CHK_OFF_DETECT_CROSS.CheckedChanged += new System.EventHandler(this.CHK_OFF_DETECT_CROSS_CheckedChanged);
            // 
            // BTN_INIT_LOOP_COUNTER
            // 
            this.BTN_INIT_LOOP_COUNTER.Location = new System.Drawing.Point(44, 92);
            this.BTN_INIT_LOOP_COUNTER.Name = "BTN_INIT_LOOP_COUNTER";
            this.BTN_INIT_LOOP_COUNTER.Size = new System.Drawing.Size(167, 23);
            this.BTN_INIT_LOOP_COUNTER.TabIndex = 86;
            this.BTN_INIT_LOOP_COUNTER.Text = "INIT_LOOP_COUNTER";
            this.BTN_INIT_LOOP_COUNTER.UseVisualStyleBackColor = true;
            this.BTN_INIT_LOOP_COUNTER.Click += new System.EventHandler(this.BTN_INIT_LOOP_COUNTER_Click);
            // 
            // TXT_LOOP_COUNTER
            // 
            this.TXT_LOOP_COUNTER.Location = new System.Drawing.Point(217, 94);
            this.TXT_LOOP_COUNTER.Name = "TXT_LOOP_COUNTER";
            this.TXT_LOOP_COUNTER.Size = new System.Drawing.Size(76, 22);
            this.TXT_LOOP_COUNTER.TabIndex = 87;
            this.TXT_LOOP_COUNTER.Text = "0";
            // 
            // CHK_USE_LOOP_COUNTER
            // 
            this.CHK_USE_LOOP_COUNTER.AutoSize = true;
            this.CHK_USE_LOOP_COUNTER.Location = new System.Drawing.Point(23, 97);
            this.CHK_USE_LOOP_COUNTER.Name = "CHK_USE_LOOP_COUNTER";
            this.CHK_USE_LOOP_COUNTER.Size = new System.Drawing.Size(15, 14);
            this.CHK_USE_LOOP_COUNTER.TabIndex = 0;
            this.CHK_USE_LOOP_COUNTER.UseVisualStyleBackColor = true;
            this.CHK_USE_LOOP_COUNTER.CheckedChanged += new System.EventHandler(this.CHK_USE_LOOP_COUNTER_CheckedChanged);
            // 
            // CHK_USE_GRAB_SAVE
            // 
            this.CHK_USE_GRAB_SAVE.AutoSize = true;
            this.CHK_USE_GRAB_SAVE.Location = new System.Drawing.Point(23, 132);
            this.CHK_USE_GRAB_SAVE.Name = "CHK_USE_GRAB_SAVE";
            this.CHK_USE_GRAB_SAVE.Size = new System.Drawing.Size(143, 18);
            this.CHK_USE_GRAB_SAVE.TabIndex = 0;
            this.CHK_USE_GRAB_SAVE.Text = "USE_GRAB_SAVE";
            this.CHK_USE_GRAB_SAVE.UseVisualStyleBackColor = true;
            this.CHK_USE_GRAB_SAVE.CheckedChanged += new System.EventHandler(this.CHK_USE_GRAB_SAVE_CheckedChanged);
            // 
            // CHK_USE_FOCUS_IMAGE_SAVE
            // 
            this.CHK_USE_FOCUS_IMAGE_SAVE.AutoSize = true;
            this.CHK_USE_FOCUS_IMAGE_SAVE.Location = new System.Drawing.Point(23, 156);
            this.CHK_USE_FOCUS_IMAGE_SAVE.Name = "CHK_USE_FOCUS_IMAGE_SAVE";
            this.CHK_USE_FOCUS_IMAGE_SAVE.Size = new System.Drawing.Size(206, 18);
            this.CHK_USE_FOCUS_IMAGE_SAVE.TabIndex = 0;
            this.CHK_USE_FOCUS_IMAGE_SAVE.Text = "USE_FOCUS_IMAGE_SAVE";
            this.CHK_USE_FOCUS_IMAGE_SAVE.UseVisualStyleBackColor = true;
            this.CHK_USE_FOCUS_IMAGE_SAVE.CheckedChanged += new System.EventHandler(this.CHK_USE_FOCUS_IMAGE_CheckedChanged);
            // 
            // Dlg_Hacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(900, 366);
            this.Controls.Add(this.TXT_LOOP_COUNTER);
            this.Controls.Add(this.BTN_INIT_LOOP_COUNTER);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CHK_USE_LOOP_COUNTER);
            this.Controls.Add(this.CHK_USE_FOCUS_IMAGE_SAVE);
            this.Controls.Add(this.CHK_USE_GRAB_SAVE);
            this.Controls.Add(this.CHK_OFF_DETECT_CROSS);
            this.Controls.Add(this.CHK_SHOW_IMAGE_PROCESSING);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Name = "Dlg_Hacker";
            this.Text = "DLG_HACKER";
            this.Load += new System.EventHandler(this.Dlg_Hacker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHK_SHOW_IMAGE_PROCESSING;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox CHK_OFF_DETECT_CROSS;
        private System.Windows.Forms.Button BTN_INIT_LOOP_COUNTER;
        private System.Windows.Forms.TextBox TXT_LOOP_COUNTER;
        private System.Windows.Forms.CheckBox CHK_USE_LOOP_COUNTER;
        private System.Windows.Forms.CheckBox CHK_USE_GRAB_SAVE;
        private System.Windows.Forms.CheckBox CHK_USE_FOCUS_IMAGE_SAVE;
    }
}
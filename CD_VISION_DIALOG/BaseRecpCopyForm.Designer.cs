namespace CD_VISION_DIALOG
{
    partial class FormBaseRecpCopy
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
            this.label1 = new System.Windows.Forms.Label();
            this.TXT_BASE_FILE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TXT_INHERITED_TARGET = new System.Windows.Forms.TextBox();
            this.BTN_GENERATE = new System.Windows.Forms.Button();
            this.BTN_CLOSE = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "BASE SOURCE :";
            // 
            // TXT_BASE_FILE
            // 
            this.TXT_BASE_FILE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_BASE_FILE.ForeColor = System.Drawing.Color.White;
            this.TXT_BASE_FILE.Location = new System.Drawing.Point(195, 16);
            this.TXT_BASE_FILE.Name = "TXT_BASE_FILE";
            this.TXT_BASE_FILE.Size = new System.Drawing.Size(325, 24);
            this.TXT_BASE_FILE.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "INHERITED TARGET :";
            // 
            // TXT_INHERITED_TARGET
            // 
            this.TXT_INHERITED_TARGET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_INHERITED_TARGET.ForeColor = System.Drawing.Color.White;
            this.TXT_INHERITED_TARGET.Location = new System.Drawing.Point(195, 52);
            this.TXT_INHERITED_TARGET.Name = "TXT_INHERITED_TARGET";
            this.TXT_INHERITED_TARGET.Size = new System.Drawing.Size(325, 24);
            this.TXT_INHERITED_TARGET.TabIndex = 1;
            // 
            // BTN_GENERATE
            // 
            this.BTN_GENERATE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.copy_1;
            this.BTN_GENERATE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_GENERATE.Location = new System.Drawing.Point(535, 6);
            this.BTN_GENERATE.Name = "BTN_GENERATE";
            this.BTN_GENERATE.Size = new System.Drawing.Size(60, 60);
            this.BTN_GENERATE.TabIndex = 2;
            this.BTN_GENERATE.UseVisualStyleBackColor = true;
            this.BTN_GENERATE.Click += new System.EventHandler(this.BTN_GENERATE_Click);
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_CLOSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CLOSE.Location = new System.Drawing.Point(601, 6);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(60, 60);
            this.BTN_CLOSE.TabIndex = 2;
            this.BTN_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_CLOSE_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(528, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "GENERATE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(606, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "CLOSE";
            // 
            // FormBaseRecpCopy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(674, 95);
            this.Controls.Add(this.BTN_CLOSE);
            this.Controls.Add(this.BTN_GENERATE);
            this.Controls.Add(this.TXT_INHERITED_TARGET);
            this.Controls.Add(this.TXT_BASE_FILE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.Name = "FormBaseRecpCopy";
            this.Text = "RECIPE COPY";
            this.Load += new System.EventHandler(this.BaseRecpCopyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXT_BASE_FILE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TXT_INHERITED_TARGET;
        private System.Windows.Forms.Button BTN_GENERATE;
        private System.Windows.Forms.Button BTN_CLOSE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
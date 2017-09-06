namespace CD_View
{
    partial class FormPopup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopup));
            this.CHK_DRAW_OVERLAY = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FORM_HEADER = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.BTN_CLOSE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TXT_MOUSE_POS_X = new System.Windows.Forms.TextBox();
            this.TXT_MOUSE_POS_Y = new System.Windows.Forms.TextBox();
            this.CHK_TARGET_SELECTED = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RDO_SELECTED_TYPE_RECT = new System.Windows.Forms.RadioButton();
            this.RDO_SELECTED_TYPE_CIRCLE = new System.Windows.Forms.RadioButton();
            this.RDO_SELECTED_TYPE_ROI = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.TXT_ACTIVATED_FIGURE_INDEX = new System.Windows.Forms.TextBox();
            this.FORM_HEADER.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CHK_DRAW_OVERLAY
            // 
            this.CHK_DRAW_OVERLAY.AutoSize = true;
            this.CHK_DRAW_OVERLAY.Location = new System.Drawing.Point(240, 41);
            this.CHK_DRAW_OVERLAY.Name = "CHK_DRAW_OVERLAY";
            this.CHK_DRAW_OVERLAY.Size = new System.Drawing.Size(79, 18);
            this.CHK_DRAW_OVERLAY.TabIndex = 0;
            this.CHK_DRAW_OVERLAY.Text = "Overlay";
            this.CHK_DRAW_OVERLAY.UseVisualStyleBackColor = true;
            this.CHK_DRAW_OVERLAY.CheckedChanged += new System.EventHandler(this.CHK_DRAW_OVERLAY_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "measureHor.png");
            this.imageList1.Images.SetKeyName(1, "measureVer.png");
            this.imageList1.Images.SetKeyName(2, "measureDig.png");
            this.imageList1.Images.SetKeyName(3, "measureCir.png");
            this.imageList1.Images.SetKeyName(4, "measureRec.png");
            // 
            // FORM_HEADER
            // 
            this.FORM_HEADER.BackColor = System.Drawing.Color.White;
            this.FORM_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FORM_HEADER.Controls.Add(this.label11);
            this.FORM_HEADER.Controls.Add(this.BTN_CLOSE);
            this.FORM_HEADER.ForeColor = System.Drawing.Color.Black;
            this.FORM_HEADER.Location = new System.Drawing.Point(0, 0);
            this.FORM_HEADER.Name = "FORM_HEADER";
            this.FORM_HEADER.Size = new System.Drawing.Size(323, 35);
            this.FORM_HEADER.TabIndex = 15;
            this.FORM_HEADER.Paint += new System.Windows.Forms.PaintEventHandler(this.FORM_HEADER_Paint);
            this.FORM_HEADER.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FORM_HEADER_MouseDown);
            this.FORM_HEADER.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FORM_HEADER_MouseMove);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(11, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "Teaching Window";
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CLOSE.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.BTN_CLOSE.FlatAppearance.BorderSize = 0;
            this.BTN_CLOSE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BTN_CLOSE.ForeColor = System.Drawing.Color.Black;
            this.BTN_CLOSE.Location = new System.Drawing.Point(293, 3);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(25, 25);
            this.BTN_CLOSE.TabIndex = 1;
            this.BTN_CLOSE.Text = "X";
            this.BTN_CLOSE.UseVisualStyleBackColor = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_TEACHING_FORM_CLOSE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "MOUSE_POS :";
            // 
            // TXT_MOUSE_POS_X
            // 
            this.TXT_MOUSE_POS_X.Location = new System.Drawing.Point(121, 68);
            this.TXT_MOUSE_POS_X.Name = "TXT_MOUSE_POS_X";
            this.TXT_MOUSE_POS_X.Size = new System.Drawing.Size(55, 22);
            this.TXT_MOUSE_POS_X.TabIndex = 17;
            this.TXT_MOUSE_POS_X.Text = "0";
            // 
            // TXT_MOUSE_POS_Y
            // 
            this.TXT_MOUSE_POS_Y.Location = new System.Drawing.Point(182, 68);
            this.TXT_MOUSE_POS_Y.Name = "TXT_MOUSE_POS_Y";
            this.TXT_MOUSE_POS_Y.Size = new System.Drawing.Size(55, 22);
            this.TXT_MOUSE_POS_Y.TabIndex = 17;
            this.TXT_MOUSE_POS_Y.Text = "0";
            // 
            // CHK_TARGET_SELECTED
            // 
            this.CHK_TARGET_SELECTED.AutoSize = true;
            this.CHK_TARGET_SELECTED.Enabled = false;
            this.CHK_TARGET_SELECTED.Location = new System.Drawing.Point(12, 21);
            this.CHK_TARGET_SELECTED.Name = "CHK_TARGET_SELECTED";
            this.CHK_TARGET_SELECTED.Size = new System.Drawing.Size(151, 18);
            this.CHK_TARGET_SELECTED.TabIndex = 18;
            this.CHK_TARGET_SELECTED.Text = "ACTVATED FIGURE";
            this.CHK_TARGET_SELECTED.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RDO_SELECTED_TYPE_ROI);
            this.groupBox1.Controls.Add(this.RDO_SELECTED_TYPE_CIRCLE);
            this.groupBox1.Controls.Add(this.TXT_ACTIVATED_FIGURE_INDEX);
            this.groupBox1.Controls.Add(this.RDO_SELECTED_TYPE_RECT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CHK_TARGET_SELECTED);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(3, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 103);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // RDO_SELECTED_TYPE_RECT
            // 
            this.RDO_SELECTED_TYPE_RECT.AutoSize = true;
            this.RDO_SELECTED_TYPE_RECT.Enabled = false;
            this.RDO_SELECTED_TYPE_RECT.Location = new System.Drawing.Point(12, 45);
            this.RDO_SELECTED_TYPE_RECT.Name = "RDO_SELECTED_TYPE_RECT";
            this.RDO_SELECTED_TYPE_RECT.Size = new System.Drawing.Size(59, 18);
            this.RDO_SELECTED_TYPE_RECT.TabIndex = 19;
            this.RDO_SELECTED_TYPE_RECT.TabStop = true;
            this.RDO_SELECTED_TYPE_RECT.Text = "RECT";
            this.RDO_SELECTED_TYPE_RECT.UseVisualStyleBackColor = true;
            // 
            // RDO_SELECTED_TYPE_CIRCLE
            // 
            this.RDO_SELECTED_TYPE_CIRCLE.AutoSize = true;
            this.RDO_SELECTED_TYPE_CIRCLE.Enabled = false;
            this.RDO_SELECTED_TYPE_CIRCLE.Location = new System.Drawing.Point(74, 45);
            this.RDO_SELECTED_TYPE_CIRCLE.Name = "RDO_SELECTED_TYPE_CIRCLE";
            this.RDO_SELECTED_TYPE_CIRCLE.Size = new System.Drawing.Size(74, 18);
            this.RDO_SELECTED_TYPE_CIRCLE.TabIndex = 19;
            this.RDO_SELECTED_TYPE_CIRCLE.TabStop = true;
            this.RDO_SELECTED_TYPE_CIRCLE.Text = "CIRCLE";
            this.RDO_SELECTED_TYPE_CIRCLE.UseVisualStyleBackColor = true;
            // 
            // RDO_SELECTED_TYPE_ROI
            // 
            this.RDO_SELECTED_TYPE_ROI.AutoSize = true;
            this.RDO_SELECTED_TYPE_ROI.Enabled = false;
            this.RDO_SELECTED_TYPE_ROI.Location = new System.Drawing.Point(154, 45);
            this.RDO_SELECTED_TYPE_ROI.Name = "RDO_SELECTED_TYPE_ROI";
            this.RDO_SELECTED_TYPE_ROI.Size = new System.Drawing.Size(51, 18);
            this.RDO_SELECTED_TYPE_ROI.TabIndex = 19;
            this.RDO_SELECTED_TYPE_ROI.TabStop = true;
            this.RDO_SELECTED_TYPE_ROI.Text = "ROI";
            this.RDO_SELECTED_TYPE_ROI.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "FIGURE INDEX :";
            // 
            // TXT_ACTIVATED_FIGURE_INDEX
            // 
            this.TXT_ACTIVATED_FIGURE_INDEX.Enabled = false;
            this.TXT_ACTIVATED_FIGURE_INDEX.Location = new System.Drawing.Point(129, 68);
            this.TXT_ACTIVATED_FIGURE_INDEX.Name = "TXT_ACTIVATED_FIGURE_INDEX";
            this.TXT_ACTIVATED_FIGURE_INDEX.Size = new System.Drawing.Size(76, 22);
            this.TXT_ACTIVATED_FIGURE_INDEX.TabIndex = 17;
            this.TXT_ACTIVATED_FIGURE_INDEX.Text = "0";
            // 
            // FormPopup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(324, 418);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TXT_MOUSE_POS_Y);
            this.Controls.Add(this.TXT_MOUSE_POS_X);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CHK_DRAW_OVERLAY);
            this.Controls.Add(this.FORM_HEADER);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.LimeGreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPopup";
            this.ShowIcon = false;
            this.Text = "Popup";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormPopup_Load);
            this.FORM_HEADER.ResumeLayout(false);
            this.FORM_HEADER.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHK_DRAW_OVERLAY;
        private System.Windows.Forms.Button BTN_CLOSE;
        private System.Windows.Forms.Panel FORM_HEADER;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXT_MOUSE_POS_X;
        private System.Windows.Forms.TextBox TXT_MOUSE_POS_Y;
        private System.Windows.Forms.CheckBox CHK_TARGET_SELECTED;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RDO_SELECTED_TYPE_ROI;
        private System.Windows.Forms.RadioButton RDO_SELECTED_TYPE_CIRCLE;
        private System.Windows.Forms.TextBox TXT_ACTIVATED_FIGURE_INDEX;
        private System.Windows.Forms.RadioButton RDO_SELECTED_TYPE_RECT;
        private System.Windows.Forms.Label label2;
    }
}
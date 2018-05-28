namespace CD_VISION_DIALOG
{
    partial class Dlg_Params
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CHK_FOCUS_IAF = new System.Windows.Forms.CheckBox();
            this.CHK_FOCUS_LAF = new System.Windows.Forms.CheckBox();
            this.CHK_FOCUS_NONE = new System.Windows.Forms.CheckBox();
            this.CHK_FOCUS_ZAF = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RDO_LIGHT_DF = new System.Windows.Forms.RadioButton();
            this.RDO_LIGHT_BF = new System.Windows.Forms.RadioButton();
            this.RDO_LIGHT_ALIGN = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RDO_CAM_50X = new System.Windows.Forms.RadioButton();
            this.RDO_CAM_25X = new System.Windows.Forms.RadioButton();
            this.RDO_CAM_ALIGN = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TXT_PIXEL_RES = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXT_LIGHT_VALUE = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BTN_RECP_PARAM_CANCEL = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.BTN_RECP_PARAM_APPLY = new System.Windows.Forms.Button();
            this.TXT_CAM_EXP = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CHK_USE_CENTERING = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.RDO_MULTI_FRAME_MEDIAN = new System.Windows.Forms.RadioButton();
            this.RDO_MULTI_FRAME_VALUE_AVG = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.TXT_MULTI_FRAME_SHOT_COUNT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TXT_MULTI_FRAME_SHOT_DELAY = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CHK_FOCUS_IAF);
            this.groupBox3.Controls.Add(this.CHK_FOCUS_LAF);
            this.groupBox3.Controls.Add(this.CHK_FOCUS_NONE);
            this.groupBox3.Controls.Add(this.CHK_FOCUS_ZAF);
            this.groupBox3.ForeColor = System.Drawing.Color.Coral;
            this.groupBox3.Location = new System.Drawing.Point(7, 212);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(521, 57);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FOCUS TYPE";
            // 
            // CHK_FOCUS_IAF
            // 
            this.CHK_FOCUS_IAF.AutoSize = true;
            this.CHK_FOCUS_IAF.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_IAF.Location = new System.Drawing.Point(288, 26);
            this.CHK_FOCUS_IAF.Name = "CHK_FOCUS_IAF";
            this.CHK_FOCUS_IAF.Size = new System.Drawing.Size(58, 21);
            this.CHK_FOCUS_IAF.TabIndex = 1;
            this.CHK_FOCUS_IAF.Text = "IAF";
            this.CHK_FOCUS_IAF.UseVisualStyleBackColor = true;
            this.CHK_FOCUS_IAF.CheckedChanged += new System.EventHandler(this.CHK_FOCUS_IAF_CheckedChanged);
            // 
            // CHK_FOCUS_LAF
            // 
            this.CHK_FOCUS_LAF.AutoSize = true;
            this.CHK_FOCUS_LAF.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_LAF.Location = new System.Drawing.Point(204, 25);
            this.CHK_FOCUS_LAF.Name = "CHK_FOCUS_LAF";
            this.CHK_FOCUS_LAF.Size = new System.Drawing.Size(59, 21);
            this.CHK_FOCUS_LAF.TabIndex = 1;
            this.CHK_FOCUS_LAF.Text = "LAF";
            this.CHK_FOCUS_LAF.UseVisualStyleBackColor = true;
            this.CHK_FOCUS_LAF.CheckedChanged += new System.EventHandler(this.CHK_FOCUS_LAF_CheckedChanged);
            // 
            // CHK_FOCUS_NONE
            // 
            this.CHK_FOCUS_NONE.AutoSize = true;
            this.CHK_FOCUS_NONE.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_NONE.Location = new System.Drawing.Point(6, 25);
            this.CHK_FOCUS_NONE.Name = "CHK_FOCUS_NONE";
            this.CHK_FOCUS_NONE.Size = new System.Drawing.Size(74, 21);
            this.CHK_FOCUS_NONE.TabIndex = 1;
            this.CHK_FOCUS_NONE.Text = "NONE";
            this.CHK_FOCUS_NONE.UseVisualStyleBackColor = true;
            this.CHK_FOCUS_NONE.CheckedChanged += new System.EventHandler(this.CHK_FOCUS_NONE_CheckedChanged);
            // 
            // CHK_FOCUS_ZAF
            // 
            this.CHK_FOCUS_ZAF.AutoSize = true;
            this.CHK_FOCUS_ZAF.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_ZAF.Location = new System.Drawing.Point(116, 25);
            this.CHK_FOCUS_ZAF.Name = "CHK_FOCUS_ZAF";
            this.CHK_FOCUS_ZAF.Size = new System.Drawing.Size(60, 21);
            this.CHK_FOCUS_ZAF.TabIndex = 1;
            this.CHK_FOCUS_ZAF.Text = "ZAF";
            this.CHK_FOCUS_ZAF.UseVisualStyleBackColor = true;
            this.CHK_FOCUS_ZAF.CheckedChanged += new System.EventHandler(this.CHK_FOCUS_ZAF_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RDO_LIGHT_DF);
            this.groupBox2.Controls.Add(this.RDO_LIGHT_BF);
            this.groupBox2.Controls.Add(this.RDO_LIGHT_ALIGN);
            this.groupBox2.ForeColor = System.Drawing.Color.Coral;
            this.groupBox2.Location = new System.Drawing.Point(7, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 57);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LIGHT";
            // 
            // RDO_LIGHT_DF
            // 
            this.RDO_LIGHT_DF.AutoSize = true;
            this.RDO_LIGHT_DF.ForeColor = System.Drawing.Color.White;
            this.RDO_LIGHT_DF.Location = new System.Drawing.Point(203, 21);
            this.RDO_LIGHT_DF.Name = "RDO_LIGHT_DF";
            this.RDO_LIGHT_DF.Size = new System.Drawing.Size(49, 21);
            this.RDO_LIGHT_DF.TabIndex = 0;
            this.RDO_LIGHT_DF.TabStop = true;
            this.RDO_LIGHT_DF.Text = "DF";
            this.RDO_LIGHT_DF.UseVisualStyleBackColor = true;
            // 
            // RDO_LIGHT_BF
            // 
            this.RDO_LIGHT_BF.AutoSize = true;
            this.RDO_LIGHT_BF.ForeColor = System.Drawing.Color.White;
            this.RDO_LIGHT_BF.Location = new System.Drawing.Point(113, 23);
            this.RDO_LIGHT_BF.Name = "RDO_LIGHT_BF";
            this.RDO_LIGHT_BF.Size = new System.Drawing.Size(49, 21);
            this.RDO_LIGHT_BF.TabIndex = 0;
            this.RDO_LIGHT_BF.TabStop = true;
            this.RDO_LIGHT_BF.Text = "BF";
            this.RDO_LIGHT_BF.UseVisualStyleBackColor = true;
            // 
            // RDO_LIGHT_ALIGN
            // 
            this.RDO_LIGHT_ALIGN.AutoSize = true;
            this.RDO_LIGHT_ALIGN.ForeColor = System.Drawing.Color.White;
            this.RDO_LIGHT_ALIGN.Location = new System.Drawing.Point(6, 23);
            this.RDO_LIGHT_ALIGN.Name = "RDO_LIGHT_ALIGN";
            this.RDO_LIGHT_ALIGN.Size = new System.Drawing.Size(79, 21);
            this.RDO_LIGHT_ALIGN.TabIndex = 0;
            this.RDO_LIGHT_ALIGN.TabStop = true;
            this.RDO_LIGHT_ALIGN.Text = "ALIGN";
            this.RDO_LIGHT_ALIGN.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RDO_CAM_50X);
            this.groupBox1.Controls.Add(this.RDO_CAM_25X);
            this.groupBox1.Controls.Add(this.RDO_CAM_ALIGN);
            this.groupBox1.ForeColor = System.Drawing.Color.Coral;
            this.groupBox1.Location = new System.Drawing.Point(7, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 57);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAM";
            // 
            // RDO_CAM_50X
            // 
            this.RDO_CAM_50X.AutoSize = true;
            this.RDO_CAM_50X.ForeColor = System.Drawing.Color.White;
            this.RDO_CAM_50X.Location = new System.Drawing.Point(205, 22);
            this.RDO_CAM_50X.Name = "RDO_CAM_50X";
            this.RDO_CAM_50X.Size = new System.Drawing.Size(60, 21);
            this.RDO_CAM_50X.TabIndex = 0;
            this.RDO_CAM_50X.TabStop = true;
            this.RDO_CAM_50X.Text = "50X";
            this.RDO_CAM_50X.UseVisualStyleBackColor = true;
            // 
            // RDO_CAM_25X
            // 
            this.RDO_CAM_25X.AutoSize = true;
            this.RDO_CAM_25X.ForeColor = System.Drawing.Color.White;
            this.RDO_CAM_25X.Location = new System.Drawing.Point(113, 23);
            this.RDO_CAM_25X.Name = "RDO_CAM_25X";
            this.RDO_CAM_25X.Size = new System.Drawing.Size(60, 21);
            this.RDO_CAM_25X.TabIndex = 0;
            this.RDO_CAM_25X.TabStop = true;
            this.RDO_CAM_25X.Text = "25X";
            this.RDO_CAM_25X.UseVisualStyleBackColor = true;
            // 
            // RDO_CAM_ALIGN
            // 
            this.RDO_CAM_ALIGN.AutoSize = true;
            this.RDO_CAM_ALIGN.ForeColor = System.Drawing.Color.White;
            this.RDO_CAM_ALIGN.Location = new System.Drawing.Point(6, 23);
            this.RDO_CAM_ALIGN.Name = "RDO_CAM_ALIGN";
            this.RDO_CAM_ALIGN.Size = new System.Drawing.Size(79, 21);
            this.RDO_CAM_ALIGN.TabIndex = 0;
            this.RDO_CAM_ALIGN.TabStop = true;
            this.RDO_CAM_ALIGN.Text = "ALIGN";
            this.RDO_CAM_ALIGN.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(192, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 45;
            this.label1.Text = "(mm)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Coral;
            this.label9.Location = new System.Drawing.Point(20, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 17);
            this.label9.TabIndex = 44;
            this.label9.Text = "PXL RES";
            // 
            // TXT_PIXEL_RES
            // 
            this.TXT_PIXEL_RES.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PIXEL_RES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PIXEL_RES.ForeColor = System.Drawing.Color.White;
            this.TXT_PIXEL_RES.Location = new System.Drawing.Point(104, 20);
            this.TXT_PIXEL_RES.Name = "TXT_PIXEL_RES";
            this.TXT_PIXEL_RES.Size = new System.Drawing.Size(82, 24);
            this.TXT_PIXEL_RES.TabIndex = 43;
            this.TXT_PIXEL_RES.Text = "0.0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(192, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 14);
            this.label18.TabIndex = 48;
            this.label18.Text = "(val)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Coral;
            this.label8.Location = new System.Drawing.Point(21, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 17);
            this.label8.TabIndex = 47;
            this.label8.Text = "L-VALUE";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.BTN_RECP_PARAM_CANCEL);
            this.panel1.Controls.Add(this.BTN_RECP_PARAM_APPLY);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 429);
            this.panel1.TabIndex = 49;
            // 
            // TXT_LIGHT_VALUE
            // 
            this.TXT_LIGHT_VALUE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_LIGHT_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_LIGHT_VALUE.ForeColor = System.Drawing.Color.White;
            this.TXT_LIGHT_VALUE.Location = new System.Drawing.Point(105, 61);
            this.TXT_LIGHT_VALUE.Name = "TXT_LIGHT_VALUE";
            this.TXT_LIGHT_VALUE.Size = new System.Drawing.Size(81, 24);
            this.TXT_LIGHT_VALUE.TabIndex = 1;
            this.TXT_LIGHT_VALUE.Text = "0";
            this.TXT_LIGHT_VALUE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(473, 404);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 14);
            this.label16.TabIndex = 75;
            this.label16.Text = "APPLY";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(405, 404);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 14);
            this.label17.TabIndex = 76;
            this.label17.Text = "CANCEL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(193, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 14);
            this.label6.TabIndex = 48;
            this.label6.Text = "(val)";
            // 
            // BTN_RECP_PARAM_CANCEL
            // 
            this.BTN_RECP_PARAM_CANCEL.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_RECP_PARAM_CANCEL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECP_PARAM_CANCEL.Location = new System.Drawing.Point(408, 340);
            this.BTN_RECP_PARAM_CANCEL.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_RECP_PARAM_CANCEL.Name = "BTN_RECP_PARAM_CANCEL";
            this.BTN_RECP_PARAM_CANCEL.Size = new System.Drawing.Size(55, 60);
            this.BTN_RECP_PARAM_CANCEL.TabIndex = 35;
            this.BTN_RECP_PARAM_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_RECP_PARAM_CANCEL.Click += new System.EventHandler(this.BTN_RECP_PARAM_CANCEL_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Coral;
            this.label5.Location = new System.Drawing.Point(21, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 47;
            this.label5.Text = "EXP";
            // 
            // BTN_RECP_PARAM_APPLY
            // 
            this.BTN_RECP_PARAM_APPLY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_ok;
            this.BTN_RECP_PARAM_APPLY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECP_PARAM_APPLY.Location = new System.Drawing.Point(473, 340);
            this.BTN_RECP_PARAM_APPLY.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_RECP_PARAM_APPLY.Name = "BTN_RECP_PARAM_APPLY";
            this.BTN_RECP_PARAM_APPLY.Size = new System.Drawing.Size(55, 60);
            this.BTN_RECP_PARAM_APPLY.TabIndex = 34;
            this.BTN_RECP_PARAM_APPLY.UseVisualStyleBackColor = true;
            this.BTN_RECP_PARAM_APPLY.Click += new System.EventHandler(this.BTN_RECP_PARAM_APPLY_Click);
            // 
            // TXT_CAM_EXP
            // 
            this.TXT_CAM_EXP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_CAM_EXP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_CAM_EXP.ForeColor = System.Drawing.Color.White;
            this.TXT_CAM_EXP.Location = new System.Drawing.Point(105, 98);
            this.TXT_CAM_EXP.Name = "TXT_CAM_EXP";
            this.TXT_CAM_EXP.Size = new System.Drawing.Size(82, 24);
            this.TXT_CAM_EXP.TabIndex = 46;
            this.TXT_CAM_EXP.Text = "0";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.CHK_USE_CENTERING);
            this.groupBox5.ForeColor = System.Drawing.Color.Coral;
            this.groupBox5.Location = new System.Drawing.Point(7, 274);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(521, 57);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CENTERING OPTION";
            // 
            // CHK_USE_CENTERING
            // 
            this.CHK_USE_CENTERING.AutoSize = true;
            this.CHK_USE_CENTERING.ForeColor = System.Drawing.Color.White;
            this.CHK_USE_CENTERING.Location = new System.Drawing.Point(6, 25);
            this.CHK_USE_CENTERING.Name = "CHK_USE_CENTERING";
            this.CHK_USE_CENTERING.Size = new System.Drawing.Size(159, 21);
            this.CHK_USE_CENTERING.TabIndex = 1;
            this.CHK_USE_CENTERING.Text = "USE CENTERING";
            this.CHK_USE_CENTERING.UseVisualStyleBackColor = true;
            this.CHK_USE_CENTERING.CheckedChanged += new System.EventHandler(this.CHK_FOCUS_NONE_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.RDO_MULTI_FRAME_MEDIAN);
            this.groupBox8.Controls.Add(this.RDO_MULTI_FRAME_VALUE_AVG);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.TXT_MULTI_FRAME_SHOT_COUNT);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.TXT_MULTI_FRAME_SHOT_DELAY);
            this.groupBox8.ForeColor = System.Drawing.Color.Coral;
            this.groupBox8.Location = new System.Drawing.Point(7, 151);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(521, 57);
            this.groupBox8.TabIndex = 36;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "MULTI-FRAME";
            // 
            // RDO_MULTI_FRAME_MEDIAN
            // 
            this.RDO_MULTI_FRAME_MEDIAN.AutoSize = true;
            this.RDO_MULTI_FRAME_MEDIAN.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RDO_MULTI_FRAME_MEDIAN.ForeColor = System.Drawing.Color.White;
            this.RDO_MULTI_FRAME_MEDIAN.Location = new System.Drawing.Point(455, 26);
            this.RDO_MULTI_FRAME_MEDIAN.Name = "RDO_MULTI_FRAME_MEDIAN";
            this.RDO_MULTI_FRAME_MEDIAN.Size = new System.Drawing.Size(63, 21);
            this.RDO_MULTI_FRAME_MEDIAN.TabIndex = 0;
            this.RDO_MULTI_FRAME_MEDIAN.Text = "MED";
            this.RDO_MULTI_FRAME_MEDIAN.UseVisualStyleBackColor = true;
            // 
            // RDO_MULTI_FRAME_VALUE_AVG
            // 
            this.RDO_MULTI_FRAME_VALUE_AVG.AutoSize = true;
            this.RDO_MULTI_FRAME_VALUE_AVG.Checked = true;
            this.RDO_MULTI_FRAME_VALUE_AVG.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.RDO_MULTI_FRAME_VALUE_AVG.ForeColor = System.Drawing.Color.White;
            this.RDO_MULTI_FRAME_VALUE_AVG.Location = new System.Drawing.Point(387, 26);
            this.RDO_MULTI_FRAME_VALUE_AVG.Name = "RDO_MULTI_FRAME_VALUE_AVG";
            this.RDO_MULTI_FRAME_VALUE_AVG.Size = new System.Drawing.Size(62, 21);
            this.RDO_MULTI_FRAME_VALUE_AVG.TabIndex = 0;
            this.RDO_MULTI_FRAME_VALUE_AVG.TabStop = true;
            this.RDO_MULTI_FRAME_VALUE_AVG.Text = "AVG";
            this.RDO_MULTI_FRAME_VALUE_AVG.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(141, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "SHOT_DELAY";
            // 
            // TXT_MULTI_FRAME_SHOT_COUNT
            // 
            this.TXT_MULTI_FRAME_SHOT_COUNT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_MULTI_FRAME_SHOT_COUNT.ForeColor = System.Drawing.Color.White;
            this.TXT_MULTI_FRAME_SHOT_COUNT.Location = new System.Drawing.Point(97, 21);
            this.TXT_MULTI_FRAME_SHOT_COUNT.Name = "TXT_MULTI_FRAME_SHOT_COUNT";
            this.TXT_MULTI_FRAME_SHOT_COUNT.Size = new System.Drawing.Size(38, 24);
            this.TXT_MULTI_FRAME_SHOT_COUNT.TabIndex = 1;
            this.TXT_MULTI_FRAME_SHOT_COUNT.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(5, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "SHOT CNT ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(341, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 14);
            this.label12.TabIndex = 48;
            this.label12.Text = "(ms)";
            // 
            // TXT_MULTI_FRAME_SHOT_DELAY
            // 
            this.TXT_MULTI_FRAME_SHOT_DELAY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_MULTI_FRAME_SHOT_DELAY.ForeColor = System.Drawing.Color.White;
            this.TXT_MULTI_FRAME_SHOT_DELAY.Location = new System.Drawing.Point(259, 21);
            this.TXT_MULTI_FRAME_SHOT_DELAY.Name = "TXT_MULTI_FRAME_SHOT_DELAY";
            this.TXT_MULTI_FRAME_SHOT_DELAY.Size = new System.Drawing.Size(76, 24);
            this.TXT_MULTI_FRAME_SHOT_DELAY.TabIndex = 1;
            this.TXT_MULTI_FRAME_SHOT_DELAY.Text = "100";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.TXT_LIGHT_VALUE);
            this.groupBox4.Controls.Add(this.TXT_PIXEL_RES);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.TXT_CAM_EXP);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Location = new System.Drawing.Point(280, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(247, 137);
            this.groupBox4.TabIndex = 77;
            this.groupBox4.TabStop = false;
            // 
            // Dlg_Params
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(560, 449);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dlg_Params";
            this.Text = "RECIPE PARAMETERS";
            this.Load += new System.EventHandler(this.Dlg_Params_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_RECP_PARAM_APPLY;
        private System.Windows.Forms.Button BTN_RECP_PARAM_CANCEL;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RDO_LIGHT_DF;
        private System.Windows.Forms.RadioButton RDO_LIGHT_BF;
        private System.Windows.Forms.RadioButton RDO_LIGHT_ALIGN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RDO_CAM_50X;
        private System.Windows.Forms.RadioButton RDO_CAM_25X;
        private System.Windows.Forms.RadioButton RDO_CAM_ALIGN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TXT_PIXEL_RES;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox CHK_FOCUS_IAF;
        private System.Windows.Forms.CheckBox CHK_FOCUS_LAF;
        private System.Windows.Forms.CheckBox CHK_FOCUS_ZAF;
        private System.Windows.Forms.CheckBox CHK_FOCUS_NONE;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox CHK_USE_CENTERING;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TXT_CAM_EXP;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TXT_MULTI_FRAME_SHOT_COUNT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TXT_MULTI_FRAME_SHOT_DELAY;
        private System.Windows.Forms.RadioButton RDO_MULTI_FRAME_MEDIAN;
        private System.Windows.Forms.RadioButton RDO_MULTI_FRAME_VALUE_AVG;
        private System.Windows.Forms.TextBox TXT_LIGHT_VALUE;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}
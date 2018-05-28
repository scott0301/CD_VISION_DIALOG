namespace CD_VISION_DIALOG
{
    partial class FormBaseRecp
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
            this.RDO_TYPE_ADI = new System.Windows.Forms.RadioButton();
            this.RDO_TYPE_ACI = new System.Windows.Forms.RadioButton();
            this.LV_BASE_RECP = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RDO_LENS_50X = new System.Windows.Forms.RadioButton();
            this.RDO_LENS_25X = new System.Windows.Forms.RadioButton();
            this.RDO_LENS_ALIGN = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TXT_LIGHT_VALUE = new System.Windows.Forms.TextBox();
            this.RDO_LIGHT_DF = new System.Windows.Forms.RadioButton();
            this.RDO_LIGHT_BF = new System.Windows.Forms.RadioButton();
            this.RDO_LIGHT_ALIGN = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.TXT_TARGET_BASE_RECP = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CHK_FOCUS_IAF = new System.Windows.Forms.CheckBox();
            this.CHK_FOCUS_LAF = new System.Windows.Forms.CheckBox();
            this.CHK_FOCUS_NONE = new System.Windows.Forms.CheckBox();
            this.CHK_FOCUS_ZAF = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CHK_USE_CENTERING = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.BTN_BASE_RECP_CANCEL = new System.Windows.Forms.Button();
            this.BTN_BASE_RECP_APPLY = new System.Windows.Forms.Button();
            this.BTN_BASE_RECP_REMOVE = new System.Windows.Forms.Button();
            this.BTN_BASE_RECP_REFRESH = new System.Windows.Forms.Button();
            this.BTN_BASE_RECP_MODIFY = new System.Windows.Forms.Button();
            this.BTN_BASE_RECP_COPY = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // RDO_TYPE_ADI
            // 
            this.RDO_TYPE_ADI.AutoSize = true;
            this.RDO_TYPE_ADI.Checked = true;
            this.RDO_TYPE_ADI.ForeColor = System.Drawing.Color.White;
            this.RDO_TYPE_ADI.Location = new System.Drawing.Point(88, 20);
            this.RDO_TYPE_ADI.Name = "RDO_TYPE_ADI";
            this.RDO_TYPE_ADI.Size = new System.Drawing.Size(59, 21);
            this.RDO_TYPE_ADI.TabIndex = 0;
            this.RDO_TYPE_ADI.TabStop = true;
            this.RDO_TYPE_ADI.Text = "ADI";
            this.RDO_TYPE_ADI.UseVisualStyleBackColor = true;
            this.RDO_TYPE_ADI.CheckedChanged += new System.EventHandler(this.RDO_TYPE_ADI_CheckedChanged);
            // 
            // RDO_TYPE_ACI
            // 
            this.RDO_TYPE_ACI.AutoSize = true;
            this.RDO_TYPE_ACI.ForeColor = System.Drawing.Color.White;
            this.RDO_TYPE_ACI.Location = new System.Drawing.Point(143, 20);
            this.RDO_TYPE_ACI.Name = "RDO_TYPE_ACI";
            this.RDO_TYPE_ACI.Size = new System.Drawing.Size(59, 21);
            this.RDO_TYPE_ACI.TabIndex = 0;
            this.RDO_TYPE_ACI.Text = "ACI";
            this.RDO_TYPE_ACI.UseVisualStyleBackColor = true;
            this.RDO_TYPE_ACI.CheckedChanged += new System.EventHandler(this.RDO_TYPE_ACI_CheckedChanged);
            // 
            // LV_BASE_RECP
            // 
            this.LV_BASE_RECP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.LV_BASE_RECP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LV_BASE_RECP.ForeColor = System.Drawing.Color.White;
            this.LV_BASE_RECP.FullRowSelect = true;
            this.LV_BASE_RECP.GridLines = true;
            this.LV_BASE_RECP.Location = new System.Drawing.Point(14, 82);
            this.LV_BASE_RECP.Name = "LV_BASE_RECP";
            this.LV_BASE_RECP.Size = new System.Drawing.Size(329, 401);
            this.LV_BASE_RECP.TabIndex = 3;
            this.LV_BASE_RECP.UseCompatibleStateImageBehavior = false;
            this.LV_BASE_RECP.View = System.Windows.Forms.View.Details;
            this.LV_BASE_RECP.SelectedIndexChanged += new System.EventHandler(this.LV_BASE_INI_SelectedIndexChanged);
            this.LV_BASE_RECP.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LV_BASE_INI_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IDX";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "LAYER";
            this.columnHeader2.Width = 227;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "TYPE :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RDO_LENS_50X);
            this.groupBox1.Controls.Add(this.RDO_LENS_25X);
            this.groupBox1.Controls.Add(this.RDO_LENS_ALIGN);
            this.groupBox1.ForeColor = System.Drawing.Color.Coral;
            this.groupBox1.Location = new System.Drawing.Point(359, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 57);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LENS";
            // 
            // RDO_LENS_50X
            // 
            this.RDO_LENS_50X.AutoSize = true;
            this.RDO_LENS_50X.ForeColor = System.Drawing.Color.White;
            this.RDO_LENS_50X.Location = new System.Drawing.Point(231, 23);
            this.RDO_LENS_50X.Name = "RDO_LENS_50X";
            this.RDO_LENS_50X.Size = new System.Drawing.Size(60, 21);
            this.RDO_LENS_50X.TabIndex = 0;
            this.RDO_LENS_50X.TabStop = true;
            this.RDO_LENS_50X.Text = "50X";
            this.RDO_LENS_50X.UseVisualStyleBackColor = true;
            // 
            // RDO_LENS_25X
            // 
            this.RDO_LENS_25X.AutoSize = true;
            this.RDO_LENS_25X.ForeColor = System.Drawing.Color.White;
            this.RDO_LENS_25X.Location = new System.Drawing.Point(119, 23);
            this.RDO_LENS_25X.Name = "RDO_LENS_25X";
            this.RDO_LENS_25X.Size = new System.Drawing.Size(60, 21);
            this.RDO_LENS_25X.TabIndex = 0;
            this.RDO_LENS_25X.TabStop = true;
            this.RDO_LENS_25X.Text = "25X";
            this.RDO_LENS_25X.UseVisualStyleBackColor = true;
            // 
            // RDO_LENS_ALIGN
            // 
            this.RDO_LENS_ALIGN.AutoSize = true;
            this.RDO_LENS_ALIGN.ForeColor = System.Drawing.Color.White;
            this.RDO_LENS_ALIGN.Location = new System.Drawing.Point(7, 23);
            this.RDO_LENS_ALIGN.Name = "RDO_LENS_ALIGN";
            this.RDO_LENS_ALIGN.Size = new System.Drawing.Size(79, 21);
            this.RDO_LENS_ALIGN.TabIndex = 0;
            this.RDO_LENS_ALIGN.TabStop = true;
            this.RDO_LENS_ALIGN.Text = "ALIGN";
            this.RDO_LENS_ALIGN.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.TXT_LIGHT_VALUE);
            this.groupBox2.Controls.Add(this.RDO_LIGHT_DF);
            this.groupBox2.Controls.Add(this.RDO_LIGHT_BF);
            this.groupBox2.Controls.Add(this.RDO_LIGHT_ALIGN);
            this.groupBox2.ForeColor = System.Drawing.Color.Coral;
            this.groupBox2.Location = new System.Drawing.Point(359, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 57);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LIGHT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(303, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "VAL :";
            // 
            // TXT_LIGHT_VALUE
            // 
            this.TXT_LIGHT_VALUE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_LIGHT_VALUE.ForeColor = System.Drawing.Color.White;
            this.TXT_LIGHT_VALUE.Location = new System.Drawing.Point(356, 22);
            this.TXT_LIGHT_VALUE.Name = "TXT_LIGHT_VALUE";
            this.TXT_LIGHT_VALUE.Size = new System.Drawing.Size(84, 24);
            this.TXT_LIGHT_VALUE.TabIndex = 1;
            // 
            // RDO_LIGHT_DF
            // 
            this.RDO_LIGHT_DF.AutoSize = true;
            this.RDO_LIGHT_DF.ForeColor = System.Drawing.Color.White;
            this.RDO_LIGHT_DF.Location = new System.Drawing.Point(231, 23);
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
            this.RDO_LIGHT_BF.Location = new System.Drawing.Point(119, 23);
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
            this.RDO_LIGHT_ALIGN.Location = new System.Drawing.Point(7, 23);
            this.RDO_LIGHT_ALIGN.Name = "RDO_LIGHT_ALIGN";
            this.RDO_LIGHT_ALIGN.Size = new System.Drawing.Size(79, 21);
            this.RDO_LIGHT_ALIGN.TabIndex = 0;
            this.RDO_LIGHT_ALIGN.TabStop = true;
            this.RDO_LIGHT_ALIGN.Text = "ALIGN";
            this.RDO_LIGHT_ALIGN.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Coral;
            this.label9.Location = new System.Drawing.Point(12, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 17);
            this.label9.TabIndex = 4;
            this.label9.Text = "TARGET :";
            // 
            // TXT_TARGET_BASE_RECP
            // 
            this.TXT_TARGET_BASE_RECP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_TARGET_BASE_RECP.ForeColor = System.Drawing.Color.White;
            this.TXT_TARGET_BASE_RECP.Location = new System.Drawing.Point(100, 50);
            this.TXT_TARGET_BASE_RECP.Name = "TXT_TARGET_BASE_RECP";
            this.TXT_TARGET_BASE_RECP.Size = new System.Drawing.Size(243, 24);
            this.TXT_TARGET_BASE_RECP.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CHK_FOCUS_IAF);
            this.groupBox3.Controls.Add(this.CHK_FOCUS_LAF);
            this.groupBox3.Controls.Add(this.CHK_FOCUS_NONE);
            this.groupBox3.Controls.Add(this.CHK_FOCUS_ZAF);
            this.groupBox3.ForeColor = System.Drawing.Color.Coral;
            this.groupBox3.Location = new System.Drawing.Point(356, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(455, 57);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FOCUS TYPE";
            // 
            // CHK_FOCUS_IAF
            // 
            this.CHK_FOCUS_IAF.AutoSize = true;
            this.CHK_FOCUS_IAF.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_IAF.Location = new System.Drawing.Point(317, 26);
            this.CHK_FOCUS_IAF.Name = "CHK_FOCUS_IAF";
            this.CHK_FOCUS_IAF.Size = new System.Drawing.Size(58, 21);
            this.CHK_FOCUS_IAF.TabIndex = 1;
            this.CHK_FOCUS_IAF.Text = "IAF";
            this.CHK_FOCUS_IAF.UseVisualStyleBackColor = true;
            // 
            // CHK_FOCUS_LAF
            // 
            this.CHK_FOCUS_LAF.AutoSize = true;
            this.CHK_FOCUS_LAF.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_LAF.Location = new System.Drawing.Point(224, 25);
            this.CHK_FOCUS_LAF.Name = "CHK_FOCUS_LAF";
            this.CHK_FOCUS_LAF.Size = new System.Drawing.Size(59, 21);
            this.CHK_FOCUS_LAF.TabIndex = 1;
            this.CHK_FOCUS_LAF.Text = "LAF";
            this.CHK_FOCUS_LAF.UseVisualStyleBackColor = true;
            // 
            // CHK_FOCUS_NONE
            // 
            this.CHK_FOCUS_NONE.AutoSize = true;
            this.CHK_FOCUS_NONE.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_NONE.Location = new System.Drawing.Point(7, 25);
            this.CHK_FOCUS_NONE.Name = "CHK_FOCUS_NONE";
            this.CHK_FOCUS_NONE.Size = new System.Drawing.Size(74, 21);
            this.CHK_FOCUS_NONE.TabIndex = 1;
            this.CHK_FOCUS_NONE.Text = "NONE";
            this.CHK_FOCUS_NONE.UseVisualStyleBackColor = true;
            // 
            // CHK_FOCUS_ZAF
            // 
            this.CHK_FOCUS_ZAF.AutoSize = true;
            this.CHK_FOCUS_ZAF.ForeColor = System.Drawing.Color.White;
            this.CHK_FOCUS_ZAF.Location = new System.Drawing.Point(128, 25);
            this.CHK_FOCUS_ZAF.Name = "CHK_FOCUS_ZAF";
            this.CHK_FOCUS_ZAF.Size = new System.Drawing.Size(60, 21);
            this.CHK_FOCUS_ZAF.TabIndex = 1;
            this.CHK_FOCUS_ZAF.Text = "ZAF";
            this.CHK_FOCUS_ZAF.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.RDO_TYPE_ADI);
            this.panel1.Controls.Add(this.BTN_BASE_RECP_CANCEL);
            this.panel1.Controls.Add(this.TXT_TARGET_BASE_RECP);
            this.panel1.Controls.Add(this.BTN_BASE_RECP_APPLY);
            this.panel1.Controls.Add(this.RDO_TYPE_ACI);
            this.panel1.Controls.Add(this.LV_BASE_RECP);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.BTN_BASE_RECP_REMOVE);
            this.panel1.Controls.Add(this.BTN_BASE_RECP_REFRESH);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.BTN_BASE_RECP_MODIFY);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.BTN_BASE_RECP_COPY);
            this.panel1.Location = new System.Drawing.Point(12, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 576);
            this.panel1.TabIndex = 42;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.CHK_USE_CENTERING);
            this.groupBox5.ForeColor = System.Drawing.Color.Coral;
            this.groupBox5.Location = new System.Drawing.Point(356, 233);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(455, 57);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CENTERING";
            // 
            // CHK_USE_CENTERING
            // 
            this.CHK_USE_CENTERING.AutoSize = true;
            this.CHK_USE_CENTERING.ForeColor = System.Drawing.Color.White;
            this.CHK_USE_CENTERING.Location = new System.Drawing.Point(7, 25);
            this.CHK_USE_CENTERING.Name = "CHK_USE_CENTERING";
            this.CHK_USE_CENTERING.Size = new System.Drawing.Size(159, 21);
            this.CHK_USE_CENTERING.TabIndex = 1;
            this.CHK_USE_CENTERING.Text = "USE CENTERING";
            this.CHK_USE_CENTERING.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(284, 560);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 14);
            this.label15.TabIndex = 2;
            this.label15.Text = "REMOVE";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(217, 559);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 14);
            this.label14.TabIndex = 2;
            this.label14.Text = "MODIFY";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(159, 559);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 14);
            this.label13.TabIndex = 2;
            this.label13.Text = "COPY";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(759, 558);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 14);
            this.label11.TabIndex = 2;
            this.label11.Text = "APPLY";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(6, 558);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 14);
            this.label12.TabIndex = 2;
            this.label12.Text = "REFRESH";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(688, 558);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 14);
            this.label10.TabIndex = 2;
            this.label10.Text = "CANCEL";
            // 
            // BTN_BASE_RECP_CANCEL
            // 
            this.BTN_BASE_RECP_CANCEL.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_BASE_RECP_CANCEL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_BASE_RECP_CANCEL.Location = new System.Drawing.Point(687, 495);
            this.BTN_BASE_RECP_CANCEL.Name = "BTN_BASE_RECP_CANCEL";
            this.BTN_BASE_RECP_CANCEL.Size = new System.Drawing.Size(60, 60);
            this.BTN_BASE_RECP_CANCEL.TabIndex = 6;
            this.BTN_BASE_RECP_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_BASE_RECP_CANCEL.Click += new System.EventHandler(this.BTN_BASE_RECP_CANCEL_Click);
            // 
            // BTN_BASE_RECP_APPLY
            // 
            this.BTN_BASE_RECP_APPLY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_ok;
            this.BTN_BASE_RECP_APPLY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_BASE_RECP_APPLY.Location = new System.Drawing.Point(753, 495);
            this.BTN_BASE_RECP_APPLY.Name = "BTN_BASE_RECP_APPLY";
            this.BTN_BASE_RECP_APPLY.Size = new System.Drawing.Size(60, 60);
            this.BTN_BASE_RECP_APPLY.TabIndex = 6;
            this.BTN_BASE_RECP_APPLY.UseVisualStyleBackColor = true;
            this.BTN_BASE_RECP_APPLY.Click += new System.EventHandler(this.BTN_BASE_RECP_APPLY_Click);
            // 
            // BTN_BASE_RECP_REMOVE
            // 
            this.BTN_BASE_RECP_REMOVE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.Trash_empty;
            this.BTN_BASE_RECP_REMOVE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_BASE_RECP_REMOVE.Location = new System.Drawing.Point(283, 495);
            this.BTN_BASE_RECP_REMOVE.Name = "BTN_BASE_RECP_REMOVE";
            this.BTN_BASE_RECP_REMOVE.Size = new System.Drawing.Size(60, 60);
            this.BTN_BASE_RECP_REMOVE.TabIndex = 6;
            this.BTN_BASE_RECP_REMOVE.UseVisualStyleBackColor = true;
            this.BTN_BASE_RECP_REMOVE.Click += new System.EventHandler(this.BTN_BASE_RECP_REMOVE_Click);
            // 
            // BTN_BASE_RECP_REFRESH
            // 
            this.BTN_BASE_RECP_REFRESH.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_BASE_RECP_REFRESH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_BASE_RECP_REFRESH.Location = new System.Drawing.Point(11, 495);
            this.BTN_BASE_RECP_REFRESH.Name = "BTN_BASE_RECP_REFRESH";
            this.BTN_BASE_RECP_REFRESH.Size = new System.Drawing.Size(60, 60);
            this.BTN_BASE_RECP_REFRESH.TabIndex = 6;
            this.BTN_BASE_RECP_REFRESH.UseVisualStyleBackColor = true;
            this.BTN_BASE_RECP_REFRESH.Click += new System.EventHandler(this.BTN_REFRESH_BASE_RECP_Click);
            // 
            // BTN_BASE_RECP_MODIFY
            // 
            this.BTN_BASE_RECP_MODIFY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.edit_yo;
            this.BTN_BASE_RECP_MODIFY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_BASE_RECP_MODIFY.Cursor = System.Windows.Forms.Cursors.Default;
            this.BTN_BASE_RECP_MODIFY.Location = new System.Drawing.Point(217, 495);
            this.BTN_BASE_RECP_MODIFY.Name = "BTN_BASE_RECP_MODIFY";
            this.BTN_BASE_RECP_MODIFY.Size = new System.Drawing.Size(60, 60);
            this.BTN_BASE_RECP_MODIFY.TabIndex = 6;
            this.BTN_BASE_RECP_MODIFY.UseVisualStyleBackColor = true;
            this.BTN_BASE_RECP_MODIFY.Click += new System.EventHandler(this.BTN_BASE_RECP_MODIFY_Click);
            // 
            // BTN_BASE_RECP_COPY
            // 
            this.BTN_BASE_RECP_COPY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.copy_1;
            this.BTN_BASE_RECP_COPY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_BASE_RECP_COPY.Location = new System.Drawing.Point(151, 495);
            this.BTN_BASE_RECP_COPY.Name = "BTN_BASE_RECP_COPY";
            this.BTN_BASE_RECP_COPY.Size = new System.Drawing.Size(60, 60);
            this.BTN_BASE_RECP_COPY.TabIndex = 6;
            this.BTN_BASE_RECP_COPY.UseVisualStyleBackColor = true;
            this.BTN_BASE_RECP_COPY.Click += new System.EventHandler(this.BTN_BASE_RECP_COPY_Click);
            // 
            // FormBaseRecp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(850, 597);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.Name = "FormBaseRecp";
            this.Text = "RECIPE CREATION ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBaseRecp_FormClosing);
            this.Load += new System.EventHandler(this.RecpCreateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton RDO_TYPE_ADI;
        private System.Windows.Forms.RadioButton RDO_TYPE_ACI;
        private System.Windows.Forms.ListView LV_BASE_RECP;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RDO_LENS_50X;
        private System.Windows.Forms.RadioButton RDO_LENS_25X;
        private System.Windows.Forms.RadioButton RDO_LENS_ALIGN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RDO_LIGHT_DF;
        private System.Windows.Forms.RadioButton RDO_LIGHT_BF;
        private System.Windows.Forms.RadioButton RDO_LIGHT_ALIGN;
        private System.Windows.Forms.TextBox TXT_LIGHT_VALUE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BTN_BASE_RECP_REFRESH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TXT_TARGET_BASE_RECP;
        private System.Windows.Forms.Button BTN_BASE_RECP_COPY;
        private System.Windows.Forms.Button BTN_BASE_RECP_MODIFY;
        private System.Windows.Forms.Button BTN_BASE_RECP_APPLY;
        private System.Windows.Forms.Button BTN_BASE_RECP_REMOVE;
        private System.Windows.Forms.Button BTN_BASE_RECP_CANCEL;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox CHK_FOCUS_IAF;
        private System.Windows.Forms.CheckBox CHK_FOCUS_LAF;
        private System.Windows.Forms.CheckBox CHK_FOCUS_NONE;
        private System.Windows.Forms.CheckBox CHK_FOCUS_ZAF;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox CHK_USE_CENTERING;
    }
}
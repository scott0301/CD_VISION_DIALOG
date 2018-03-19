namespace CD_VISION_DIALOG
{
    partial class Dlg_Tunning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dlg_Tunning));
            this.label16 = new System.Windows.Forms.Label();
            this.TAB_TUNNING_TARGET = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RDO_RECT_TYPE_DIA = new System.Windows.Forms.RadioButton();
            this.RDO_RECT_TYPE_VER = new System.Windows.Forms.RadioButton();
            this.RDO_RECT_TYPE_HOR = new System.Windows.Forms.RadioButton();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.BTN_CHECK_EDGE_DETECTION = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.TXT_RECT_CANDIDATE_COUNT = new System.Windows.Forms.NumericUpDown();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.RDO_RECT_APD_SCD = new System.Windows.Forms.RadioButton();
            this.RDO_RECT_APD_FST = new System.Windows.Forms.RadioButton();
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD = new System.Windows.Forms.Button();
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD = new System.Windows.Forms.Button();
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST = new System.Windows.Forms.Button();
            this.BTN_RECT_VERIFY_EDGE_REGION_FST = new System.Windows.Forms.Button();
            this.CB_RECT_TARGET_INDEX_SCD = new System.Windows.Forms.ComboBox();
            this.CB_RECT_TARGET_INDEX_FST = new System.Windows.Forms.ComboBox();
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION = new System.Windows.Forms.CheckBox();
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD = new System.Windows.Forms.TextBox();
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST = new System.Windows.Forms.TextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.TXT_RECT_SELECTED_FIGURE = new System.Windows.Forms.TextBox();
            this.TXT_RECT_SELECTED_INDEX = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.uc_thumb_nail_rect = new NS_UC_THUMB_NAIL.UC_THUMB_NAIL();
            this.BTN_PARAM_WRITE = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.uc_tunning_view = new CD_View.UC_CD_VIEWER();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_PTRN_APPLY = new System.Windows.Forms.Button();
            this.BTN_RECOVER = new System.Windows.Forms.Button();
            this.TAB_TUNNING_TARGET.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TXT_RECT_CANDIDATE_COUNT)).BeginInit();
            this.groupBox17.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(1307, 689);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 14);
            this.label16.TabIndex = 81;
            this.label16.Text = "CLOSE";
            // 
            // TAB_TUNNING_TARGET
            // 
            this.TAB_TUNNING_TARGET.Controls.Add(this.tabPage2);
            this.TAB_TUNNING_TARGET.ItemSize = new System.Drawing.Size(100, 40);
            this.TAB_TUNNING_TARGET.Location = new System.Drawing.Point(618, 12);
            this.TAB_TUNNING_TARGET.Name = "TAB_TUNNING_TARGET";
            this.TAB_TUNNING_TARGET.Padding = new System.Drawing.Point(0, 0);
            this.TAB_TUNNING_TARGET.SelectedIndex = 0;
            this.TAB_TUNNING_TARGET.Size = new System.Drawing.Size(749, 608);
            this.TAB_TUNNING_TARGET.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TAB_TUNNING_TARGET.TabIndex = 83;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox18);
            this.tabPage2.Controls.Add(this.groupBox17);
            this.tabPage2.Controls.Add(this.groupBox12);
            this.tabPage2.Controls.Add(this.uc_thumb_nail_rect);
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(741, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RECT";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RDO_RECT_TYPE_DIA);
            this.groupBox3.Controls.Add(this.RDO_RECT_TYPE_VER);
            this.groupBox3.Controls.Add(this.RDO_RECT_TYPE_HOR);
            this.groupBox3.ForeColor = System.Drawing.Color.Coral;
            this.groupBox3.Location = new System.Drawing.Point(6, 227);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(729, 51);
            this.groupBox3.TabIndex = 104;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RECTANGLE TYPE";
            // 
            // RDO_RECT_TYPE_DIA
            // 
            this.RDO_RECT_TYPE_DIA.AutoSize = true;
            this.RDO_RECT_TYPE_DIA.Enabled = false;
            this.RDO_RECT_TYPE_DIA.ForeColor = System.Drawing.Color.White;
            this.RDO_RECT_TYPE_DIA.Location = new System.Drawing.Point(243, 21);
            this.RDO_RECT_TYPE_DIA.Name = "RDO_RECT_TYPE_DIA";
            this.RDO_RECT_TYPE_DIA.Size = new System.Drawing.Size(93, 18);
            this.RDO_RECT_TYPE_DIA.TabIndex = 0;
            this.RDO_RECT_TYPE_DIA.TabStop = true;
            this.RDO_RECT_TYPE_DIA.Text = "RECT_DIA";
            this.RDO_RECT_TYPE_DIA.UseVisualStyleBackColor = true;
            // 
            // RDO_RECT_TYPE_VER
            // 
            this.RDO_RECT_TYPE_VER.AutoSize = true;
            this.RDO_RECT_TYPE_VER.Enabled = false;
            this.RDO_RECT_TYPE_VER.ForeColor = System.Drawing.Color.White;
            this.RDO_RECT_TYPE_VER.Location = new System.Drawing.Point(126, 21);
            this.RDO_RECT_TYPE_VER.Name = "RDO_RECT_TYPE_VER";
            this.RDO_RECT_TYPE_VER.Size = new System.Drawing.Size(94, 18);
            this.RDO_RECT_TYPE_VER.TabIndex = 0;
            this.RDO_RECT_TYPE_VER.TabStop = true;
            this.RDO_RECT_TYPE_VER.Text = "RECT_VER";
            this.RDO_RECT_TYPE_VER.UseVisualStyleBackColor = true;
            // 
            // RDO_RECT_TYPE_HOR
            // 
            this.RDO_RECT_TYPE_HOR.AutoSize = true;
            this.RDO_RECT_TYPE_HOR.Enabled = false;
            this.RDO_RECT_TYPE_HOR.ForeColor = System.Drawing.Color.White;
            this.RDO_RECT_TYPE_HOR.Location = new System.Drawing.Point(9, 21);
            this.RDO_RECT_TYPE_HOR.Name = "RDO_RECT_TYPE_HOR";
            this.RDO_RECT_TYPE_HOR.Size = new System.Drawing.Size(98, 18);
            this.RDO_RECT_TYPE_HOR.TabIndex = 0;
            this.RDO_RECT_TYPE_HOR.TabStop = true;
            this.RDO_RECT_TYPE_HOR.Text = "RECT_HOR";
            this.RDO_RECT_TYPE_HOR.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.BTN_CHECK_EDGE_DETECTION);
            this.groupBox18.Controls.Add(this.label21);
            this.groupBox18.Controls.Add(this.TXT_RECT_CANDIDATE_COUNT);
            this.groupBox18.ForeColor = System.Drawing.Color.Coral;
            this.groupBox18.Location = new System.Drawing.Point(433, 284);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(302, 113);
            this.groupBox18.TabIndex = 104;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "PEAK DETECTION PARAM";
            // 
            // BTN_CHECK_EDGE_DETECTION
            // 
            this.BTN_CHECK_EDGE_DETECTION.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_CHECK_EDGE_DETECTION.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BTN_CHECK_EDGE_DETECTION.ForeColor = System.Drawing.Color.Black;
            this.BTN_CHECK_EDGE_DETECTION.Location = new System.Drawing.Point(10, 47);
            this.BTN_CHECK_EDGE_DETECTION.Name = "BTN_CHECK_EDGE_DETECTION";
            this.BTN_CHECK_EDGE_DETECTION.Size = new System.Drawing.Size(286, 52);
            this.BTN_CHECK_EDGE_DETECTION.TabIndex = 80;
            this.BTN_CHECK_EDGE_DETECTION.Text = "DETECTION";
            this.BTN_CHECK_EDGE_DETECTION.UseVisualStyleBackColor = true;
            this.BTN_CHECK_EDGE_DETECTION.Click += new System.EventHandler(this.BTN_CHECK_EDGE_DETECTION_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(14, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 14);
            this.label21.TabIndex = 93;
            this.label21.Text = "PEAK COUNT";
            // 
            // TXT_RECT_CANDIDATE_COUNT
            // 
            this.TXT_RECT_CANDIDATE_COUNT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TXT_RECT_CANDIDATE_COUNT.ForeColor = System.Drawing.Color.White;
            this.TXT_RECT_CANDIDATE_COUNT.Location = new System.Drawing.Point(114, 19);
            this.TXT_RECT_CANDIDATE_COUNT.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TXT_RECT_CANDIDATE_COUNT.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.TXT_RECT_CANDIDATE_COUNT.Name = "TXT_RECT_CANDIDATE_COUNT";
            this.TXT_RECT_CANDIDATE_COUNT.Size = new System.Drawing.Size(182, 22);
            this.TXT_RECT_CANDIDATE_COUNT.TabIndex = 95;
            this.TXT_RECT_CANDIDATE_COUNT.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.RDO_RECT_APD_SCD);
            this.groupBox17.Controls.Add(this.RDO_RECT_APD_FST);
            this.groupBox17.Controls.Add(this.BTN_RECT_VERIFY_EDGE_REGION_SCD);
            this.groupBox17.Controls.Add(this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD);
            this.groupBox17.Controls.Add(this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST);
            this.groupBox17.Controls.Add(this.BTN_RECT_VERIFY_EDGE_REGION_FST);
            this.groupBox17.Controls.Add(this.CB_RECT_TARGET_INDEX_SCD);
            this.groupBox17.Controls.Add(this.CB_RECT_TARGET_INDEX_FST);
            this.groupBox17.Controls.Add(this.CHK_RECT_USE_AUTO_PEAK_DETECTION);
            this.groupBox17.Controls.Add(this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD);
            this.groupBox17.Controls.Add(this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST);
            this.groupBox17.ForeColor = System.Drawing.Color.Coral;
            this.groupBox17.Location = new System.Drawing.Point(6, 284);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(421, 116);
            this.groupBox17.TabIndex = 99;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "AUTO PEAK DETECTION";
            // 
            // RDO_RECT_APD_SCD
            // 
            this.RDO_RECT_APD_SCD.AutoSize = true;
            this.RDO_RECT_APD_SCD.Location = new System.Drawing.Point(12, 75);
            this.RDO_RECT_APD_SCD.Name = "RDO_RECT_APD_SCD";
            this.RDO_RECT_APD_SCD.Size = new System.Drawing.Size(121, 18);
            this.RDO_RECT_APD_SCD.TabIndex = 95;
            this.RDO_RECT_APD_SCD.TabStop = true;
            this.RDO_RECT_APD_SCD.Text = "SECOND PEAK";
            this.RDO_RECT_APD_SCD.UseVisualStyleBackColor = true;
            // 
            // RDO_RECT_APD_FST
            // 
            this.RDO_RECT_APD_FST.AutoSize = true;
            this.RDO_RECT_APD_FST.Location = new System.Drawing.Point(10, 46);
            this.RDO_RECT_APD_FST.Name = "RDO_RECT_APD_FST";
            this.RDO_RECT_APD_FST.Size = new System.Drawing.Size(104, 18);
            this.RDO_RECT_APD_FST.TabIndex = 95;
            this.RDO_RECT_APD_FST.TabStop = true;
            this.RDO_RECT_APD_FST.Text = "FIRST PEAK";
            this.RDO_RECT_APD_FST.UseVisualStyleBackColor = true;
            // 
            // BTN_RECT_VERIFY_EDGE_REGION_SCD
            // 
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.ForeColor = System.Drawing.Color.Black;
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.Location = new System.Drawing.Point(294, 74);
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.Name = "BTN_RECT_VERIFY_EDGE_REGION_SCD";
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.Size = new System.Drawing.Size(121, 30);
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.TabIndex = 80;
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.Text = "CHECK";
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.UseVisualStyleBackColor = true;
            this.BTN_RECT_VERIFY_EDGE_REGION_SCD.Click += new System.EventHandler(this.BTN_RECT_VERIFY_EDGE_REGION_SCD_Click);
            // 
            // BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD
            // 
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.ForeColor = System.Drawing.Color.Black;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.Location = new System.Drawing.Point(201, 73);
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.Name = "BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD";
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.Size = new System.Drawing.Size(27, 30);
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.TabIndex = 80;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.Text = "◀";
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.UseVisualStyleBackColor = true;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD.Click += new System.EventHandler(this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD_Click);
            // 
            // BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST
            // 
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.ForeColor = System.Drawing.Color.Black;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.Location = new System.Drawing.Point(201, 42);
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.Name = "BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST";
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.Size = new System.Drawing.Size(27, 30);
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.TabIndex = 80;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.Text = "◀";
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.UseVisualStyleBackColor = true;
            this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST.Click += new System.EventHandler(this.BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST_Click);
            // 
            // BTN_RECT_VERIFY_EDGE_REGION_FST
            // 
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.ForeColor = System.Drawing.Color.Black;
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.Location = new System.Drawing.Point(294, 44);
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.Name = "BTN_RECT_VERIFY_EDGE_REGION_FST";
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.Size = new System.Drawing.Size(121, 30);
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.TabIndex = 80;
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.Text = "CHECK";
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.UseVisualStyleBackColor = true;
            this.BTN_RECT_VERIFY_EDGE_REGION_FST.Click += new System.EventHandler(this.BTN_RECT_VERIFY_EDGE_REGION_FST_Click);
            // 
            // CB_RECT_TARGET_INDEX_SCD
            // 
            this.CB_RECT_TARGET_INDEX_SCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.CB_RECT_TARGET_INDEX_SCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_RECT_TARGET_INDEX_SCD.ForeColor = System.Drawing.Color.White;
            this.CB_RECT_TARGET_INDEX_SCD.FormatString = "N0";
            this.CB_RECT_TARGET_INDEX_SCD.FormattingEnabled = true;
            this.CB_RECT_TARGET_INDEX_SCD.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.CB_RECT_TARGET_INDEX_SCD.Location = new System.Drawing.Point(234, 78);
            this.CB_RECT_TARGET_INDEX_SCD.Name = "CB_RECT_TARGET_INDEX_SCD";
            this.CB_RECT_TARGET_INDEX_SCD.Size = new System.Drawing.Size(54, 22);
            this.CB_RECT_TARGET_INDEX_SCD.TabIndex = 94;
            this.CB_RECT_TARGET_INDEX_SCD.Text = "0";
            // 
            // CB_RECT_TARGET_INDEX_FST
            // 
            this.CB_RECT_TARGET_INDEX_FST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.CB_RECT_TARGET_INDEX_FST.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_RECT_TARGET_INDEX_FST.ForeColor = System.Drawing.Color.White;
            this.CB_RECT_TARGET_INDEX_FST.FormatString = "N0";
            this.CB_RECT_TARGET_INDEX_FST.FormattingEnabled = true;
            this.CB_RECT_TARGET_INDEX_FST.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.CB_RECT_TARGET_INDEX_FST.Location = new System.Drawing.Point(234, 46);
            this.CB_RECT_TARGET_INDEX_FST.Name = "CB_RECT_TARGET_INDEX_FST";
            this.CB_RECT_TARGET_INDEX_FST.Size = new System.Drawing.Size(54, 22);
            this.CB_RECT_TARGET_INDEX_FST.TabIndex = 94;
            this.CB_RECT_TARGET_INDEX_FST.Text = "0";
            // 
            // CHK_RECT_USE_AUTO_PEAK_DETECTION
            // 
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.AutoSize = true;
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.ForeColor = System.Drawing.Color.White;
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.Location = new System.Drawing.Point(12, 21);
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.Name = "CHK_RECT_USE_AUTO_PEAK_DETECTION";
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.Size = new System.Drawing.Size(231, 18);
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.TabIndex = 12;
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.Text = "USE_AUTO_PEAK_DETECTION";
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.UseVisualStyleBackColor = true;
            this.CHK_RECT_USE_AUTO_PEAK_DETECTION.CheckedChanged += new System.EventHandler(this.CHK_RECT_USE_AUTO_PEAK_DETECTION_CheckedChanged);
            // 
            // TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD
            // 
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.ForeColor = System.Drawing.Color.White;
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Location = new System.Drawing.Point(148, 78);
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Name = "TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD";
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Size = new System.Drawing.Size(44, 22);
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.TabIndex = 3;
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.Text = "0";
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST
            // 
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.ForeColor = System.Drawing.Color.White;
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Location = new System.Drawing.Point(148, 46);
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Name = "TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST";
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Size = new System.Drawing.Size(44, 22);
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.TabIndex = 3;
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.Text = "0";
            this.TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.TXT_RECT_SELECTED_FIGURE);
            this.groupBox12.Controls.Add(this.TXT_RECT_SELECTED_INDEX);
            this.groupBox12.Controls.Add(this.label17);
            this.groupBox12.Controls.Add(this.label18);
            this.groupBox12.ForeColor = System.Drawing.Color.Coral;
            this.groupBox12.Location = new System.Drawing.Point(6, 171);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(729, 50);
            this.groupBox12.TabIndex = 86;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "TARGET INFO";
            // 
            // TXT_RECT_SELECTED_FIGURE
            // 
            this.TXT_RECT_SELECTED_FIGURE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TXT_RECT_SELECTED_FIGURE.Enabled = false;
            this.TXT_RECT_SELECTED_FIGURE.ForeColor = System.Drawing.Color.White;
            this.TXT_RECT_SELECTED_FIGURE.Location = new System.Drawing.Point(319, 17);
            this.TXT_RECT_SELECTED_FIGURE.Name = "TXT_RECT_SELECTED_FIGURE";
            this.TXT_RECT_SELECTED_FIGURE.Size = new System.Drawing.Size(186, 22);
            this.TXT_RECT_SELECTED_FIGURE.TabIndex = 3;
            // 
            // TXT_RECT_SELECTED_INDEX
            // 
            this.TXT_RECT_SELECTED_INDEX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.TXT_RECT_SELECTED_INDEX.Enabled = false;
            this.TXT_RECT_SELECTED_INDEX.ForeColor = System.Drawing.Color.White;
            this.TXT_RECT_SELECTED_INDEX.Location = new System.Drawing.Point(70, 17);
            this.TXT_RECT_SELECTED_INDEX.Name = "TXT_RECT_SELECTED_INDEX";
            this.TXT_RECT_SELECTED_INDEX.Size = new System.Drawing.Size(88, 22);
            this.TXT_RECT_SELECTED_INDEX.TabIndex = 3;
            this.TXT_RECT_SELECTED_INDEX.Text = "0";
            this.TXT_RECT_SELECTED_INDEX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(6, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 14);
            this.label17.TabIndex = 2;
            this.label17.Text = "INDEX :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(174, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(149, 14);
            this.label18.TabIndex = 2;
            this.label18.Text = "SELECTED_TARGET : ";
            // 
            // uc_thumb_nail_rect
            // 
            this.uc_thumb_nail_rect.BackColor = System.Drawing.Color.Black;
            this.uc_thumb_nail_rect.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.uc_thumb_nail_rect.Location = new System.Drawing.Point(6, 5);
            this.uc_thumb_nail_rect.m_bLoadAvailable = false;
            this.uc_thumb_nail_rect.Name = "uc_thumb_nail_rect";
            this.uc_thumb_nail_rect.PATH_FOLDER = "C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\IDE";
            this.uc_thumb_nail_rect.Size = new System.Drawing.Size(729, 160);
            this.uc_thumb_nail_rect.TabIndex = 1;
            this.uc_thumb_nail_rect.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.uc_thumb_nail_rect.ThumbNailSize = 196;
            this.uc_thumb_nail_rect.UseCompatibleStateImageBehavior = false;
            this.uc_thumb_nail_rect.DoubleClick += new System.EventHandler(this.uc_thumb_nail_rect_DoubleClick);
            // 
            // BTN_PARAM_WRITE
            // 
            this.BTN_PARAM_WRITE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_PARAM_WRITE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PARAM_WRITE.Location = new System.Drawing.Point(1235, 626);
            this.BTN_PARAM_WRITE.Name = "BTN_PARAM_WRITE";
            this.BTN_PARAM_WRITE.Size = new System.Drawing.Size(60, 60);
            this.BTN_PARAM_WRITE.TabIndex = 80;
            this.BTN_PARAM_WRITE.UseVisualStyleBackColor = true;
            this.BTN_PARAM_WRITE.Click += new System.EventHandler(this.BTN_PARAM_WRITE_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1235, 689);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 14);
            this.label5.TabIndex = 82;
            this.label5.Text = "UPDATE";
            // 
            // uc_tunning_view
            // 
            this.uc_tunning_view.AllowDrop = true;
            this.uc_tunning_view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uc_tunning_view.BOOL_DRAW_FOCUS_ROI = true;
            this.uc_tunning_view.BOOL_DRAW_PTRN_ROI = true;
            this.uc_tunning_view.BOOL_TEACHING_ACTIVATION = false;
            this.uc_tunning_view.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_tunning_view.ForeColor = System.Drawing.Color.Lime;
            this.uc_tunning_view.Location = new System.Drawing.Point(12, 13);
            this.uc_tunning_view.Name = "uc_tunning_view";
            this.uc_tunning_view.PT_FIGURE_TO_DRAW = ((System.Drawing.PointF)(resources.GetObject("uc_tunning_view.PT_FIGURE_TO_DRAW")));
            this.uc_tunning_view.ROI_INDEX = -1;
            this.uc_tunning_view.Size = new System.Drawing.Size(600, 685);
            this.uc_tunning_view.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(618, 689);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 82;
            this.label1.Text = "RECOVER";
            // 
            // BTN_PTRN_APPLY
            // 
            this.BTN_PTRN_APPLY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_PTRN_APPLY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_APPLY.Location = new System.Drawing.Point(1301, 626);
            this.BTN_PTRN_APPLY.Name = "BTN_PTRN_APPLY";
            this.BTN_PTRN_APPLY.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_APPLY.TabIndex = 79;
            this.BTN_PTRN_APPLY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTN_PTRN_APPLY.UseVisualStyleBackColor = true;
            this.BTN_PTRN_APPLY.Click += new System.EventHandler(this.BTN_PTRN_APPLY_Click);
            // 
            // BTN_RECOVER
            // 
            this.BTN_RECOVER.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.openfile;
            this.BTN_RECOVER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECOVER.Location = new System.Drawing.Point(622, 626);
            this.BTN_RECOVER.Name = "BTN_RECOVER";
            this.BTN_RECOVER.Size = new System.Drawing.Size(60, 60);
            this.BTN_RECOVER.TabIndex = 80;
            this.BTN_RECOVER.UseVisualStyleBackColor = true;
            this.BTN_RECOVER.Click += new System.EventHandler(this.BTN_RECOVER_Click);
            // 
            // Dlg_Tunning
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1373, 708);
            this.Controls.Add(this.uc_tunning_view);
            this.Controls.Add(this.TAB_TUNNING_TARGET);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_PTRN_APPLY);
            this.Controls.Add(this.BTN_RECOVER);
            this.Controls.Add(this.BTN_PARAM_WRITE);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Dlg_Tunning";
            this.Text = "PEAK INDEX MEASUREMENT";
            this.Load += new System.EventHandler(this.Dlg_Tunning_Load);
            this.TAB_TUNNING_TARGET.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TXT_RECT_CANDIDATE_COUNT)).EndInit();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BTN_PTRN_APPLY;
        private System.Windows.Forms.TabControl TAB_TUNNING_TARGET;
        private System.Windows.Forms.TabPage tabPage2;
        private CD_View.UC_CD_VIEWER uc_tunning_view;
        private System.Windows.Forms.Button BTN_RECOVER;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BTN_PARAM_WRITE;
        private NS_UC_THUMB_NAIL.UC_THUMB_NAIL uc_thumb_nail_rect;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox TXT_RECT_SELECTED_FIGURE;
        private System.Windows.Forms.TextBox TXT_RECT_SELECTED_INDEX;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox CHK_RECT_USE_AUTO_PEAK_DETECTION;
        private System.Windows.Forms.ComboBox CB_RECT_TARGET_INDEX_FST;
        private System.Windows.Forms.NumericUpDown TXT_RECT_CANDIDATE_COUNT;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BTN_CHECK_EDGE_DETECTION;
        private System.Windows.Forms.Button BTN_RECT_VERIFY_EDGE_REGION_FST;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.TextBox TXT_RECT_EDGE_DETEC_TARGET_INDEX_SCD;
        private System.Windows.Forms.TextBox TXT_RECT_EDGE_DETEC_TARGET_INDEX_FST;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Button BTN_RECT_VERIFY_EDGE_REGION_SCD;
        private System.Windows.Forms.ComboBox CB_RECT_TARGET_INDEX_SCD;
        private System.Windows.Forms.Button BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_SCD;
        private System.Windows.Forms.Button BTN_RECT_UPDATE_SELECTED_PEAK_INDEX_FST;
        private System.Windows.Forms.RadioButton RDO_RECT_APD_SCD;
        private System.Windows.Forms.RadioButton RDO_RECT_APD_FST;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton RDO_RECT_TYPE_DIA;
        private System.Windows.Forms.RadioButton RDO_RECT_TYPE_VER;
        private System.Windows.Forms.RadioButton RDO_RECT_TYPE_HOR;
    }
}
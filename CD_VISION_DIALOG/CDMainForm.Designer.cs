namespace CD_VISION_DIALOG
{
    partial class CDMainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CDMainForm));
            this.LV_FILE_LIST = new System.Windows.Forms.ListView();
            this.INDEX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FILES = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHK_MEASURE_DUMP = new System.Windows.Forms.CheckBox();
            this.CHK_MEASURE_VIEW_ONLY = new System.Windows.Forms.CheckBox();
            this.CHK_BLEND = new System.Windows.Forms.CheckBox();
            this.TB_BLENDING_RATIO = new System.Windows.Forms.TrackBar();
            this.LB_BLEND_VALUE = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.TAB_FIGURE = new System.Windows.Forms.TabControl();
            this.draw_Rect = new System.Windows.Forms.TabPage();
            this.LV_PAIR_DIG = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel19 = new System.Windows.Forms.Panel();
            this.RDO_TYPE_DIA = new System.Windows.Forms.RadioButton();
            this.RDO_TYPE_VER = new System.Windows.Forms.RadioButton();
            this.RDO_TYPE_HOR = new System.Windows.Forms.RadioButton();
            this.BTN_DIG_COPY = new System.Windows.Forms.Button();
            this.BTN_DIG_ADD = new System.Windows.Forms.Button();
            this.BTN_DIG_MODIFY = new System.Windows.Forms.Button();
            this.BTN_DIG_REMOVE = new System.Windows.Forms.Button();
            this.panel21 = new System.Windows.Forms.Panel();
            this.TXT_PARAM_DIA_ANGLE = new System.Windows.Forms.TextBox();
            this.LB_PARAM_DIA_ANGLE = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.TXT_RCD_H = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.BTN_DIA_ANGLE_DW = new System.Windows.Forms.Button();
            this.BTN_DIA_ANGLE_RV = new System.Windows.Forms.Button();
            this.label46 = new System.Windows.Forms.Label();
            this.TXT_RCD_W = new System.Windows.Forms.TextBox();
            this.BTN_DIA_ANGLE_UP = new System.Windows.Forms.Button();
            this.TXT_PARAM_DIG_NICK = new System.Windows.Forms.TextBox();
            this.draw_circle = new System.Windows.Forms.TabPage();
            this.LV_PAIR_CIR = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel22 = new System.Windows.Forms.Panel();
            this.BTN_CIR_ADD = new System.Windows.Forms.Button();
            this.BTN_CIR_COPY = new System.Windows.Forms.Button();
            this.BTN_CIR_MODIFY = new System.Windows.Forms.Button();
            this.BTN_CIR_REMOVE = new System.Windows.Forms.Button();
            this.panel24 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.TXT_CIRCLE_H = new System.Windows.Forms.TextBox();
            this.TXT_CIRCLE_W = new System.Windows.Forms.TextBox();
            this.TXT_PARAM_CIR_NICK = new System.Windows.Forms.TextBox();
            this.draw_ovl = new System.Windows.Forms.TabPage();
            this.LV_PAIR_OVL = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel28 = new System.Windows.Forms.Panel();
            this.label54 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.TXT_OVL_EX_H = new System.Windows.Forms.TextBox();
            this.TXT_OVL_IN_H = new System.Windows.Forms.TextBox();
            this.TXT_OVL_EX_W = new System.Windows.Forms.TextBox();
            this.TXT_OVL_IN_W = new System.Windows.Forms.TextBox();
            this.TXT_PARAM_OVL_NICK = new System.Windows.Forms.TextBox();
            this.panel23 = new System.Windows.Forms.Panel();
            this.RDO_ROI_OVL_EX = new System.Windows.Forms.RadioButton();
            this.RDO_ROI_OVL_ENTIRE = new System.Windows.Forms.RadioButton();
            this.RDO_ROI_OVL_IN = new System.Windows.Forms.RadioButton();
            this.BTN_OL_ADD = new System.Windows.Forms.Button();
            this.BTN_OL_COPY = new System.Windows.Forms.Button();
            this.BTN_OL_REMOVE = new System.Windows.Forms.Button();
            this.draw_matching = new System.Windows.Forms.TabPage();
            this.panel17 = new System.Windows.Forms.Panel();
            this.BTN_SET_FOCUS_ROI = new System.Windows.Forms.Button();
            this.BTN_DRAW_FOCUS_ROI = new System.Windows.Forms.Button();
            this.BTN_REMOVE_FOCUS_ROI = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.BTN_UPDATE_FIGURE_LIST = new System.Windows.Forms.Button();
            this.LV_RECP = new System.Windows.Forms.ListView();
            this.IDX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FIELS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label52 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.TXT_PTRN_ACC_RATIO = new System.Windows.Forms.TextBox();
            this.TXT_PATH_PTRN_FILE = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.TXT_PATH_RECP_FILE = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.LV_PARAMETER = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label31 = new System.Windows.Forms.Label();
            this.msg = new System.Windows.Forms.RichTextBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.BTN_FIGURE_MODIF_SCALE_PLUS = new System.Windows.Forms.Button();
            this.BTN_FIGURE_MODIF_SCALE_MINUS = new System.Windows.Forms.Button();
            this.RDO_ROI_ASYM = new System.Windows.Forms.RadioButton();
            this.RDO_ROI_SIZE = new System.Windows.Forms.RadioButton();
            this.TXT_FIGURE_CONTROL_SCALE = new System.Windows.Forms.TextBox();
            this.TB_FIGURE_CTRL_SCALE = new System.Windows.Forms.TrackBar();
            this.RDO_ROI_POSITION = new System.Windows.Forms.RadioButton();
            this.RDO_ROI_GAP = new System.Windows.Forms.RadioButton();
            this.label47 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.CHK_USE_HISTORY = new System.Windows.Forms.CheckBox();
            this.TAB_VIEW = new System.Windows.Forms.TabControl();
            this.tabpage33 = new System.Windows.Forms.TabPage();
            this.imageView1 = new CD_View.UC_CD_VIEWER();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageView2 = new CD_View.UC_CD_VIEWER();
            this.UC_LOG_VIEWER = new UC_LogView.UC_LOG_VIEWER();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TXT_BASE_RECP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TXT_FOCUS_POS_Y = new System.Windows.Forms.TextBox();
            this.TXT_PTRN_POS_ORG_Y = new System.Windows.Forms.TextBox();
            this.TXT_FOCUS_POS_X = new System.Windows.Forms.TextBox();
            this.TXT_PTRN_POS_ORG_X = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.LB_HACKER = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PNL_DRAW_FIGURE = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UC_Parameter = new CD_Paramter.pnl_parameter();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.PNL_MAIN = new System.Windows.Forms.Panel();
            this.CHK_USE_SAVE_PTRN_ERR = new System.Windows.Forms.CheckBox();
            this.CHK_USE_SAVE_INPUT = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTN_PTRN_RESULT = new System.Windows.Forms.Button();
            this.PNL_MEASURE_RESULT = new System.Windows.Forms.Panel();
            this.BTN_PLAY = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.BTN_GET_IMAGE_FROM_SEQUENCE = new System.Windows.Forms.Button();
            this.BTN_MEASURE = new System.Windows.Forms.Button();
            this.PIC_FOCUS = new System.Windows.Forms.PictureBox();
            this.PIC_PTRN = new System.Windows.Forms.PictureBox();
            this.BTN_RECIPE_SAVE = new System.Windows.Forms.Button();
            this.BTN_RECP_SAVE_AS = new System.Windows.Forms.Button();
            this.BTN_PTRN_MATCH = new System.Windows.Forms.Button();
            this.BTN_UPDATE_PARAMETER = new System.Windows.Forms.Button();
            this.BTN_RELOAD_PARAM_FIGURES = new System.Windows.Forms.Button();
            this.BTN_FIGURE_REMOVE_ALL = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BTN_MENU_STATISTICS = new System.Windows.Forms.Button();
            this.BTN_MAIN_LOGO = new System.Windows.Forms.Button();
            this.BTN_MENU_CREATE_RECP = new System.Windows.Forms.Button();
            this.BTN_MENU_IMAGE_PROCESSING = new System.Windows.Forms.Button();
            this.BTN_MENU_TUNNING = new System.Windows.Forms.Button();
            this.BTN_HACKER = new System.Windows.Forms.Button();
            this.BTN_MENU_RECP = new System.Windows.Forms.Button();
            this.BTN_MENU_HISTORY_MATCHING = new System.Windows.Forms.Button();
            this.BTN_MENU_CONFIG = new System.Windows.Forms.Button();
            this.BTN_MENU_HISTORY_MEASURE = new System.Windows.Forms.Button();
            this.BTN_MENU_PTRN = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TB_BLENDING_RATIO)).BeginInit();
            this.TAB_FIGURE.SuspendLayout();
            this.draw_Rect.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel21.SuspendLayout();
            this.draw_circle.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel24.SuspendLayout();
            this.draw_ovl.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel23.SuspendLayout();
            this.draw_matching.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_FIGURE_CTRL_SCALE)).BeginInit();
            this.TAB_VIEW.SuspendLayout();
            this.tabpage33.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.PNL_DRAW_FIGURE.SuspendLayout();
            this.PNL_MAIN.SuspendLayout();
            this.PNL_MEASURE_RESULT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_FOCUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_PTRN)).BeginInit();
            this.SuspendLayout();
            // 
            // LV_FILE_LIST
            // 
            this.LV_FILE_LIST.AllowDrop = true;
            this.LV_FILE_LIST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LV_FILE_LIST.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.INDEX,
            this.FILES});
            this.LV_FILE_LIST.ForeColor = System.Drawing.Color.Black;
            this.LV_FILE_LIST.FullRowSelect = true;
            this.LV_FILE_LIST.GridLines = true;
            this.LV_FILE_LIST.Location = new System.Drawing.Point(673, 799);
            this.LV_FILE_LIST.MultiSelect = false;
            this.LV_FILE_LIST.Name = "LV_FILE_LIST";
            this.LV_FILE_LIST.Size = new System.Drawing.Size(604, 237);
            this.LV_FILE_LIST.TabIndex = 10;
            this.LV_FILE_LIST.UseCompatibleStateImageBehavior = false;
            this.LV_FILE_LIST.View = System.Windows.Forms.View.Details;
            this.LV_FILE_LIST.SelectedIndexChanged += new System.EventHandler(this.LV_FILE_LIST_SelectedIndexChanged);
            this.LV_FILE_LIST.DragDrop += new System.Windows.Forms.DragEventHandler(this.LV_FILE_LIST_DragDrop);
            this.LV_FILE_LIST.DragEnter += new System.Windows.Forms.DragEventHandler(this.LV_FILE_LIST_DragEnter);
            this.LV_FILE_LIST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LV_FILE_LIST_KeyDown);
            // 
            // INDEX
            // 
            this.INDEX.Text = "IDX";
            // 
            // FILES
            // 
            this.FILES.Text = "FILES";
            this.FILES.Width = 523;
            // 
            // CHK_MEASURE_DUMP
            // 
            this.CHK_MEASURE_DUMP.AutoSize = true;
            this.CHK_MEASURE_DUMP.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CHK_MEASURE_DUMP.Location = new System.Drawing.Point(490, 136);
            this.CHK_MEASURE_DUMP.Name = "CHK_MEASURE_DUMP";
            this.CHK_MEASURE_DUMP.Size = new System.Drawing.Size(106, 18);
            this.CHK_MEASURE_DUMP.TabIndex = 11;
            this.CHK_MEASURE_DUMP.Text = "DUMP DATA";
            this.CHK_MEASURE_DUMP.UseVisualStyleBackColor = true;
            // 
            // CHK_MEASURE_VIEW_ONLY
            // 
            this.CHK_MEASURE_VIEW_ONLY.AutoSize = true;
            this.CHK_MEASURE_VIEW_ONLY.Checked = true;
            this.CHK_MEASURE_VIEW_ONLY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_MEASURE_VIEW_ONLY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CHK_MEASURE_VIEW_ONLY.Location = new System.Drawing.Point(490, 112);
            this.CHK_MEASURE_VIEW_ONLY.Name = "CHK_MEASURE_VIEW_ONLY";
            this.CHK_MEASURE_VIEW_ONLY.Size = new System.Drawing.Size(106, 18);
            this.CHK_MEASURE_VIEW_ONLY.TabIndex = 12;
            this.CHK_MEASURE_VIEW_ONLY.Text = "VIEW ONLY";
            this.CHK_MEASURE_VIEW_ONLY.UseVisualStyleBackColor = true;
            // 
            // CHK_BLEND
            // 
            this.CHK_BLEND.AutoSize = true;
            this.CHK_BLEND.Location = new System.Drawing.Point(5, 712);
            this.CHK_BLEND.Name = "CHK_BLEND";
            this.CHK_BLEND.Size = new System.Drawing.Size(15, 14);
            this.CHK_BLEND.TabIndex = 5;
            this.CHK_BLEND.UseVisualStyleBackColor = true;
            this.CHK_BLEND.CheckedChanged += new System.EventHandler(this.CHK_BLEND_CheckedChanged);
            // 
            // TB_BLENDING_RATIO
            // 
            this.TB_BLENDING_RATIO.Enabled = false;
            this.TB_BLENDING_RATIO.Location = new System.Drawing.Point(99, 696);
            this.TB_BLENDING_RATIO.Maximum = 100;
            this.TB_BLENDING_RATIO.Name = "TB_BLENDING_RATIO";
            this.TB_BLENDING_RATIO.Size = new System.Drawing.Size(430, 45);
            this.TB_BLENDING_RATIO.SmallChange = 5;
            this.TB_BLENDING_RATIO.TabIndex = 4;
            this.TB_BLENDING_RATIO.TickFrequency = 5;
            this.TB_BLENDING_RATIO.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TB_BLENDING_RATIO.Scroll += new System.EventHandler(this.TB_BLENDING_RATIO_Scroll);
            // 
            // LB_BLEND_VALUE
            // 
            this.LB_BLEND_VALUE.AutoSize = true;
            this.LB_BLEND_VALUE.ForeColor = System.Drawing.Color.Black;
            this.LB_BLEND_VALUE.Location = new System.Drawing.Point(556, 712);
            this.LB_BLEND_VALUE.Name = "LB_BLEND_VALUE";
            this.LB_BLEND_VALUE.Size = new System.Drawing.Size(35, 14);
            this.LB_BLEND_VALUE.TabIndex = 0;
            this.LB_BLEND_VALUE.Text = "0 %";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label30.Location = new System.Drawing.Point(26, 712);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(67, 14);
            this.label30.TabIndex = 0;
            this.label30.Text = "Blender :";
            // 
            // TAB_FIGURE
            // 
            this.TAB_FIGURE.Controls.Add(this.draw_Rect);
            this.TAB_FIGURE.Controls.Add(this.draw_circle);
            this.TAB_FIGURE.Controls.Add(this.draw_ovl);
            this.TAB_FIGURE.Controls.Add(this.draw_matching);
            this.TAB_FIGURE.ItemSize = new System.Drawing.Size(90, 35);
            this.TAB_FIGURE.Location = new System.Drawing.Point(3, 3);
            this.TAB_FIGURE.Multiline = true;
            this.TAB_FIGURE.Name = "TAB_FIGURE";
            this.TAB_FIGURE.SelectedIndex = 0;
            this.TAB_FIGURE.Size = new System.Drawing.Size(602, 275);
            this.TAB_FIGURE.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TAB_FIGURE.TabIndex = 37;
            this.TAB_FIGURE.SelectedIndexChanged += new System.EventHandler(this.TAB_FIGURE_SelectedIndexChanged);
            // 
            // draw_Rect
            // 
            this.draw_Rect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.draw_Rect.Controls.Add(this.LV_PAIR_DIG);
            this.draw_Rect.Controls.Add(this.panel19);
            this.draw_Rect.Controls.Add(this.panel21);
            this.draw_Rect.ImageIndex = 5;
            this.draw_Rect.Location = new System.Drawing.Point(4, 39);
            this.draw_Rect.Name = "draw_Rect";
            this.draw_Rect.Padding = new System.Windows.Forms.Padding(3);
            this.draw_Rect.Size = new System.Drawing.Size(594, 232);
            this.draw_Rect.TabIndex = 1;
            this.draw_Rect.Text = "RECT";
            // 
            // LV_PAIR_DIG
            // 
            this.LV_PAIR_DIG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LV_PAIR_DIG.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.LV_PAIR_DIG.ForeColor = System.Drawing.Color.Black;
            this.LV_PAIR_DIG.FullRowSelect = true;
            this.LV_PAIR_DIG.GridLines = true;
            this.LV_PAIR_DIG.Location = new System.Drawing.Point(245, 5);
            this.LV_PAIR_DIG.MultiSelect = false;
            this.LV_PAIR_DIG.Name = "LV_PAIR_DIG";
            this.LV_PAIR_DIG.Size = new System.Drawing.Size(343, 220);
            this.LV_PAIR_DIG.TabIndex = 18;
            this.LV_PAIR_DIG.UseCompatibleStateImageBehavior = false;
            this.LV_PAIR_DIG.View = System.Windows.Forms.View.Details;
            this.LV_PAIR_DIG.SelectedIndexChanged += new System.EventHandler(this.LV_PAIR_DIG_SelectedIndexChanged);
            this.LV_PAIR_DIG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LV_Figure_KeyDown);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "IDX";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "FIGURE";
            this.columnHeader8.Width = 230;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel19.Controls.Add(this.RDO_TYPE_DIA);
            this.panel19.Controls.Add(this.RDO_TYPE_VER);
            this.panel19.Controls.Add(this.RDO_TYPE_HOR);
            this.panel19.Controls.Add(this.BTN_DIG_COPY);
            this.panel19.Controls.Add(this.BTN_DIG_ADD);
            this.panel19.Controls.Add(this.BTN_DIG_MODIFY);
            this.panel19.Controls.Add(this.BTN_DIG_REMOVE);
            this.panel19.Location = new System.Drawing.Point(125, 5);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(115, 220);
            this.panel19.TabIndex = 20;
            // 
            // RDO_TYPE_DIA
            // 
            this.RDO_TYPE_DIA.AutoSize = true;
            this.RDO_TYPE_DIA.Location = new System.Drawing.Point(4, 192);
            this.RDO_TYPE_DIA.Name = "RDO_TYPE_DIA";
            this.RDO_TYPE_DIA.Size = new System.Drawing.Size(83, 18);
            this.RDO_TYPE_DIA.TabIndex = 5;
            this.RDO_TYPE_DIA.Text = "Diagonal";
            this.RDO_TYPE_DIA.UseVisualStyleBackColor = true;
            this.RDO_TYPE_DIA.CheckedChanged += new System.EventHandler(this.RDO_TYPE_DIA_CheckedChanged);
            // 
            // RDO_TYPE_VER
            // 
            this.RDO_TYPE_VER.AutoSize = true;
            this.RDO_TYPE_VER.Location = new System.Drawing.Point(4, 169);
            this.RDO_TYPE_VER.Name = "RDO_TYPE_VER";
            this.RDO_TYPE_VER.Size = new System.Drawing.Size(76, 18);
            this.RDO_TYPE_VER.TabIndex = 5;
            this.RDO_TYPE_VER.Text = "Vertical";
            this.RDO_TYPE_VER.UseVisualStyleBackColor = true;
            this.RDO_TYPE_VER.CheckedChanged += new System.EventHandler(this.RDO_TYPE_VER_CheckedChanged);
            // 
            // RDO_TYPE_HOR
            // 
            this.RDO_TYPE_HOR.AutoSize = true;
            this.RDO_TYPE_HOR.Checked = true;
            this.RDO_TYPE_HOR.Location = new System.Drawing.Point(4, 147);
            this.RDO_TYPE_HOR.Name = "RDO_TYPE_HOR";
            this.RDO_TYPE_HOR.Size = new System.Drawing.Size(93, 18);
            this.RDO_TYPE_HOR.TabIndex = 5;
            this.RDO_TYPE_HOR.TabStop = true;
            this.RDO_TYPE_HOR.Text = "Horizontal";
            this.RDO_TYPE_HOR.UseVisualStyleBackColor = true;
            this.RDO_TYPE_HOR.CheckedChanged += new System.EventHandler(this.RDO_TYPE_HOR_CheckedChanged);
            // 
            // BTN_DIG_COPY
            // 
            this.BTN_DIG_COPY.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BTN_DIG_COPY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_COPY.FlatAppearance.BorderSize = 3;
            this.BTN_DIG_COPY.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIG_COPY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_COPY.Location = new System.Drawing.Point(2, 40);
            this.BTN_DIG_COPY.Name = "BTN_DIG_COPY";
            this.BTN_DIG_COPY.Size = new System.Drawing.Size(110, 30);
            this.BTN_DIG_COPY.TabIndex = 4;
            this.BTN_DIG_COPY.Text = "COPY";
            this.BTN_DIG_COPY.UseVisualStyleBackColor = false;
            this.BTN_DIG_COPY.Click += new System.EventHandler(this.BTN_DIG_COPY_Click);
            // 
            // BTN_DIG_ADD
            // 
            this.BTN_DIG_ADD.BackColor = System.Drawing.Color.White;
            this.BTN_DIG_ADD.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_ADD.FlatAppearance.BorderSize = 3;
            this.BTN_DIG_ADD.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIG_ADD.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_ADD.Location = new System.Drawing.Point(2, 5);
            this.BTN_DIG_ADD.Name = "BTN_DIG_ADD";
            this.BTN_DIG_ADD.Size = new System.Drawing.Size(110, 30);
            this.BTN_DIG_ADD.TabIndex = 2;
            this.BTN_DIG_ADD.Text = "ADD";
            this.BTN_DIG_ADD.UseVisualStyleBackColor = false;
            this.BTN_DIG_ADD.Click += new System.EventHandler(this.BTN_DIG_ADD_Click);
            // 
            // BTN_DIG_MODIFY
            // 
            this.BTN_DIG_MODIFY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_MODIFY.FlatAppearance.BorderSize = 3;
            this.BTN_DIG_MODIFY.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIG_MODIFY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_MODIFY.Location = new System.Drawing.Point(2, 75);
            this.BTN_DIG_MODIFY.Name = "BTN_DIG_MODIFY";
            this.BTN_DIG_MODIFY.Size = new System.Drawing.Size(110, 30);
            this.BTN_DIG_MODIFY.TabIndex = 2;
            this.BTN_DIG_MODIFY.Text = "MODIFY";
            this.BTN_DIG_MODIFY.UseVisualStyleBackColor = true;
            this.BTN_DIG_MODIFY.Click += new System.EventHandler(this.BTN_DIG_MODIFY_Click);
            // 
            // BTN_DIG_REMOVE
            // 
            this.BTN_DIG_REMOVE.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_REMOVE.FlatAppearance.BorderSize = 3;
            this.BTN_DIG_REMOVE.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIG_REMOVE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DIG_REMOVE.Location = new System.Drawing.Point(2, 111);
            this.BTN_DIG_REMOVE.Name = "BTN_DIG_REMOVE";
            this.BTN_DIG_REMOVE.Size = new System.Drawing.Size(110, 30);
            this.BTN_DIG_REMOVE.TabIndex = 2;
            this.BTN_DIG_REMOVE.TabStop = false;
            this.BTN_DIG_REMOVE.Text = "REMOVE";
            this.BTN_DIG_REMOVE.UseVisualStyleBackColor = true;
            this.BTN_DIG_REMOVE.Click += new System.EventHandler(this.BTN_DIG_REMOVE_Click);
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel21.Controls.Add(this.TXT_PARAM_DIA_ANGLE);
            this.panel21.Controls.Add(this.LB_PARAM_DIA_ANGLE);
            this.panel21.Controls.Add(this.label44);
            this.panel21.Controls.Add(this.TXT_RCD_H);
            this.panel21.Controls.Add(this.label45);
            this.panel21.Controls.Add(this.BTN_DIA_ANGLE_DW);
            this.panel21.Controls.Add(this.BTN_DIA_ANGLE_RV);
            this.panel21.Controls.Add(this.label46);
            this.panel21.Controls.Add(this.TXT_RCD_W);
            this.panel21.Controls.Add(this.BTN_DIA_ANGLE_UP);
            this.panel21.Controls.Add(this.button5);
            this.panel21.Controls.Add(this.TXT_PARAM_DIG_NICK);
            this.panel21.Location = new System.Drawing.Point(5, 5);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(115, 220);
            this.panel21.TabIndex = 19;
            // 
            // TXT_PARAM_DIA_ANGLE
            // 
            this.TXT_PARAM_DIA_ANGLE.BackColor = System.Drawing.Color.White;
            this.TXT_PARAM_DIA_ANGLE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXT_PARAM_DIA_ANGLE.ForeColor = System.Drawing.Color.Black;
            this.TXT_PARAM_DIA_ANGLE.Location = new System.Drawing.Point(51, 81);
            this.TXT_PARAM_DIA_ANGLE.Name = "TXT_PARAM_DIA_ANGLE";
            this.TXT_PARAM_DIA_ANGLE.Size = new System.Drawing.Size(37, 15);
            this.TXT_PARAM_DIA_ANGLE.TabIndex = 8;
            this.TXT_PARAM_DIA_ANGLE.Text = "0";
            this.TXT_PARAM_DIA_ANGLE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TXT_PARAM_DIA_ANGLE.Visible = false;
            // 
            // LB_PARAM_DIA_ANGLE
            // 
            this.LB_PARAM_DIA_ANGLE.AutoSize = true;
            this.LB_PARAM_DIA_ANGLE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LB_PARAM_DIA_ANGLE.Location = new System.Drawing.Point(23, 82);
            this.LB_PARAM_DIA_ANGLE.Name = "LB_PARAM_DIA_ANGLE";
            this.LB_PARAM_DIA_ANGLE.Size = new System.Drawing.Size(24, 14);
            this.LB_PARAM_DIA_ANGLE.TabIndex = 0;
            this.LB_PARAM_DIA_ANGLE.Text = "θ :";
            this.LB_PARAM_DIA_ANGLE.Visible = false;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label44.Location = new System.Drawing.Point(5, 140);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(50, 14);
            this.label44.TabIndex = 7;
            this.label44.Text = "NICK :";
            // 
            // TXT_RCD_H
            // 
            this.TXT_RCD_H.BackColor = System.Drawing.Color.White;
            this.TXT_RCD_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_RCD_H.ForeColor = System.Drawing.Color.Black;
            this.TXT_RCD_H.Location = new System.Drawing.Point(58, 192);
            this.TXT_RCD_H.Name = "TXT_RCD_H";
            this.TXT_RCD_H.Size = new System.Drawing.Size(54, 22);
            this.TXT_RCD_H.TabIndex = 26;
            this.TXT_RCD_H.Text = "0";
            this.TXT_RCD_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label45.Location = new System.Drawing.Point(5, 167);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(51, 14);
            this.label45.TabIndex = 8;
            this.label45.Text = "SZ_X :";
            // 
            // BTN_DIA_ANGLE_DW
            // 
            this.BTN_DIA_ANGLE_DW.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIA_ANGLE_DW.Location = new System.Drawing.Point(77, 111);
            this.BTN_DIA_ANGLE_DW.Name = "BTN_DIA_ANGLE_DW";
            this.BTN_DIA_ANGLE_DW.Size = new System.Drawing.Size(35, 23);
            this.BTN_DIA_ANGLE_DW.TabIndex = 9;
            this.BTN_DIA_ANGLE_DW.Text = "↓";
            this.BTN_DIA_ANGLE_DW.UseVisualStyleBackColor = true;
            this.BTN_DIA_ANGLE_DW.Visible = false;
            this.BTN_DIA_ANGLE_DW.Click += new System.EventHandler(this.BTN_DIG_ANGLE_DW_Click);
            // 
            // BTN_DIA_ANGLE_RV
            // 
            this.BTN_DIA_ANGLE_RV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIA_ANGLE_RV.Location = new System.Drawing.Point(40, 111);
            this.BTN_DIA_ANGLE_RV.Name = "BTN_DIA_ANGLE_RV";
            this.BTN_DIA_ANGLE_RV.Size = new System.Drawing.Size(35, 23);
            this.BTN_DIA_ANGLE_RV.TabIndex = 9;
            this.BTN_DIA_ANGLE_RV.Text = "↔";
            this.BTN_DIA_ANGLE_RV.UseVisualStyleBackColor = true;
            this.BTN_DIA_ANGLE_RV.Visible = false;
            this.BTN_DIA_ANGLE_RV.Click += new System.EventHandler(this.BTN_DIG_ANGLE_RV_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label46.Location = new System.Drawing.Point(5, 194);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(52, 14);
            this.label46.TabIndex = 9;
            this.label46.Text = "SZ_Y :";
            // 
            // TXT_RCD_W
            // 
            this.TXT_RCD_W.BackColor = System.Drawing.Color.White;
            this.TXT_RCD_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_RCD_W.ForeColor = System.Drawing.Color.Black;
            this.TXT_RCD_W.Location = new System.Drawing.Point(58, 165);
            this.TXT_RCD_W.Name = "TXT_RCD_W";
            this.TXT_RCD_W.Size = new System.Drawing.Size(54, 22);
            this.TXT_RCD_W.TabIndex = 27;
            this.TXT_RCD_W.Text = "0";
            this.TXT_RCD_W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BTN_DIA_ANGLE_UP
            // 
            this.BTN_DIA_ANGLE_UP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_DIA_ANGLE_UP.Location = new System.Drawing.Point(2, 111);
            this.BTN_DIA_ANGLE_UP.Name = "BTN_DIA_ANGLE_UP";
            this.BTN_DIA_ANGLE_UP.Size = new System.Drawing.Size(35, 23);
            this.BTN_DIA_ANGLE_UP.TabIndex = 9;
            this.BTN_DIA_ANGLE_UP.Text = "↑";
            this.BTN_DIA_ANGLE_UP.UseVisualStyleBackColor = true;
            this.BTN_DIA_ANGLE_UP.Visible = false;
            this.BTN_DIA_ANGLE_UP.Click += new System.EventHandler(this.BTN_DIG_ANGLE_UP_Click);
            // 
            // TXT_PARAM_DIG_NICK
            // 
            this.TXT_PARAM_DIG_NICK.BackColor = System.Drawing.Color.White;
            this.TXT_PARAM_DIG_NICK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PARAM_DIG_NICK.ForeColor = System.Drawing.Color.Black;
            this.TXT_PARAM_DIG_NICK.Location = new System.Drawing.Point(58, 138);
            this.TXT_PARAM_DIG_NICK.Name = "TXT_PARAM_DIG_NICK";
            this.TXT_PARAM_DIG_NICK.Size = new System.Drawing.Size(54, 22);
            this.TXT_PARAM_DIG_NICK.TabIndex = 7;
            this.TXT_PARAM_DIG_NICK.Text = "RECT";
            this.TXT_PARAM_DIG_NICK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // draw_circle
            // 
            this.draw_circle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.draw_circle.Controls.Add(this.LV_PAIR_CIR);
            this.draw_circle.Controls.Add(this.panel22);
            this.draw_circle.Controls.Add(this.panel24);
            this.draw_circle.ImageKey = "measureCir.png";
            this.draw_circle.Location = new System.Drawing.Point(4, 39);
            this.draw_circle.Name = "draw_circle";
            this.draw_circle.Size = new System.Drawing.Size(594, 232);
            this.draw_circle.TabIndex = 4;
            this.draw_circle.Text = "CIRCLE";
            // 
            // LV_PAIR_CIR
            // 
            this.LV_PAIR_CIR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LV_PAIR_CIR.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.LV_PAIR_CIR.ForeColor = System.Drawing.Color.Black;
            this.LV_PAIR_CIR.FullRowSelect = true;
            this.LV_PAIR_CIR.GridLines = true;
            this.LV_PAIR_CIR.Location = new System.Drawing.Point(245, 5);
            this.LV_PAIR_CIR.MultiSelect = false;
            this.LV_PAIR_CIR.Name = "LV_PAIR_CIR";
            this.LV_PAIR_CIR.Size = new System.Drawing.Size(346, 220);
            this.LV_PAIR_CIR.TabIndex = 25;
            this.LV_PAIR_CIR.UseCompatibleStateImageBehavior = false;
            this.LV_PAIR_CIR.View = System.Windows.Forms.View.Details;
            this.LV_PAIR_CIR.SelectedIndexChanged += new System.EventHandler(this.LV_PAIR_CIR_SelectedIndexChanged);
            this.LV_PAIR_CIR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LV_Figure_KeyDown);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "IDX";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "FIGURE";
            this.columnHeader10.Width = 150;
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel22.Controls.Add(this.BTN_CIR_ADD);
            this.panel22.Controls.Add(this.BTN_CIR_COPY);
            this.panel22.Controls.Add(this.BTN_CIR_MODIFY);
            this.panel22.Controls.Add(this.BTN_CIR_REMOVE);
            this.panel22.Location = new System.Drawing.Point(125, 5);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(115, 220);
            this.panel22.TabIndex = 27;
            // 
            // BTN_CIR_ADD
            // 
            this.BTN_CIR_ADD.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_ADD.FlatAppearance.BorderSize = 3;
            this.BTN_CIR_ADD.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_CIR_ADD.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_ADD.Location = new System.Drawing.Point(2, 5);
            this.BTN_CIR_ADD.Name = "BTN_CIR_ADD";
            this.BTN_CIR_ADD.Size = new System.Drawing.Size(110, 30);
            this.BTN_CIR_ADD.TabIndex = 2;
            this.BTN_CIR_ADD.Text = "ADD";
            this.BTN_CIR_ADD.UseVisualStyleBackColor = true;
            this.BTN_CIR_ADD.Click += new System.EventHandler(this.BTN_CIR_ADD_Click);
            // 
            // BTN_CIR_COPY
            // 
            this.BTN_CIR_COPY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_COPY.FlatAppearance.BorderSize = 3;
            this.BTN_CIR_COPY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_CIR_COPY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_COPY.Location = new System.Drawing.Point(2, 40);
            this.BTN_CIR_COPY.Name = "BTN_CIR_COPY";
            this.BTN_CIR_COPY.Size = new System.Drawing.Size(110, 30);
            this.BTN_CIR_COPY.TabIndex = 2;
            this.BTN_CIR_COPY.Text = "COPY";
            this.BTN_CIR_COPY.UseVisualStyleBackColor = true;
            this.BTN_CIR_COPY.Click += new System.EventHandler(this.BTN_CIR_COPY_Click);
            // 
            // BTN_CIR_MODIFY
            // 
            this.BTN_CIR_MODIFY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_MODIFY.FlatAppearance.BorderSize = 3;
            this.BTN_CIR_MODIFY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_CIR_MODIFY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_MODIFY.Location = new System.Drawing.Point(2, 75);
            this.BTN_CIR_MODIFY.Name = "BTN_CIR_MODIFY";
            this.BTN_CIR_MODIFY.Size = new System.Drawing.Size(110, 30);
            this.BTN_CIR_MODIFY.TabIndex = 2;
            this.BTN_CIR_MODIFY.Text = "MODIFY";
            this.BTN_CIR_MODIFY.UseVisualStyleBackColor = true;
            this.BTN_CIR_MODIFY.Click += new System.EventHandler(this.BTN_CIR_MODIFY_Click);
            // 
            // BTN_CIR_REMOVE
            // 
            this.BTN_CIR_REMOVE.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_REMOVE.FlatAppearance.BorderSize = 3;
            this.BTN_CIR_REMOVE.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_CIR_REMOVE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_CIR_REMOVE.Location = new System.Drawing.Point(2, 111);
            this.BTN_CIR_REMOVE.Name = "BTN_CIR_REMOVE";
            this.BTN_CIR_REMOVE.Size = new System.Drawing.Size(110, 30);
            this.BTN_CIR_REMOVE.TabIndex = 2;
            this.BTN_CIR_REMOVE.TabStop = false;
            this.BTN_CIR_REMOVE.Text = "REMOVE";
            this.BTN_CIR_REMOVE.UseVisualStyleBackColor = true;
            this.BTN_CIR_REMOVE.Click += new System.EventHandler(this.BTN_CIR_REMOVE_Click);
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel24.Controls.Add(this.label48);
            this.panel24.Controls.Add(this.label49);
            this.panel24.Controls.Add(this.label50);
            this.panel24.Controls.Add(this.TXT_CIRCLE_H);
            this.panel24.Controls.Add(this.TXT_CIRCLE_W);
            this.panel24.Controls.Add(this.button9);
            this.panel24.Controls.Add(this.TXT_PARAM_CIR_NICK);
            this.panel24.Location = new System.Drawing.Point(5, 5);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(115, 220);
            this.panel24.TabIndex = 26;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label48.Location = new System.Drawing.Point(5, 140);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(50, 14);
            this.label48.TabIndex = 35;
            this.label48.Text = "NICK :";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label49.Location = new System.Drawing.Point(5, 167);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(51, 14);
            this.label49.TabIndex = 36;
            this.label49.Text = "SZ_X :";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label50.Location = new System.Drawing.Point(5, 194);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(52, 14);
            this.label50.TabIndex = 37;
            this.label50.Text = "SZ_Y :";
            // 
            // TXT_CIRCLE_H
            // 
            this.TXT_CIRCLE_H.BackColor = System.Drawing.Color.White;
            this.TXT_CIRCLE_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_CIRCLE_H.ForeColor = System.Drawing.Color.Black;
            this.TXT_CIRCLE_H.Location = new System.Drawing.Point(58, 192);
            this.TXT_CIRCLE_H.Name = "TXT_CIRCLE_H";
            this.TXT_CIRCLE_H.Size = new System.Drawing.Size(54, 22);
            this.TXT_CIRCLE_H.TabIndex = 33;
            this.TXT_CIRCLE_H.Text = "0";
            this.TXT_CIRCLE_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_CIRCLE_W
            // 
            this.TXT_CIRCLE_W.BackColor = System.Drawing.Color.White;
            this.TXT_CIRCLE_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_CIRCLE_W.ForeColor = System.Drawing.Color.Black;
            this.TXT_CIRCLE_W.Location = new System.Drawing.Point(58, 165);
            this.TXT_CIRCLE_W.Name = "TXT_CIRCLE_W";
            this.TXT_CIRCLE_W.Size = new System.Drawing.Size(54, 22);
            this.TXT_CIRCLE_W.TabIndex = 34;
            this.TXT_CIRCLE_W.Text = "0";
            this.TXT_CIRCLE_W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_PARAM_CIR_NICK
            // 
            this.TXT_PARAM_CIR_NICK.BackColor = System.Drawing.Color.White;
            this.TXT_PARAM_CIR_NICK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PARAM_CIR_NICK.ForeColor = System.Drawing.Color.Black;
            this.TXT_PARAM_CIR_NICK.Location = new System.Drawing.Point(58, 138);
            this.TXT_PARAM_CIR_NICK.Name = "TXT_PARAM_CIR_NICK";
            this.TXT_PARAM_CIR_NICK.Size = new System.Drawing.Size(54, 22);
            this.TXT_PARAM_CIR_NICK.TabIndex = 7;
            this.TXT_PARAM_CIR_NICK.Text = "circle";
            this.TXT_PARAM_CIR_NICK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // draw_ovl
            // 
            this.draw_ovl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.draw_ovl.Controls.Add(this.LV_PAIR_OVL);
            this.draw_ovl.Controls.Add(this.panel28);
            this.draw_ovl.Controls.Add(this.panel23);
            this.draw_ovl.Location = new System.Drawing.Point(4, 39);
            this.draw_ovl.Name = "draw_ovl";
            this.draw_ovl.Size = new System.Drawing.Size(594, 232);
            this.draw_ovl.TabIndex = 3;
            this.draw_ovl.Text = "OVL";
            // 
            // LV_PAIR_OVL
            // 
            this.LV_PAIR_OVL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LV_PAIR_OVL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LV_PAIR_OVL.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.LV_PAIR_OVL.ForeColor = System.Drawing.Color.Black;
            this.LV_PAIR_OVL.FullRowSelect = true;
            this.LV_PAIR_OVL.GridLines = true;
            this.LV_PAIR_OVL.Location = new System.Drawing.Point(245, 5);
            this.LV_PAIR_OVL.MultiSelect = false;
            this.LV_PAIR_OVL.Name = "LV_PAIR_OVL";
            this.LV_PAIR_OVL.Size = new System.Drawing.Size(346, 220);
            this.LV_PAIR_OVL.TabIndex = 29;
            this.LV_PAIR_OVL.UseCompatibleStateImageBehavior = false;
            this.LV_PAIR_OVL.View = System.Windows.Forms.View.Details;
            this.LV_PAIR_OVL.SelectedIndexChanged += new System.EventHandler(this.LV_PAIR_OVL_SelectedIndexChanged);
            this.LV_PAIR_OVL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LV_Figure_KeyDown);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "IDX";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "FIGURE";
            this.columnHeader12.Width = 150;
            // 
            // panel28
            // 
            this.panel28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel28.Controls.Add(this.label54);
            this.panel28.Controls.Add(this.label58);
            this.panel28.Controls.Add(this.label55);
            this.panel28.Controls.Add(this.label57);
            this.panel28.Controls.Add(this.label56);
            this.panel28.Controls.Add(this.TXT_OVL_EX_H);
            this.panel28.Controls.Add(this.TXT_OVL_IN_H);
            this.panel28.Controls.Add(this.TXT_OVL_EX_W);
            this.panel28.Controls.Add(this.TXT_OVL_IN_W);
            this.panel28.Controls.Add(this.button3);
            this.panel28.Controls.Add(this.TXT_PARAM_OVL_NICK);
            this.panel28.Location = new System.Drawing.Point(5, 5);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(115, 220);
            this.panel28.TabIndex = 30;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.label54.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label54.Location = new System.Drawing.Point(9, 114);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(50, 14);
            this.label54.TabIndex = 35;
            this.label54.Text = "NICK :";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label58.Location = new System.Drawing.Point(3, 178);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(59, 14);
            this.label58.TabIndex = 36;
            this.label58.Text = "SZ_EX :";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label55.Location = new System.Drawing.Point(3, 135);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(57, 14);
            this.label55.TabIndex = 36;
            this.label55.Text = "SZ_IX :";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label57.Location = new System.Drawing.Point(3, 200);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(60, 14);
            this.label57.TabIndex = 37;
            this.label57.Text = "SZ_EY :";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label56.Location = new System.Drawing.Point(3, 157);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(58, 14);
            this.label56.TabIndex = 37;
            this.label56.Text = "SZ_IY :";
            // 
            // TXT_OVL_EX_H
            // 
            this.TXT_OVL_EX_H.BackColor = System.Drawing.Color.White;
            this.TXT_OVL_EX_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_OVL_EX_H.ForeColor = System.Drawing.Color.Black;
            this.TXT_OVL_EX_H.Location = new System.Drawing.Point(63, 195);
            this.TXT_OVL_EX_H.Name = "TXT_OVL_EX_H";
            this.TXT_OVL_EX_H.Size = new System.Drawing.Size(50, 22);
            this.TXT_OVL_EX_H.TabIndex = 33;
            this.TXT_OVL_EX_H.Text = "0";
            this.TXT_OVL_EX_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_OVL_IN_H
            // 
            this.TXT_OVL_IN_H.BackColor = System.Drawing.Color.White;
            this.TXT_OVL_IN_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_OVL_IN_H.ForeColor = System.Drawing.Color.Black;
            this.TXT_OVL_IN_H.Location = new System.Drawing.Point(63, 153);
            this.TXT_OVL_IN_H.Name = "TXT_OVL_IN_H";
            this.TXT_OVL_IN_H.Size = new System.Drawing.Size(50, 22);
            this.TXT_OVL_IN_H.TabIndex = 33;
            this.TXT_OVL_IN_H.Text = "0";
            this.TXT_OVL_IN_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_OVL_EX_W
            // 
            this.TXT_OVL_EX_W.BackColor = System.Drawing.Color.White;
            this.TXT_OVL_EX_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_OVL_EX_W.ForeColor = System.Drawing.Color.Black;
            this.TXT_OVL_EX_W.Location = new System.Drawing.Point(63, 174);
            this.TXT_OVL_EX_W.Name = "TXT_OVL_EX_W";
            this.TXT_OVL_EX_W.Size = new System.Drawing.Size(50, 22);
            this.TXT_OVL_EX_W.TabIndex = 34;
            this.TXT_OVL_EX_W.Text = "0";
            this.TXT_OVL_EX_W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_OVL_IN_W
            // 
            this.TXT_OVL_IN_W.BackColor = System.Drawing.Color.White;
            this.TXT_OVL_IN_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_OVL_IN_W.ForeColor = System.Drawing.Color.Black;
            this.TXT_OVL_IN_W.Location = new System.Drawing.Point(63, 132);
            this.TXT_OVL_IN_W.Name = "TXT_OVL_IN_W";
            this.TXT_OVL_IN_W.Size = new System.Drawing.Size(50, 22);
            this.TXT_OVL_IN_W.TabIndex = 34;
            this.TXT_OVL_IN_W.Text = "0";
            this.TXT_OVL_IN_W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_PARAM_OVL_NICK
            // 
            this.TXT_PARAM_OVL_NICK.BackColor = System.Drawing.Color.White;
            this.TXT_PARAM_OVL_NICK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PARAM_OVL_NICK.ForeColor = System.Drawing.Color.Black;
            this.TXT_PARAM_OVL_NICK.Location = new System.Drawing.Point(63, 111);
            this.TXT_PARAM_OVL_NICK.Name = "TXT_PARAM_OVL_NICK";
            this.TXT_PARAM_OVL_NICK.Size = new System.Drawing.Size(50, 22);
            this.TXT_PARAM_OVL_NICK.TabIndex = 7;
            this.TXT_PARAM_OVL_NICK.Text = "OVL";
            this.TXT_PARAM_OVL_NICK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel23.Controls.Add(this.RDO_ROI_OVL_EX);
            this.panel23.Controls.Add(this.RDO_ROI_OVL_ENTIRE);
            this.panel23.Controls.Add(this.RDO_ROI_OVL_IN);
            this.panel23.Controls.Add(this.BTN_OL_ADD);
            this.panel23.Controls.Add(this.BTN_OL_COPY);
            this.panel23.Controls.Add(this.BTN_OL_REMOVE);
            this.panel23.Location = new System.Drawing.Point(125, 5);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(115, 220);
            this.panel23.TabIndex = 28;
            // 
            // RDO_ROI_OVL_EX
            // 
            this.RDO_ROI_OVL_EX.AutoSize = true;
            this.RDO_ROI_OVL_EX.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_OVL_EX.Location = new System.Drawing.Point(10, 197);
            this.RDO_ROI_OVL_EX.Name = "RDO_ROI_OVL_EX";
            this.RDO_ROI_OVL_EX.Size = new System.Drawing.Size(94, 18);
            this.RDO_ROI_OVL_EX.TabIndex = 2;
            this.RDO_ROI_OVL_EX.Text = "EXTERNAL";
            this.RDO_ROI_OVL_EX.UseVisualStyleBackColor = true;
            // 
            // RDO_ROI_OVL_ENTIRE
            // 
            this.RDO_ROI_OVL_ENTIRE.AutoSize = true;
            this.RDO_ROI_OVL_ENTIRE.Checked = true;
            this.RDO_ROI_OVL_ENTIRE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_OVL_ENTIRE.Location = new System.Drawing.Point(10, 151);
            this.RDO_ROI_OVL_ENTIRE.Name = "RDO_ROI_OVL_ENTIRE";
            this.RDO_ROI_OVL_ENTIRE.Size = new System.Drawing.Size(74, 18);
            this.RDO_ROI_OVL_ENTIRE.TabIndex = 2;
            this.RDO_ROI_OVL_ENTIRE.TabStop = true;
            this.RDO_ROI_OVL_ENTIRE.Text = "ENTIRE";
            this.RDO_ROI_OVL_ENTIRE.UseVisualStyleBackColor = true;
            // 
            // RDO_ROI_OVL_IN
            // 
            this.RDO_ROI_OVL_IN.AutoSize = true;
            this.RDO_ROI_OVL_IN.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_OVL_IN.Location = new System.Drawing.Point(10, 173);
            this.RDO_ROI_OVL_IN.Name = "RDO_ROI_OVL_IN";
            this.RDO_ROI_OVL_IN.Size = new System.Drawing.Size(93, 18);
            this.RDO_ROI_OVL_IN.TabIndex = 2;
            this.RDO_ROI_OVL_IN.Text = "INTERNAL";
            this.RDO_ROI_OVL_IN.UseVisualStyleBackColor = true;
            // 
            // BTN_OL_ADD
            // 
            this.BTN_OL_ADD.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_OL_ADD.FlatAppearance.BorderSize = 3;
            this.BTN_OL_ADD.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_OL_ADD.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_OL_ADD.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_OL_ADD.Location = new System.Drawing.Point(2, 5);
            this.BTN_OL_ADD.Name = "BTN_OL_ADD";
            this.BTN_OL_ADD.Size = new System.Drawing.Size(110, 30);
            this.BTN_OL_ADD.TabIndex = 2;
            this.BTN_OL_ADD.TabStop = false;
            this.BTN_OL_ADD.Text = "ADD";
            this.BTN_OL_ADD.UseVisualStyleBackColor = true;
            this.BTN_OL_ADD.Click += new System.EventHandler(this.BTN_OL_ADD_Click);
            // 
            // BTN_OL_COPY
            // 
            this.BTN_OL_COPY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_OL_COPY.FlatAppearance.BorderSize = 3;
            this.BTN_OL_COPY.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_OL_COPY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_OL_COPY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_OL_COPY.Location = new System.Drawing.Point(2, 40);
            this.BTN_OL_COPY.Name = "BTN_OL_COPY";
            this.BTN_OL_COPY.Size = new System.Drawing.Size(110, 30);
            this.BTN_OL_COPY.TabIndex = 2;
            this.BTN_OL_COPY.TabStop = false;
            this.BTN_OL_COPY.Text = "COPY";
            this.BTN_OL_COPY.UseVisualStyleBackColor = true;
            this.BTN_OL_COPY.Click += new System.EventHandler(this.BTN_OL_COPY_Click);
            // 
            // BTN_OL_REMOVE
            // 
            this.BTN_OL_REMOVE.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_OL_REMOVE.FlatAppearance.BorderSize = 3;
            this.BTN_OL_REMOVE.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_OL_REMOVE.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_OL_REMOVE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_OL_REMOVE.Location = new System.Drawing.Point(2, 75);
            this.BTN_OL_REMOVE.Name = "BTN_OL_REMOVE";
            this.BTN_OL_REMOVE.Size = new System.Drawing.Size(110, 30);
            this.BTN_OL_REMOVE.TabIndex = 2;
            this.BTN_OL_REMOVE.TabStop = false;
            this.BTN_OL_REMOVE.Text = "REMOVE";
            this.BTN_OL_REMOVE.UseVisualStyleBackColor = true;
            this.BTN_OL_REMOVE.Click += new System.EventHandler(this.BTN_OL_REMOVE_Click);
            // 
            // draw_matching
            // 
            this.draw_matching.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.draw_matching.Controls.Add(this.panel17);
            this.draw_matching.Controls.Add(this.panel13);
            this.draw_matching.Location = new System.Drawing.Point(4, 39);
            this.draw_matching.Name = "draw_matching";
            this.draw_matching.Size = new System.Drawing.Size(594, 232);
            this.draw_matching.TabIndex = 5;
            this.draw_matching.Text = "FOCUS ROI";
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel17.Controls.Add(this.BTN_SET_FOCUS_ROI);
            this.panel17.Controls.Add(this.BTN_DRAW_FOCUS_ROI);
            this.panel17.Controls.Add(this.BTN_REMOVE_FOCUS_ROI);
            this.panel17.ForeColor = System.Drawing.SystemColors.Desktop;
            this.panel17.Location = new System.Drawing.Point(125, 5);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(466, 220);
            this.panel17.TabIndex = 30;
            // 
            // BTN_SET_FOCUS_ROI
            // 
            this.BTN_SET_FOCUS_ROI.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_SET_FOCUS_ROI.FlatAppearance.BorderSize = 3;
            this.BTN_SET_FOCUS_ROI.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_SET_FOCUS_ROI.Location = new System.Drawing.Point(104, 5);
            this.BTN_SET_FOCUS_ROI.Name = "BTN_SET_FOCUS_ROI";
            this.BTN_SET_FOCUS_ROI.Size = new System.Drawing.Size(90, 45);
            this.BTN_SET_FOCUS_ROI.TabIndex = 1;
            this.BTN_SET_FOCUS_ROI.Text = "SET";
            this.BTN_SET_FOCUS_ROI.UseVisualStyleBackColor = true;
            this.BTN_SET_FOCUS_ROI.Click += new System.EventHandler(this.BTN_SET_FOCUS_ROI_Click);
            // 
            // BTN_DRAW_FOCUS_ROI
            // 
            this.BTN_DRAW_FOCUS_ROI.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DRAW_FOCUS_ROI.FlatAppearance.BorderSize = 3;
            this.BTN_DRAW_FOCUS_ROI.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_DRAW_FOCUS_ROI.Location = new System.Drawing.Point(8, 5);
            this.BTN_DRAW_FOCUS_ROI.Name = "BTN_DRAW_FOCUS_ROI";
            this.BTN_DRAW_FOCUS_ROI.Size = new System.Drawing.Size(90, 45);
            this.BTN_DRAW_FOCUS_ROI.TabIndex = 1;
            this.BTN_DRAW_FOCUS_ROI.Text = "DRAW";
            this.BTN_DRAW_FOCUS_ROI.UseVisualStyleBackColor = true;
            this.BTN_DRAW_FOCUS_ROI.Click += new System.EventHandler(this.BTN_DRAW_FOCUS_ROI_Click);
            // 
            // BTN_REMOVE_FOCUS_ROI
            // 
            this.BTN_REMOVE_FOCUS_ROI.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_REMOVE_FOCUS_ROI.FlatAppearance.BorderSize = 3;
            this.BTN_REMOVE_FOCUS_ROI.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_REMOVE_FOCUS_ROI.Location = new System.Drawing.Point(200, 5);
            this.BTN_REMOVE_FOCUS_ROI.Name = "BTN_REMOVE_FOCUS_ROI";
            this.BTN_REMOVE_FOCUS_ROI.Size = new System.Drawing.Size(91, 45);
            this.BTN_REMOVE_FOCUS_ROI.TabIndex = 1;
            this.BTN_REMOVE_FOCUS_ROI.Text = "REMOVE";
            this.BTN_REMOVE_FOCUS_ROI.UseVisualStyleBackColor = true;
            this.BTN_REMOVE_FOCUS_ROI.Click += new System.EventHandler(this.BTN_REMOVE_FOCUS_ROI_Click);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.button2);
            this.panel13.Location = new System.Drawing.Point(5, 5);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(115, 220);
            this.panel13.TabIndex = 29;
            // 
            // BTN_UPDATE_FIGURE_LIST
            // 
            this.BTN_UPDATE_FIGURE_LIST.Location = new System.Drawing.Point(383, 256);
            this.BTN_UPDATE_FIGURE_LIST.Name = "BTN_UPDATE_FIGURE_LIST";
            this.BTN_UPDATE_FIGURE_LIST.Size = new System.Drawing.Size(189, 50);
            this.BTN_UPDATE_FIGURE_LIST.TabIndex = 13;
            this.BTN_UPDATE_FIGURE_LIST.Text = "Update Figure List";
            this.BTN_UPDATE_FIGURE_LIST.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_FIGURE_LIST.Visible = false;
            this.BTN_UPDATE_FIGURE_LIST.Click += new System.EventHandler(this.BTN_UPDATE_FIGURE_LIST_Click);
            // 
            // LV_RECP
            // 
            this.LV_RECP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LV_RECP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDX,
            this.FIELS});
            this.LV_RECP.ForeColor = System.Drawing.Color.Black;
            this.LV_RECP.FullRowSelect = true;
            this.LV_RECP.GridLines = true;
            this.LV_RECP.Location = new System.Drawing.Point(208, 157);
            this.LV_RECP.Name = "LV_RECP";
            this.LV_RECP.Size = new System.Drawing.Size(380, 170);
            this.LV_RECP.TabIndex = 27;
            this.LV_RECP.UseCompatibleStateImageBehavior = false;
            this.LV_RECP.View = System.Windows.Forms.View.Details;
            this.LV_RECP.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_RECP_ColumnClick);
            this.LV_RECP.SelectedIndexChanged += new System.EventHandler(this.LV_RECP_SelectedIndexChanged);
            this.LV_RECP.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LV_RECP_MouseDoubleClick);
            // 
            // IDX
            // 
            this.IDX.Text = "IDX";
            this.IDX.Width = 40;
            // 
            // FIELS
            // 
            this.FIELS.Text = "FILES";
            this.FIELS.Width = 325;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label52.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label52.Location = new System.Drawing.Point(401, 92);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(21, 13);
            this.label52.TabIndex = 0;
            this.label52.Text = "%";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label22.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label22.Location = new System.Drawing.Point(463, 69);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 12);
            this.label22.TabIndex = 1;
            this.label22.Text = "Overwrite";
            // 
            // TXT_PTRN_ACC_RATIO
            // 
            this.TXT_PTRN_ACC_RATIO.BackColor = System.Drawing.Color.White;
            this.TXT_PTRN_ACC_RATIO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_ACC_RATIO.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_ACC_RATIO.ForeColor = System.Drawing.Color.Black;
            this.TXT_PTRN_ACC_RATIO.Location = new System.Drawing.Point(308, 88);
            this.TXT_PTRN_ACC_RATIO.Name = "TXT_PTRN_ACC_RATIO";
            this.TXT_PTRN_ACC_RATIO.Size = new System.Drawing.Size(83, 20);
            this.TXT_PTRN_ACC_RATIO.TabIndex = 1;
            // 
            // TXT_PATH_PTRN_FILE
            // 
            this.TXT_PATH_PTRN_FILE.BackColor = System.Drawing.Color.White;
            this.TXT_PATH_PTRN_FILE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PATH_PTRN_FILE.ForeColor = System.Drawing.Color.Black;
            this.TXT_PATH_PTRN_FILE.Location = new System.Drawing.Point(114, 62);
            this.TXT_PATH_PTRN_FILE.Name = "TXT_PATH_PTRN_FILE";
            this.TXT_PATH_PTRN_FILE.Size = new System.Drawing.Size(277, 22);
            this.TXT_PATH_PTRN_FILE.TabIndex = 1;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label51.Location = new System.Drawing.Point(266, 91);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(36, 14);
            this.label51.TabIndex = 0;
            this.label51.Text = "MR :";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label38.Location = new System.Drawing.Point(14, 64);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(94, 14);
            this.label38.TabIndex = 0;
            this.label38.Text = "NAME PTRN :";
            // 
            // TXT_PATH_RECP_FILE
            // 
            this.TXT_PATH_RECP_FILE.BackColor = System.Drawing.Color.White;
            this.TXT_PATH_RECP_FILE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PATH_RECP_FILE.ForeColor = System.Drawing.Color.Black;
            this.TXT_PATH_RECP_FILE.Location = new System.Drawing.Point(114, 34);
            this.TXT_PATH_RECP_FILE.Name = "TXT_PATH_RECP_FILE";
            this.TXT_PATH_RECP_FILE.Size = new System.Drawing.Size(277, 22);
            this.TXT_PATH_RECP_FILE.TabIndex = 1;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label33.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label33.Location = new System.Drawing.Point(541, 70);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(51, 13);
            this.label33.TabIndex = 0;
            this.label33.Text = "MATCH";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label20.Location = new System.Drawing.Point(14, 36);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 14);
            this.label20.TabIndex = 0;
            this.label20.Text = "NAME_RECP :";
            // 
            // LV_PARAMETER
            // 
            this.LV_PARAMETER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LV_PARAMETER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LV_PARAMETER.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LV_PARAMETER.ForeColor = System.Drawing.Color.Black;
            this.LV_PARAMETER.FullRowSelect = true;
            this.LV_PARAMETER.GridLines = true;
            this.LV_PARAMETER.Location = new System.Drawing.Point(3, 312);
            this.LV_PARAMETER.Name = "LV_PARAMETER";
            this.LV_PARAMETER.Scrollable = false;
            this.LV_PARAMETER.Size = new System.Drawing.Size(227, 263);
            this.LV_PARAMETER.TabIndex = 11;
            this.LV_PARAMETER.UseCompatibleStateImageBehavior = false;
            this.LV_PARAMETER.View = System.Windows.Forms.View.Details;
            this.LV_PARAMETER.SelectedIndexChanged += new System.EventHandler(this.LV_PARAMETER_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IDX";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "FIGURES";
            this.columnHeader2.Width = 177;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label31.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label31.Location = new System.Drawing.Point(534, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(67, 13);
            this.label31.TabIndex = 41;
            this.label31.Text = "MEASURE";
            // 
            // msg
            // 
            this.msg.BackColor = System.Drawing.Color.White;
            this.msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msg.ForeColor = System.Drawing.Color.Black;
            this.msg.Location = new System.Drawing.Point(4, 7);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(425, 155);
            this.msg.TabIndex = 39;
            this.msg.Text = "";
            // 
            // panel27
            // 
            this.panel27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel27.Controls.Add(this.BTN_FIGURE_MODIF_SCALE_PLUS);
            this.panel27.Controls.Add(this.BTN_FIGURE_MODIF_SCALE_MINUS);
            this.panel27.Controls.Add(this.RDO_ROI_ASYM);
            this.panel27.Controls.Add(this.RDO_ROI_SIZE);
            this.panel27.Controls.Add(this.TXT_FIGURE_CONTROL_SCALE);
            this.panel27.Controls.Add(this.TB_FIGURE_CTRL_SCALE);
            this.panel27.Controls.Add(this.RDO_ROI_POSITION);
            this.panel27.Controls.Add(this.RDO_ROI_GAP);
            this.panel27.Controls.Add(this.label47);
            this.panel27.Controls.Add(this.label87);
            this.panel27.Location = new System.Drawing.Point(3, 660);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(602, 89);
            this.panel27.TabIndex = 42;
            // 
            // BTN_FIGURE_MODIF_SCALE_PLUS
            // 
            this.BTN_FIGURE_MODIF_SCALE_PLUS.Location = new System.Drawing.Point(574, 56);
            this.BTN_FIGURE_MODIF_SCALE_PLUS.Name = "BTN_FIGURE_MODIF_SCALE_PLUS";
            this.BTN_FIGURE_MODIF_SCALE_PLUS.Size = new System.Drawing.Size(23, 23);
            this.BTN_FIGURE_MODIF_SCALE_PLUS.TabIndex = 8;
            this.BTN_FIGURE_MODIF_SCALE_PLUS.Text = "▶";
            this.BTN_FIGURE_MODIF_SCALE_PLUS.UseVisualStyleBackColor = true;
            this.BTN_FIGURE_MODIF_SCALE_PLUS.Click += new System.EventHandler(this.BTN_FIGURE_MODIFY_SCALE_Click);
            // 
            // BTN_FIGURE_MODIF_SCALE_MINUS
            // 
            this.BTN_FIGURE_MODIF_SCALE_MINUS.Location = new System.Drawing.Point(545, 56);
            this.BTN_FIGURE_MODIF_SCALE_MINUS.Name = "BTN_FIGURE_MODIF_SCALE_MINUS";
            this.BTN_FIGURE_MODIF_SCALE_MINUS.Size = new System.Drawing.Size(23, 23);
            this.BTN_FIGURE_MODIF_SCALE_MINUS.TabIndex = 8;
            this.BTN_FIGURE_MODIF_SCALE_MINUS.Text = "◀";
            this.BTN_FIGURE_MODIF_SCALE_MINUS.UseVisualStyleBackColor = true;
            this.BTN_FIGURE_MODIF_SCALE_MINUS.Click += new System.EventHandler(this.BTN_FIGURE_MODIFY_SCALE_Click);
            // 
            // RDO_ROI_ASYM
            // 
            this.RDO_ROI_ASYM.AutoSize = true;
            this.RDO_ROI_ASYM.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_ASYM.Location = new System.Drawing.Point(232, 66);
            this.RDO_ROI_ASYM.Name = "RDO_ROI_ASYM";
            this.RDO_ROI_ASYM.Size = new System.Drawing.Size(69, 18);
            this.RDO_ROI_ASYM.TabIndex = 2;
            this.RDO_ROI_ASYM.Text = "ZigZag";
            this.RDO_ROI_ASYM.UseVisualStyleBackColor = true;
            this.RDO_ROI_ASYM.Visible = false;
            this.RDO_ROI_ASYM.CheckedChanged += new System.EventHandler(this.RDO_ROI_ASYM_CheckedChanged);
            // 
            // RDO_ROI_SIZE
            // 
            this.RDO_ROI_SIZE.AutoSize = true;
            this.RDO_ROI_SIZE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_SIZE.Location = new System.Drawing.Point(170, 66);
            this.RDO_ROI_SIZE.Name = "RDO_ROI_SIZE";
            this.RDO_ROI_SIZE.Size = new System.Drawing.Size(56, 18);
            this.RDO_ROI_SIZE.TabIndex = 2;
            this.RDO_ROI_SIZE.Text = "SIZE";
            this.RDO_ROI_SIZE.UseVisualStyleBackColor = true;
            this.RDO_ROI_SIZE.CheckedChanged += new System.EventHandler(this.RDO_ROI_FIGURE_CheckedChanged);
            // 
            // TXT_FIGURE_CONTROL_SCALE
            // 
            this.TXT_FIGURE_CONTROL_SCALE.BackColor = System.Drawing.Color.White;
            this.TXT_FIGURE_CONTROL_SCALE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_FIGURE_CONTROL_SCALE.ForeColor = System.Drawing.Color.Black;
            this.TXT_FIGURE_CONTROL_SCALE.Location = new System.Drawing.Point(557, 28);
            this.TXT_FIGURE_CONTROL_SCALE.Name = "TXT_FIGURE_CONTROL_SCALE";
            this.TXT_FIGURE_CONTROL_SCALE.Size = new System.Drawing.Size(29, 22);
            this.TXT_FIGURE_CONTROL_SCALE.TabIndex = 7;
            this.TXT_FIGURE_CONTROL_SCALE.Text = "10";
            this.TXT_FIGURE_CONTROL_SCALE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TB_FIGURE_CTRL_SCALE
            // 
            this.TB_FIGURE_CTRL_SCALE.Location = new System.Drawing.Point(8, 6);
            this.TB_FIGURE_CTRL_SCALE.Maximum = 50;
            this.TB_FIGURE_CTRL_SCALE.Minimum = 1;
            this.TB_FIGURE_CTRL_SCALE.Name = "TB_FIGURE_CTRL_SCALE";
            this.TB_FIGURE_CTRL_SCALE.Size = new System.Drawing.Size(536, 45);
            this.TB_FIGURE_CTRL_SCALE.SmallChange = 5;
            this.TB_FIGURE_CTRL_SCALE.TabIndex = 3;
            this.TB_FIGURE_CTRL_SCALE.TickFrequency = 5;
            this.TB_FIGURE_CTRL_SCALE.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TB_FIGURE_CTRL_SCALE.Value = 5;
            this.TB_FIGURE_CTRL_SCALE.Scroll += new System.EventHandler(this.TB_FIGURE_CTRL_SCALE_Scroll);
            // 
            // RDO_ROI_POSITION
            // 
            this.RDO_ROI_POSITION.AutoSize = true;
            this.RDO_ROI_POSITION.Checked = true;
            this.RDO_ROI_POSITION.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_POSITION.Location = new System.Drawing.Point(57, 65);
            this.RDO_ROI_POSITION.Name = "RDO_ROI_POSITION";
            this.RDO_ROI_POSITION.Size = new System.Drawing.Size(54, 18);
            this.RDO_ROI_POSITION.TabIndex = 2;
            this.RDO_ROI_POSITION.TabStop = true;
            this.RDO_ROI_POSITION.Text = "POS";
            this.RDO_ROI_POSITION.UseVisualStyleBackColor = true;
            this.RDO_ROI_POSITION.CheckedChanged += new System.EventHandler(this.RDO_ROI_FIGURE_CheckedChanged);
            // 
            // RDO_ROI_GAP
            // 
            this.RDO_ROI_GAP.AutoSize = true;
            this.RDO_ROI_GAP.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RDO_ROI_GAP.Location = new System.Drawing.Point(116, 66);
            this.RDO_ROI_GAP.Name = "RDO_ROI_GAP";
            this.RDO_ROI_GAP.Size = new System.Drawing.Size(53, 18);
            this.RDO_ROI_GAP.TabIndex = 2;
            this.RDO_ROI_GAP.Text = "GAP";
            this.RDO_ROI_GAP.UseVisualStyleBackColor = true;
            this.RDO_ROI_GAP.CheckedChanged += new System.EventHandler(this.RDO_ROI_FIGURE_CheckedChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label47.Location = new System.Drawing.Point(10, 66);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(42, 14);
            this.label47.TabIndex = 0;
            this.label47.Text = "ACT :";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label87.Location = new System.Drawing.Point(550, 10);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(41, 14);
            this.label87.TabIndex = 0;
            this.label87.Text = "UNIT";
            // 
            // CHK_USE_HISTORY
            // 
            this.CHK_USE_HISTORY.AutoSize = true;
            this.CHK_USE_HISTORY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CHK_USE_HISTORY.Location = new System.Drawing.Point(131, 126);
            this.CHK_USE_HISTORY.Name = "CHK_USE_HISTORY";
            this.CHK_USE_HISTORY.Size = new System.Drawing.Size(97, 18);
            this.CHK_USE_HISTORY.TabIndex = 12;
            this.CHK_USE_HISTORY.Text = "MEAS RES.";
            this.CHK_USE_HISTORY.UseVisualStyleBackColor = true;
            // 
            // TAB_VIEW
            // 
            this.TAB_VIEW.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TAB_VIEW.Controls.Add(this.tabpage33);
            this.TAB_VIEW.Controls.Add(this.tabPage2);
            this.TAB_VIEW.HotTrack = true;
            this.TAB_VIEW.ItemSize = new System.Drawing.Size(90, 35);
            this.TAB_VIEW.Location = new System.Drawing.Point(5, 5);
            this.TAB_VIEW.Multiline = true;
            this.TAB_VIEW.Name = "TAB_VIEW";
            this.TAB_VIEW.Padding = new System.Drawing.Point(0, 0);
            this.TAB_VIEW.SelectedIndex = 0;
            this.TAB_VIEW.Size = new System.Drawing.Size(655, 760);
            this.TAB_VIEW.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TAB_VIEW.TabIndex = 52;
            // 
            // tabpage33
            // 
            this.tabpage33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabpage33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabpage33.Controls.Add(this.imageView1);
            this.tabpage33.Controls.Add(this.CHK_BLEND);
            this.tabpage33.Controls.Add(this.TB_BLENDING_RATIO);
            this.tabpage33.Controls.Add(this.LB_BLEND_VALUE);
            this.tabpage33.Controls.Add(this.label30);
            this.tabpage33.Location = new System.Drawing.Point(39, 4);
            this.tabpage33.Name = "tabpage33";
            this.tabpage33.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage33.Size = new System.Drawing.Size(612, 752);
            this.tabpage33.TabIndex = 0;
            this.tabpage33.Text = "VIEW1";
            // 
            // imageView1
            // 
            this.imageView1.AllowDrop = true;
            this.imageView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.imageView1.BOOL_DRAW_FOCUS_ROI = true;
            this.imageView1.BOOL_DRAW_PTRN_ROI = true;
            this.imageView1.BOOL_TEACHING_ACTIVATION = false;
            this.imageView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageView1.ForeColor = System.Drawing.Color.Lime;
            this.imageView1.Location = new System.Drawing.Point(0, 0);
            this.imageView1.Name = "imageView1";
            this.imageView1.PT_FIGURE_TO_DRAW = ((System.Drawing.PointF)(resources.GetObject("imageView1.PT_FIGURE_TO_DRAW")));
            this.imageView1.ROI_INDEX = -1;
            this.imageView1.Size = new System.Drawing.Size(600, 678);
            this.imageView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage2.Controls.Add(this.imageView2);
            this.tabPage2.Location = new System.Drawing.Point(39, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 752);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VIEW2";
            // 
            // imageView2
            // 
            this.imageView2.AllowDrop = true;
            this.imageView2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.imageView2.BOOL_DRAW_FOCUS_ROI = true;
            this.imageView2.BOOL_DRAW_PTRN_ROI = true;
            this.imageView2.BOOL_TEACHING_ACTIVATION = false;
            this.imageView2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageView2.ForeColor = System.Drawing.Color.Lime;
            this.imageView2.Location = new System.Drawing.Point(0, 0);
            this.imageView2.Name = "imageView2";
            this.imageView2.PT_FIGURE_TO_DRAW = ((System.Drawing.PointF)(resources.GetObject("imageView2.PT_FIGURE_TO_DRAW")));
            this.imageView2.ROI_INDEX = -1;
            this.imageView2.Size = new System.Drawing.Size(600, 678);
            this.imageView2.TabIndex = 0;
            // 
            // UC_LOG_VIEWER
            // 
            this.UC_LOG_VIEWER.AllowDrop = true;
            this.UC_LOG_VIEWER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UC_LOG_VIEWER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UC_LOG_VIEWER.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UC_LOG_VIEWER.ForeColor = System.Drawing.Color.LimeGreen;
            this.UC_LOG_VIEWER.Location = new System.Drawing.Point(12, 799);
            this.UC_LOG_VIEWER.m_workIndexer = 330;
            this.UC_LOG_VIEWER.Name = "UC_LOG_VIEWER";
            this.UC_LOG_VIEWER.Size = new System.Drawing.Size(651, 237);
            this.UC_LOG_VIEWER.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label14.Location = new System.Drawing.Point(16, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "BASE_RECP :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label11.Location = new System.Drawing.Point(399, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Save As";
            // 
            // TXT_BASE_RECP
            // 
            this.TXT_BASE_RECP.BackColor = System.Drawing.Color.White;
            this.TXT_BASE_RECP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_BASE_RECP.ForeColor = System.Drawing.Color.Black;
            this.TXT_BASE_RECP.Location = new System.Drawing.Point(114, 6);
            this.TXT_BASE_RECP.Name = "TXT_BASE_RECP";
            this.TXT_BASE_RECP.Size = new System.Drawing.Size(277, 22);
            this.TXT_BASE_RECP.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label15.Location = new System.Drawing.Point(340, 338);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 14);
            this.label15.TabIndex = 0;
            this.label15.Text = "POS FOCUS :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label10.Location = new System.Drawing.Point(15, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 14);
            this.label10.TabIndex = 0;
            this.label10.Text = "POS ORG :";
            // 
            // TXT_FOCUS_POS_Y
            // 
            this.TXT_FOCUS_POS_Y.BackColor = System.Drawing.Color.White;
            this.TXT_FOCUS_POS_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_FOCUS_POS_Y.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_FOCUS_POS_Y.ForeColor = System.Drawing.Color.Black;
            this.TXT_FOCUS_POS_Y.Location = new System.Drawing.Point(516, 334);
            this.TXT_FOCUS_POS_Y.Name = "TXT_FOCUS_POS_Y";
            this.TXT_FOCUS_POS_Y.Size = new System.Drawing.Size(70, 20);
            this.TXT_FOCUS_POS_Y.TabIndex = 1;
            // 
            // TXT_PTRN_POS_ORG_Y
            // 
            this.TXT_PTRN_POS_ORG_Y.BackColor = System.Drawing.Color.White;
            this.TXT_PTRN_POS_ORG_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_POS_ORG_Y.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_POS_ORG_Y.ForeColor = System.Drawing.Color.Black;
            this.TXT_PTRN_POS_ORG_Y.Location = new System.Drawing.Point(190, 89);
            this.TXT_PTRN_POS_ORG_Y.Name = "TXT_PTRN_POS_ORG_Y";
            this.TXT_PTRN_POS_ORG_Y.Size = new System.Drawing.Size(70, 20);
            this.TXT_PTRN_POS_ORG_Y.TabIndex = 1;
            // 
            // TXT_FOCUS_POS_X
            // 
            this.TXT_FOCUS_POS_X.BackColor = System.Drawing.Color.White;
            this.TXT_FOCUS_POS_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_FOCUS_POS_X.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_FOCUS_POS_X.ForeColor = System.Drawing.Color.Black;
            this.TXT_FOCUS_POS_X.Location = new System.Drawing.Point(440, 334);
            this.TXT_FOCUS_POS_X.Name = "TXT_FOCUS_POS_X";
            this.TXT_FOCUS_POS_X.Size = new System.Drawing.Size(70, 20);
            this.TXT_FOCUS_POS_X.TabIndex = 1;
            // 
            // TXT_PTRN_POS_ORG_X
            // 
            this.TXT_PTRN_POS_ORG_X.BackColor = System.Drawing.Color.White;
            this.TXT_PTRN_POS_ORG_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_POS_ORG_X.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_POS_ORG_X.ForeColor = System.Drawing.Color.Black;
            this.TXT_PTRN_POS_ORG_X.Location = new System.Drawing.Point(114, 89);
            this.TXT_PTRN_POS_ORG_X.Name = "TXT_PTRN_POS_ORG_X";
            this.TXT_PTRN_POS_ORG_X.Size = new System.Drawing.Size(70, 20);
            this.TXT_PTRN_POS_ORG_X.TabIndex = 1;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "CD_MEASURE";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Measure";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.BTN_MENU_STATISTICS);
            this.panel3.Controls.Add(this.BTN_MAIN_LOGO);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.LB_HACKER);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.BTN_MENU_CREATE_RECP);
            this.panel3.Controls.Add(this.BTN_MENU_IMAGE_PROCESSING);
            this.panel3.Controls.Add(this.BTN_MENU_TUNNING);
            this.panel3.Controls.Add(this.BTN_HACKER);
            this.panel3.Controls.Add(this.BTN_MENU_RECP);
            this.panel3.Controls.Add(this.BTN_MENU_HISTORY_MATCHING);
            this.panel3.Controls.Add(this.BTN_MENU_CONFIG);
            this.panel3.Controls.Add(this.BTN_MENU_HISTORY_MEASURE);
            this.panel3.Controls.Add(this.BTN_MENU_PTRN);
            this.panel3.Location = new System.Drawing.Point(673, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1225, 86);
            this.panel3.TabIndex = 57;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label9.Location = new System.Drawing.Point(1092, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "MEAS.";
            // 
            // LB_HACKER
            // 
            this.LB_HACKER.AutoSize = true;
            this.LB_HACKER.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.LB_HACKER.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LB_HACKER.Location = new System.Drawing.Point(873, 67);
            this.LB_HACKER.Name = "LB_HACKER";
            this.LB_HACKER.Size = new System.Drawing.Size(91, 13);
            this.LB_HACKER.TabIndex = 42;
            this.LB_HACKER.Text = "Hacker Mode";
            this.LB_HACKER.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label23.Location = new System.Drawing.Point(502, 67);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(56, 13);
            this.label23.TabIndex = 42;
            this.label23.Text = "Params";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label24.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label24.Location = new System.Drawing.Point(434, 69);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 13);
            this.label24.TabIndex = 42;
            this.label24.Text = "Tunning";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label21.Location = new System.Drawing.Point(363, 69);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "ImgProc";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label19.Location = new System.Drawing.Point(307, 69);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 42;
            this.label19.Text = "PTRN";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label18.Location = new System.Drawing.Point(217, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 42;
            this.label18.Text = "NEW RECP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label8.Location = new System.Drawing.Point(1020, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "PTRN.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(1156, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "CONFIG";
            // 
            // PNL_DRAW_FIGURE
            // 
            this.PNL_DRAW_FIGURE.BackColor = System.Drawing.SystemColors.Control;
            this.PNL_DRAW_FIGURE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNL_DRAW_FIGURE.Controls.Add(this.label25);
            this.PNL_DRAW_FIGURE.Controls.Add(this.BTN_UPDATE_FIGURE_LIST);
            this.PNL_DRAW_FIGURE.Controls.Add(this.label4);
            this.PNL_DRAW_FIGURE.Controls.Add(this.BTN_UPDATE_PARAMETER);
            this.PNL_DRAW_FIGURE.Controls.Add(this.BTN_RELOAD_PARAM_FIGURES);
            this.PNL_DRAW_FIGURE.Controls.Add(this.LV_PARAMETER);
            this.PNL_DRAW_FIGURE.Controls.Add(this.BTN_FIGURE_REMOVE_ALL);
            this.PNL_DRAW_FIGURE.Controls.Add(this.TAB_FIGURE);
            this.PNL_DRAW_FIGURE.Controls.Add(this.label5);
            this.PNL_DRAW_FIGURE.Controls.Add(this.label6);
            this.PNL_DRAW_FIGURE.Controls.Add(this.UC_Parameter);
            this.PNL_DRAW_FIGURE.Controls.Add(this.panel27);
            this.PNL_DRAW_FIGURE.Controls.Add(this.label13);
            this.PNL_DRAW_FIGURE.Controls.Add(this.label12);
            this.PNL_DRAW_FIGURE.Location = new System.Drawing.Point(1289, 97);
            this.PNL_DRAW_FIGURE.Name = "PNL_DRAW_FIGURE";
            this.PNL_DRAW_FIGURE.Size = new System.Drawing.Size(610, 760);
            this.PNL_DRAW_FIGURE.TabIndex = 58;
            this.PNL_DRAW_FIGURE.Paint += new System.Windows.Forms.PaintEventHandler(this.PNL_DRAW_FIGURE_Paint);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label25.Location = new System.Drawing.Point(450, 569);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(151, 14);
            this.label25.TabIndex = 0;
            this.label25.Text = "235, 310 / 370 , 340";
            this.label25.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(12, 645);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "REFRESH";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label5.Location = new System.Drawing.Point(108, 644);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "RESET";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label6.Location = new System.Drawing.Point(171, 644);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "MODIFY";
            // 
            // UC_Parameter
            // 
            this.UC_Parameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UC_Parameter.Location = new System.Drawing.Point(269, 362);
            this.UC_Parameter.Name = "UC_Parameter";
            this.UC_Parameter.Size = new System.Drawing.Size(423, 396);
            this.UC_Parameter.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label13.Location = new System.Drawing.Point(233, 286);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 14);
            this.label13.TabIndex = 0;
            this.label13.Text = ":- PARAMETERS ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label12.Location = new System.Drawing.Point(12, 286);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = ":- FIGURES";
            // 
            // PNL_MAIN
            // 
            this.PNL_MAIN.BackColor = System.Drawing.SystemColors.Control;
            this.PNL_MAIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNL_MAIN.Controls.Add(this.CHK_USE_SAVE_PTRN_ERR);
            this.PNL_MAIN.Controls.Add(this.CHK_USE_SAVE_INPUT);
            this.PNL_MAIN.Controls.Add(this.LV_RECP);
            this.PNL_MAIN.Controls.Add(this.CHK_USE_HISTORY);
            this.PNL_MAIN.Controls.Add(this.label14);
            this.PNL_MAIN.Controls.Add(this.PIC_FOCUS);
            this.PNL_MAIN.Controls.Add(this.PIC_PTRN);
            this.PNL_MAIN.Controls.Add(this.TXT_PATH_PTRN_FILE);
            this.PNL_MAIN.Controls.Add(this.BTN_RECIPE_SAVE);
            this.PNL_MAIN.Controls.Add(this.label20);
            this.PNL_MAIN.Controls.Add(this.BTN_RECP_SAVE_AS);
            this.PNL_MAIN.Controls.Add(this.label52);
            this.PNL_MAIN.Controls.Add(this.TXT_PTRN_ACC_RATIO);
            this.PNL_MAIN.Controls.Add(this.label33);
            this.PNL_MAIN.Controls.Add(this.TXT_PTRN_POS_ORG_X);
            this.PNL_MAIN.Controls.Add(this.label11);
            this.PNL_MAIN.Controls.Add(this.TXT_FOCUS_POS_X);
            this.PNL_MAIN.Controls.Add(this.label22);
            this.PNL_MAIN.Controls.Add(this.TXT_PTRN_POS_ORG_Y);
            this.PNL_MAIN.Controls.Add(this.TXT_BASE_RECP);
            this.PNL_MAIN.Controls.Add(this.TXT_FOCUS_POS_Y);
            this.PNL_MAIN.Controls.Add(this.TXT_PATH_RECP_FILE);
            this.PNL_MAIN.Controls.Add(this.label51);
            this.PNL_MAIN.Controls.Add(this.label3);
            this.PNL_MAIN.Controls.Add(this.label10);
            this.PNL_MAIN.Controls.Add(this.label2);
            this.PNL_MAIN.Controls.Add(this.label15);
            this.PNL_MAIN.Controls.Add(this.label38);
            this.PNL_MAIN.Controls.Add(this.BTN_PTRN_MATCH);
            this.PNL_MAIN.Controls.Add(this.BTN_PTRN_RESULT);
            this.PNL_MAIN.Location = new System.Drawing.Point(673, 97);
            this.PNL_MAIN.Name = "PNL_MAIN";
            this.PNL_MAIN.Size = new System.Drawing.Size(604, 671);
            this.PNL_MAIN.TabIndex = 59;
            // 
            // CHK_USE_SAVE_PTRN_ERR
            // 
            this.CHK_USE_SAVE_PTRN_ERR.AutoSize = true;
            this.CHK_USE_SAVE_PTRN_ERR.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CHK_USE_SAVE_PTRN_ERR.Location = new System.Drawing.Point(357, 126);
            this.CHK_USE_SAVE_PTRN_ERR.Name = "CHK_USE_SAVE_PTRN_ERR";
            this.CHK_USE_SAVE_PTRN_ERR.Size = new System.Drawing.Size(134, 18);
            this.CHK_USE_SAVE_PTRN_ERR.TabIndex = 12;
            this.CHK_USE_SAVE_PTRN_ERR.Text = "MATCHING_ERR";
            this.CHK_USE_SAVE_PTRN_ERR.UseVisualStyleBackColor = true;
            // 
            // CHK_USE_SAVE_INPUT
            // 
            this.CHK_USE_SAVE_INPUT.AutoSize = true;
            this.CHK_USE_SAVE_INPUT.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CHK_USE_SAVE_INPUT.Location = new System.Drawing.Point(234, 126);
            this.CHK_USE_SAVE_INPUT.Name = "CHK_USE_SAVE_INPUT";
            this.CHK_USE_SAVE_INPUT.Size = new System.Drawing.Size(117, 18);
            this.CHK_USE_SAVE_INPUT.TabIndex = 12;
            this.CHK_USE_SAVE_INPUT.Text = "INPUT IMAGE";
            this.CHK_USE_SAVE_INPUT.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label3.Location = new System.Drawing.Point(15, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "SAVE OPTION :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(13, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = ":- FOCUS STATUS";
            // 
            // BTN_PTRN_RESULT
            // 
            this.BTN_PTRN_RESULT.BackColor = System.Drawing.Color.DimGray;
            this.BTN_PTRN_RESULT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_PTRN_RESULT.Location = new System.Drawing.Point(8, 152);
            this.BTN_PTRN_RESULT.Name = "BTN_PTRN_RESULT";
            this.BTN_PTRN_RESULT.Size = new System.Drawing.Size(191, 175);
            this.BTN_PTRN_RESULT.TabIndex = 29;
            this.BTN_PTRN_RESULT.UseVisualStyleBackColor = false;
            // 
            // PNL_MEASURE_RESULT
            // 
            this.PNL_MEASURE_RESULT.BackColor = System.Drawing.SystemColors.Control;
            this.PNL_MEASURE_RESULT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNL_MEASURE_RESULT.Controls.Add(this.BTN_PLAY);
            this.PNL_MEASURE_RESULT.Controls.Add(this.BTN_GET_IMAGE_FROM_SEQUENCE);
            this.PNL_MEASURE_RESULT.Controls.Add(this.BTN_MEASURE);
            this.PNL_MEASURE_RESULT.Controls.Add(this.msg);
            this.PNL_MEASURE_RESULT.Controls.Add(this.CHK_MEASURE_DUMP);
            this.PNL_MEASURE_RESULT.Controls.Add(this.CHK_MEASURE_VIEW_ONLY);
            this.PNL_MEASURE_RESULT.Controls.Add(this.label1);
            this.PNL_MEASURE_RESULT.Controls.Add(this.label31);
            this.PNL_MEASURE_RESULT.Location = new System.Drawing.Point(1288, 863);
            this.PNL_MEASURE_RESULT.Name = "PNL_MEASURE_RESULT";
            this.PNL_MEASURE_RESULT.Size = new System.Drawing.Size(610, 173);
            this.PNL_MEASURE_RESULT.TabIndex = 60;
            // 
            // BTN_PLAY
            // 
            this.BTN_PLAY.Location = new System.Drawing.Point(435, 86);
            this.BTN_PLAY.Name = "BTN_PLAY";
            this.BTN_PLAY.Size = new System.Drawing.Size(75, 23);
            this.BTN_PLAY.TabIndex = 42;
            this.BTN_PLAY.Text = "Play";
            this.BTN_PLAY.UseVisualStyleBackColor = true;
            this.BTN_PLAY.Click += new System.EventHandler(this.BTN_PLAY_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(481, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "GRAB";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label16.Location = new System.Drawing.Point(673, 770);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 14);
            this.label16.TabIndex = 0;
            this.label16.Text = ":- OFFLINE-FILE LIST";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label17.Location = new System.Drawing.Point(6, 770);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(153, 14);
            this.label17.TabIndex = 0;
            this.label17.Text = ":- OPERATIONAL LOG";
            // 
            // BTN_GET_IMAGE_FROM_SEQUENCE
            // 
            this.BTN_GET_IMAGE_FROM_SEQUENCE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.camera_info;
            this.BTN_GET_IMAGE_FROM_SEQUENCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_GET_IMAGE_FROM_SEQUENCE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BTN_GET_IMAGE_FROM_SEQUENCE.Location = new System.Drawing.Point(470, 7);
            this.BTN_GET_IMAGE_FROM_SEQUENCE.Name = "BTN_GET_IMAGE_FROM_SEQUENCE";
            this.BTN_GET_IMAGE_FROM_SEQUENCE.Size = new System.Drawing.Size(60, 60);
            this.BTN_GET_IMAGE_FROM_SEQUENCE.TabIndex = 39;
            this.BTN_GET_IMAGE_FROM_SEQUENCE.UseVisualStyleBackColor = true;
            this.BTN_GET_IMAGE_FROM_SEQUENCE.Click += new System.EventHandler(this.COMMON_BTN_GET_OPTICS_INFO_Click);
            // 
            // BTN_MEASURE
            // 
            this.BTN_MEASURE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_MEASURE.BackgroundImage")));
            this.BTN_MEASURE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MEASURE.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_MEASURE.Font = new System.Drawing.Font("Verdana", 9F);
            this.BTN_MEASURE.ForeColor = System.Drawing.Color.Cornsilk;
            this.BTN_MEASURE.Location = new System.Drawing.Point(536, 7);
            this.BTN_MEASURE.Name = "BTN_MEASURE";
            this.BTN_MEASURE.Size = new System.Drawing.Size(60, 60);
            this.BTN_MEASURE.TabIndex = 40;
            this.BTN_MEASURE.UseVisualStyleBackColor = true;
            this.BTN_MEASURE.Click += new System.EventHandler(this.BTN_MEASURE_Click);
            // 
            // PIC_FOCUS
            // 
            this.PIC_FOCUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.PIC_FOCUS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PIC_FOCUS.Location = new System.Drawing.Point(8, 360);
            this.PIC_FOCUS.Name = "PIC_FOCUS";
            this.PIC_FOCUS.Size = new System.Drawing.Size(580, 300);
            this.PIC_FOCUS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PIC_FOCUS.TabIndex = 28;
            this.PIC_FOCUS.TabStop = false;
            // 
            // PIC_PTRN
            // 
            this.PIC_PTRN.BackColor = System.Drawing.Color.White;
            this.PIC_PTRN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PIC_PTRN.Location = new System.Drawing.Point(21, 165);
            this.PIC_PTRN.Name = "PIC_PTRN";
            this.PIC_PTRN.Size = new System.Drawing.Size(165, 150);
            this.PIC_PTRN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PIC_PTRN.TabIndex = 28;
            this.PIC_PTRN.TabStop = false;
            // 
            // BTN_RECIPE_SAVE
            // 
            this.BTN_RECIPE_SAVE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_RECIPE_SAVE.BackgroundImage")));
            this.BTN_RECIPE_SAVE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECIPE_SAVE.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_RECIPE_SAVE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_RECIPE_SAVE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_RECIPE_SAVE.Location = new System.Drawing.Point(465, 6);
            this.BTN_RECIPE_SAVE.Name = "BTN_RECIPE_SAVE";
            this.BTN_RECIPE_SAVE.Size = new System.Drawing.Size(60, 60);
            this.BTN_RECIPE_SAVE.TabIndex = 12;
            this.BTN_RECIPE_SAVE.UseVisualStyleBackColor = true;
            this.BTN_RECIPE_SAVE.Click += new System.EventHandler(this.BTN_RECIPE_SAVE_Click);
            // 
            // BTN_RECP_SAVE_AS
            // 
            this.BTN_RECP_SAVE_AS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_RECP_SAVE_AS.BackgroundImage")));
            this.BTN_RECP_SAVE_AS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RECP_SAVE_AS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_RECP_SAVE_AS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_RECP_SAVE_AS.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_RECP_SAVE_AS.Location = new System.Drawing.Point(397, 6);
            this.BTN_RECP_SAVE_AS.Name = "BTN_RECP_SAVE_AS";
            this.BTN_RECP_SAVE_AS.Size = new System.Drawing.Size(60, 60);
            this.BTN_RECP_SAVE_AS.TabIndex = 12;
            this.BTN_RECP_SAVE_AS.UseVisualStyleBackColor = true;
            this.BTN_RECP_SAVE_AS.Click += new System.EventHandler(this.BTN_RECP_SAVE_AS_Click);
            // 
            // BTN_PTRN_MATCH
            // 
            this.BTN_PTRN_MATCH.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_PTRN_MATCH.BackgroundImage")));
            this.BTN_PTRN_MATCH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_MATCH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_PTRN_MATCH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_PTRN_MATCH.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_PTRN_MATCH.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BTN_PTRN_MATCH.Location = new System.Drawing.Point(534, 7);
            this.BTN_PTRN_MATCH.Name = "BTN_PTRN_MATCH";
            this.BTN_PTRN_MATCH.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_MATCH.TabIndex = 12;
            this.BTN_PTRN_MATCH.UseVisualStyleBackColor = true;
            this.BTN_PTRN_MATCH.Click += new System.EventHandler(this.BTN_PTRN_MATCH_Click);
            // 
            // BTN_UPDATE_PARAMETER
            // 
            this.BTN_UPDATE_PARAMETER.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.view_refresh;
            this.BTN_UPDATE_PARAMETER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_UPDATE_PARAMETER.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_UPDATE_PARAMETER.FlatAppearance.BorderSize = 3;
            this.BTN_UPDATE_PARAMETER.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BTN_UPDATE_PARAMETER.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_UPDATE_PARAMETER.Location = new System.Drawing.Point(169, 581);
            this.BTN_UPDATE_PARAMETER.Name = "BTN_UPDATE_PARAMETER";
            this.BTN_UPDATE_PARAMETER.Size = new System.Drawing.Size(60, 60);
            this.BTN_UPDATE_PARAMETER.TabIndex = 0;
            this.BTN_UPDATE_PARAMETER.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_PARAMETER.Click += new System.EventHandler(this.BTN_UPDATE_PARAMETER_Click);
            // 
            // BTN_RELOAD_PARAM_FIGURES
            // 
            this.BTN_RELOAD_PARAM_FIGURES.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_RELOAD_PARAM_FIGURES.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_RELOAD_PARAM_FIGURES.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_RELOAD_PARAM_FIGURES.FlatAppearance.BorderSize = 3;
            this.BTN_RELOAD_PARAM_FIGURES.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_RELOAD_PARAM_FIGURES.Location = new System.Drawing.Point(12, 581);
            this.BTN_RELOAD_PARAM_FIGURES.Name = "BTN_RELOAD_PARAM_FIGURES";
            this.BTN_RELOAD_PARAM_FIGURES.Size = new System.Drawing.Size(60, 60);
            this.BTN_RELOAD_PARAM_FIGURES.TabIndex = 12;
            this.BTN_RELOAD_PARAM_FIGURES.UseVisualStyleBackColor = true;
            this.BTN_RELOAD_PARAM_FIGURES.Click += new System.EventHandler(this.BTN_RELOAD_PARAM_FIGURES_Click);
            // 
            // BTN_FIGURE_REMOVE_ALL
            // 
            this.BTN_FIGURE_REMOVE_ALL.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.Trash_empty;
            this.BTN_FIGURE_REMOVE_ALL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_FIGURE_REMOVE_ALL.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_FIGURE_REMOVE_ALL.FlatAppearance.BorderSize = 3;
            this.BTN_FIGURE_REMOVE_ALL.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_FIGURE_REMOVE_ALL.Location = new System.Drawing.Point(104, 581);
            this.BTN_FIGURE_REMOVE_ALL.Name = "BTN_FIGURE_REMOVE_ALL";
            this.BTN_FIGURE_REMOVE_ALL.Size = new System.Drawing.Size(60, 60);
            this.BTN_FIGURE_REMOVE_ALL.TabIndex = 12;
            this.BTN_FIGURE_REMOVE_ALL.UseVisualStyleBackColor = true;
            this.BTN_FIGURE_REMOVE_ALL.Click += new System.EventHandler(this.BTN_FIGURE_REMOVE_ALL_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button5.Location = new System.Drawing.Point(5, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 100);
            this.button5.TabIndex = 8;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button9.Location = new System.Drawing.Point(5, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(100, 100);
            this.button9.TabIndex = 8;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button3.Location = new System.Drawing.Point(5, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 100);
            this.button3.TabIndex = 8;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button2.Location = new System.Drawing.Point(5, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 100);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // BTN_MENU_STATISTICS
            // 
            this.BTN_MENU_STATISTICS.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.trends;
            this.BTN_MENU_STATISTICS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_STATISTICS.Location = new System.Drawing.Point(564, 7);
            this.BTN_MENU_STATISTICS.Name = "BTN_MENU_STATISTICS";
            this.BTN_MENU_STATISTICS.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_STATISTICS.TabIndex = 44;
            this.BTN_MENU_STATISTICS.UseVisualStyleBackColor = true;
            this.BTN_MENU_STATISTICS.Click += new System.EventHandler(this.BTN_MENU_STATISTICS_Click);
            // 
            // BTN_MAIN_LOGO
            // 
            this.BTN_MAIN_LOGO.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.logo;
            this.BTN_MAIN_LOGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MAIN_LOGO.FlatAppearance.BorderSize = 0;
            this.BTN_MAIN_LOGO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MAIN_LOGO.Location = new System.Drawing.Point(8, 17);
            this.BTN_MAIN_LOGO.Name = "BTN_MAIN_LOGO";
            this.BTN_MAIN_LOGO.Size = new System.Drawing.Size(200, 50);
            this.BTN_MAIN_LOGO.TabIndex = 43;
            this.BTN_MAIN_LOGO.UseVisualStyleBackColor = true;
            this.BTN_MAIN_LOGO.Click += new System.EventHandler(this.BTN_MAIN_LOGO_Click);
            this.BTN_MAIN_LOGO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BTN_MAIN_LOGO_KeyDown);
            // 
            // BTN_MENU_CREATE_RECP
            // 
            this.BTN_MENU_CREATE_RECP.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.new_recp;
            this.BTN_MENU_CREATE_RECP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_CREATE_RECP.Location = new System.Drawing.Point(225, 5);
            this.BTN_MENU_CREATE_RECP.Name = "BTN_MENU_CREATE_RECP";
            this.BTN_MENU_CREATE_RECP.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_CREATE_RECP.TabIndex = 0;
            this.BTN_MENU_CREATE_RECP.UseVisualStyleBackColor = true;
            this.BTN_MENU_CREATE_RECP.Click += new System.EventHandler(this.BTN_MENU_CREATE_RECP_Click);
            // 
            // BTN_MENU_IMAGE_PROCESSING
            // 
            this.BTN_MENU_IMAGE_PROCESSING.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.image2;
            this.BTN_MENU_IMAGE_PROCESSING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_IMAGE_PROCESSING.Location = new System.Drawing.Point(365, 7);
            this.BTN_MENU_IMAGE_PROCESSING.Name = "BTN_MENU_IMAGE_PROCESSING";
            this.BTN_MENU_IMAGE_PROCESSING.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_IMAGE_PROCESSING.TabIndex = 0;
            this.BTN_MENU_IMAGE_PROCESSING.UseVisualStyleBackColor = true;
            this.BTN_MENU_IMAGE_PROCESSING.Click += new System.EventHandler(this.BTN_MENU_IMAGE_PROCESSING_Click);
            // 
            // BTN_MENU_TUNNING
            // 
            this.BTN_MENU_TUNNING.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.camera_info;
            this.BTN_MENU_TUNNING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_TUNNING.Location = new System.Drawing.Point(431, 7);
            this.BTN_MENU_TUNNING.Name = "BTN_MENU_TUNNING";
            this.BTN_MENU_TUNNING.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_TUNNING.TabIndex = 0;
            this.BTN_MENU_TUNNING.UseVisualStyleBackColor = true;
            this.BTN_MENU_TUNNING.Click += new System.EventHandler(this.BTN_MENU_TUNNING_Click);
            // 
            // BTN_HACKER
            // 
            this.BTN_HACKER.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.matrix;
            this.BTN_HACKER.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_HACKER.Location = new System.Drawing.Point(882, 6);
            this.BTN_HACKER.Name = "BTN_HACKER";
            this.BTN_HACKER.Size = new System.Drawing.Size(60, 60);
            this.BTN_HACKER.TabIndex = 0;
            this.BTN_HACKER.UseVisualStyleBackColor = true;
            this.BTN_HACKER.Visible = false;
            this.BTN_HACKER.Click += new System.EventHandler(this.BTN_HACKER_Click);
            // 
            // BTN_MENU_RECP
            // 
            this.BTN_MENU_RECP.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.setting;
            this.BTN_MENU_RECP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_RECP.Location = new System.Drawing.Point(498, 6);
            this.BTN_MENU_RECP.Name = "BTN_MENU_RECP";
            this.BTN_MENU_RECP.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_RECP.TabIndex = 0;
            this.BTN_MENU_RECP.UseVisualStyleBackColor = true;
            this.BTN_MENU_RECP.Click += new System.EventHandler(this.BTN_MENU_RECP_Click);
            // 
            // BTN_MENU_HISTORY_MATCHING
            // 
            this.BTN_MENU_HISTORY_MATCHING.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.history__2_;
            this.BTN_MENU_HISTORY_MATCHING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_HISTORY_MATCHING.Location = new System.Drawing.Point(1011, 6);
            this.BTN_MENU_HISTORY_MATCHING.Name = "BTN_MENU_HISTORY_MATCHING";
            this.BTN_MENU_HISTORY_MATCHING.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_HISTORY_MATCHING.TabIndex = 0;
            this.BTN_MENU_HISTORY_MATCHING.UseVisualStyleBackColor = true;
            this.BTN_MENU_HISTORY_MATCHING.Click += new System.EventHandler(this.BTN_MENU_HISTORY_MATCHING_Click);
            // 
            // BTN_MENU_CONFIG
            // 
            this.BTN_MENU_CONFIG.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.configs;
            this.BTN_MENU_CONFIG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_CONFIG.Location = new System.Drawing.Point(1154, 4);
            this.BTN_MENU_CONFIG.Name = "BTN_MENU_CONFIG";
            this.BTN_MENU_CONFIG.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_CONFIG.TabIndex = 0;
            this.BTN_MENU_CONFIG.UseVisualStyleBackColor = true;
            this.BTN_MENU_CONFIG.Click += new System.EventHandler(this.BTN_MENU_CONFIG_Click);
            // 
            // BTN_MENU_HISTORY_MEASURE
            // 
            this.BTN_MENU_HISTORY_MEASURE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.history__1_;
            this.BTN_MENU_HISTORY_MEASURE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_HISTORY_MEASURE.Location = new System.Drawing.Point(1084, 6);
            this.BTN_MENU_HISTORY_MEASURE.Name = "BTN_MENU_HISTORY_MEASURE";
            this.BTN_MENU_HISTORY_MEASURE.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_HISTORY_MEASURE.TabIndex = 0;
            this.BTN_MENU_HISTORY_MEASURE.UseVisualStyleBackColor = true;
            this.BTN_MENU_HISTORY_MEASURE.Click += new System.EventHandler(this.BTN_MENU_HISTORY_Click);
            // 
            // BTN_MENU_PTRN
            // 
            this.BTN_MENU_PTRN.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.patching;
            this.BTN_MENU_PTRN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MENU_PTRN.Location = new System.Drawing.Point(296, 7);
            this.BTN_MENU_PTRN.Name = "BTN_MENU_PTRN";
            this.BTN_MENU_PTRN.Size = new System.Drawing.Size(60, 60);
            this.BTN_MENU_PTRN.TabIndex = 0;
            this.BTN_MENU_PTRN.UseVisualStyleBackColor = true;
            this.BTN_MENU_PTRN.Click += new System.EventHandler(this.BTN_MENU_PTRN_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label26.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label26.Location = new System.Drawing.Point(582, 67);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(31, 13);
            this.label26.TabIndex = 42;
            this.label26.Text = "SPC";
            // 
            // CDMainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1904, 1042);
            this.Controls.Add(this.UC_LOG_VIEWER);
            this.Controls.Add(this.LV_FILE_LIST);
            this.Controls.Add(this.PNL_MEASURE_RESULT);
            this.Controls.Add(this.PNL_MAIN);
            this.Controls.Add(this.PNL_DRAW_FIGURE);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TAB_VIEW);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CDMainForm";
            this.Text = "AUROS - CRITICAL DEMENSION MEASURE SYSTEM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TB_BLENDING_RATIO)).EndInit();
            this.TAB_FIGURE.ResumeLayout(false);
            this.draw_Rect.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.draw_circle.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.draw_ovl.ResumeLayout(false);
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.draw_matching.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_FIGURE_CTRL_SCALE)).EndInit();
            this.TAB_VIEW.ResumeLayout(false);
            this.tabpage33.ResumeLayout(false);
            this.tabpage33.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.PNL_DRAW_FIGURE.ResumeLayout(false);
            this.PNL_DRAW_FIGURE.PerformLayout();
            this.PNL_MAIN.ResumeLayout(false);
            this.PNL_MAIN.PerformLayout();
            this.PNL_MEASURE_RESULT.ResumeLayout(false);
            this.PNL_MEASURE_RESULT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_FOCUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_PTRN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CD_View.UC_CD_VIEWER imageView2;
        private System.Windows.Forms.CheckBox CHK_MEASURE_DUMP;
        private System.Windows.Forms.CheckBox CHK_MEASURE_VIEW_ONLY;
        private System.Windows.Forms.ListView LV_FILE_LIST;
        private System.Windows.Forms.ColumnHeader INDEX;
        private System.Windows.Forms.ColumnHeader FILES;
        private System.Windows.Forms.CheckBox CHK_BLEND;
        private System.Windows.Forms.TrackBar TB_BLENDING_RATIO;
        private System.Windows.Forms.Label LB_BLEND_VALUE;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TabControl TAB_FIGURE;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button BTN_PTRN_MATCH;
        private System.Windows.Forms.TextBox TXT_PTRN_ACC_RATIO;
        private System.Windows.Forms.TextBox TXT_PATH_PTRN_FILE;
        private System.Windows.Forms.Button BTN_RECIPE_SAVE;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox TXT_PATH_RECP_FILE;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage draw_Rect;
        private System.Windows.Forms.ListView LV_PAIR_DIG;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.RadioButton RDO_TYPE_DIA;
        private System.Windows.Forms.RadioButton RDO_TYPE_VER;
        private System.Windows.Forms.RadioButton RDO_TYPE_HOR;
        private System.Windows.Forms.Button BTN_DIG_COPY;
        private System.Windows.Forms.Button BTN_DIG_ADD;
        private System.Windows.Forms.Button BTN_DIG_MODIFY;
        private System.Windows.Forms.Button BTN_DIG_REMOVE;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.TextBox TXT_PARAM_DIA_ANGLE;
        private System.Windows.Forms.Label LB_PARAM_DIA_ANGLE;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox TXT_RCD_H;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Button BTN_DIA_ANGLE_DW;
        private System.Windows.Forms.Button BTN_DIA_ANGLE_RV;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox TXT_RCD_W;
        private System.Windows.Forms.Button BTN_DIA_ANGLE_UP;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox TXT_PARAM_DIG_NICK;
        private System.Windows.Forms.TabPage draw_circle;
        private System.Windows.Forms.ListView LV_PAIR_CIR;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Button BTN_CIR_ADD;
        private System.Windows.Forms.Button BTN_CIR_COPY;
        private System.Windows.Forms.Button BTN_CIR_MODIFY;
        private System.Windows.Forms.Button BTN_CIR_REMOVE;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox TXT_CIRCLE_H;
        private System.Windows.Forms.TextBox TXT_CIRCLE_W;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox TXT_PARAM_CIR_NICK;
        private System.Windows.Forms.TabPage draw_ovl;
        private System.Windows.Forms.ListView LV_PAIR_OVL;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox TXT_OVL_EX_H;
        private System.Windows.Forms.TextBox TXT_OVL_IN_H;
        private System.Windows.Forms.TextBox TXT_OVL_EX_W;
        private System.Windows.Forms.TextBox TXT_OVL_IN_W;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox TXT_PARAM_OVL_NICK;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.RadioButton RDO_ROI_OVL_EX;
        private System.Windows.Forms.RadioButton RDO_ROI_OVL_ENTIRE;
        private System.Windows.Forms.RadioButton RDO_ROI_OVL_IN;
        private System.Windows.Forms.Button BTN_OL_ADD;
        private System.Windows.Forms.Button BTN_OL_COPY;
        private System.Windows.Forms.Button BTN_OL_REMOVE;
        private System.Windows.Forms.TabPage draw_matching;
        private System.Windows.Forms.PictureBox PIC_PTRN;
        private System.Windows.Forms.ListView LV_RECP;
        private System.Windows.Forms.ColumnHeader IDX;
        private System.Windows.Forms.ColumnHeader FIELS;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Button BTN_REMOVE_FOCUS_ROI;
        private System.Windows.Forms.Button BTN_DRAW_FOCUS_ROI;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BTN_UPDATE_FIGURE_LIST;
        private System.Windows.Forms.Button BTN_RELOAD_PARAM_FIGURES;
        private System.Windows.Forms.ListView LV_PARAMETER;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button BTN_UPDATE_PARAMETER;
        private UC_LogView.UC_LOG_VIEWER UC_LOG_VIEWER;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button BTN_MEASURE;
        private System.Windows.Forms.RichTextBox msg;
        private CD_Paramter.pnl_parameter UC_Parameter;
        private CD_View.UC_CD_VIEWER imageView1;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.RadioButton RDO_ROI_ASYM;
        private System.Windows.Forms.RadioButton RDO_ROI_SIZE;
        private System.Windows.Forms.TextBox TXT_FIGURE_CONTROL_SCALE;
        private System.Windows.Forms.TrackBar TB_FIGURE_CTRL_SCALE;
        private System.Windows.Forms.RadioButton RDO_ROI_POSITION;
        private System.Windows.Forms.RadioButton RDO_ROI_GAP;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.CheckBox CHK_USE_HISTORY;
        private System.Windows.Forms.TabControl TAB_VIEW;
        private System.Windows.Forms.TabPage tabpage33;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button BTN_GET_IMAGE_FROM_SEQUENCE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TXT_PTRN_POS_ORG_Y;
        private System.Windows.Forms.TextBox TXT_PTRN_POS_ORG_X;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BTN_RECP_SAVE_AS;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TXT_BASE_RECP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TXT_FOCUS_POS_Y;
        private System.Windows.Forms.TextBox TXT_FOCUS_POS_X;
        private System.Windows.Forms.Button BTN_SET_FOCUS_ROI;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BTN_MENU_PTRN;
        private System.Windows.Forms.Button BTN_MENU_RECP;
        private System.Windows.Forms.Button BTN_FIGURE_REMOVE_ALL;
        private System.Windows.Forms.Button BTN_MENU_CREATE_RECP;
        private System.Windows.Forms.Panel PNL_DRAW_FIGURE;
        private System.Windows.Forms.Button BTN_MENU_HISTORY_MEASURE;
        private System.Windows.Forms.Panel PNL_MAIN;
        private System.Windows.Forms.Panel PNL_MEASURE_RESULT;
        private System.Windows.Forms.Button BTN_FIGURE_MODIF_SCALE_PLUS;
        private System.Windows.Forms.Button BTN_FIGURE_MODIF_SCALE_MINUS;
        private System.Windows.Forms.CheckBox CHK_USE_SAVE_INPUT;
        private System.Windows.Forms.CheckBox CHK_USE_SAVE_PTRN_ERR;
        private System.Windows.Forms.Button BTN_MENU_HISTORY_MATCHING;
        private System.Windows.Forms.Button BTN_PTRN_RESULT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_MENU_IMAGE_PROCESSING;
        private System.Windows.Forms.Button BTN_MENU_CONFIG;
        private System.Windows.Forms.PictureBox PIC_FOCUS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button BTN_MAIN_LOGO;
        private System.Windows.Forms.Button BTN_MENU_TUNNING;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label LB_HACKER;
        private System.Windows.Forms.Button BTN_HACKER;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button BTN_PLAY;
        private System.Windows.Forms.Button BTN_MENU_STATISTICS;
        private System.Windows.Forms.Label label26;



    }
}


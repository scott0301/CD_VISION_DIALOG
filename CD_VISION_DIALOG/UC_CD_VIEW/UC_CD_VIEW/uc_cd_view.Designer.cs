namespace CD_View
{
    partial class UC_CD_VIEWER
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_CD_VIEWER));
            this.LB_VIEW_MOUSE_POS_X = new System.Windows.Forms.Label();
            this.LB_VIEW_MOUSE_POS_Y = new System.Windows.Forms.Label();
            this.LB_VIEW_PIXEL_VALUE = new System.Windows.Forms.Label();
            this.PIC_VIEW = new System.Windows.Forms.PictureBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LB_VIEW_ZOOM_ORIGIN = new System.Windows.Forms.Label();
            this.BTN_VIEW_OVERLAY_CLEAR = new System.Windows.Forms.Button();
            this.PNL_TOOL_COMMON = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_VIEW_PANNING = new System.Windows.Forms.Button();
            this.BTN_VIEW_SET_OVERLAY = new System.Windows.Forms.Button();
            this.BTN_VIEW_DRAW_FIGURE = new System.Windows.Forms.Button();
            this.BTN_VIEW_OVERLAY_MAG_ORG = new System.Windows.Forms.Button();
            this.BTN_VIEW_MAG_PLUS = new System.Windows.Forms.Button();
            this.BTN_VIEW_MAG_MINUS = new System.Windows.Forms.Button();
            this.BTN_VIEW_IMAGE_LOAD = new System.Windows.Forms.Button();
            this.BTN_VIEW_SAVE_OVERLAY = new System.Windows.Forms.Button();
            this.BTN_VIEW_IMAGE_SAVE = new System.Windows.Forms.Button();
            this.PNL_TOOL_BASIC = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.PNL_VIEW = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_VIEW)).BeginInit();
            this.PNL_TOOL_COMMON.SuspendLayout();
            this.PNL_TOOL_BASIC.SuspendLayout();
            this.PNL_VIEW.SuspendLayout();
            this.SuspendLayout();
            // 
            // LB_VIEW_MOUSE_POS_X
            // 
            this.LB_VIEW_MOUSE_POS_X.AutoSize = true;
            this.LB_VIEW_MOUSE_POS_X.BackColor = System.Drawing.Color.Transparent;
            this.LB_VIEW_MOUSE_POS_X.ForeColor = System.Drawing.Color.LimeGreen;
            this.LB_VIEW_MOUSE_POS_X.Location = new System.Drawing.Point(10, 573);
            this.LB_VIEW_MOUSE_POS_X.Name = "LB_VIEW_MOUSE_POS_X";
            this.LB_VIEW_MOUSE_POS_X.Size = new System.Drawing.Size(25, 14);
            this.LB_VIEW_MOUSE_POS_X.TabIndex = 152;
            this.LB_VIEW_MOUSE_POS_X.Text = "X :";
            // 
            // LB_VIEW_MOUSE_POS_Y
            // 
            this.LB_VIEW_MOUSE_POS_Y.AutoSize = true;
            this.LB_VIEW_MOUSE_POS_Y.BackColor = System.Drawing.Color.Transparent;
            this.LB_VIEW_MOUSE_POS_Y.ForeColor = System.Drawing.Color.LimeGreen;
            this.LB_VIEW_MOUSE_POS_Y.Location = new System.Drawing.Point(100, 573);
            this.LB_VIEW_MOUSE_POS_Y.Name = "LB_VIEW_MOUSE_POS_Y";
            this.LB_VIEW_MOUSE_POS_Y.Size = new System.Drawing.Size(30, 14);
            this.LB_VIEW_MOUSE_POS_Y.TabIndex = 152;
            this.LB_VIEW_MOUSE_POS_Y.Text = "Y : ";
            // 
            // LB_VIEW_PIXEL_VALUE
            // 
            this.LB_VIEW_PIXEL_VALUE.AutoSize = true;
            this.LB_VIEW_PIXEL_VALUE.BackColor = System.Drawing.Color.Transparent;
            this.LB_VIEW_PIXEL_VALUE.ForeColor = System.Drawing.Color.LimeGreen;
            this.LB_VIEW_PIXEL_VALUE.Location = new System.Drawing.Point(200, 573);
            this.LB_VIEW_PIXEL_VALUE.Name = "LB_VIEW_PIXEL_VALUE";
            this.LB_VIEW_PIXEL_VALUE.Size = new System.Drawing.Size(26, 14);
            this.LB_VIEW_PIXEL_VALUE.TabIndex = 152;
            this.LB_VIEW_PIXEL_VALUE.Text = "G :";
            // 
            // PIC_VIEW
            // 
            this.PIC_VIEW.BackColor = System.Drawing.Color.Black;
            this.PIC_VIEW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PIC_VIEW.Location = new System.Drawing.Point(0, 0);
            this.PIC_VIEW.Margin = new System.Windows.Forms.Padding(0);
            this.PIC_VIEW.Name = "PIC_VIEW";
            this.PIC_VIEW.Size = new System.Drawing.Size(600, 600);
            this.PIC_VIEW.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PIC_VIEW.TabIndex = 149;
            this.PIC_VIEW.TabStop = false;
            this.PIC_VIEW.Paint += new System.Windows.Forms.PaintEventHandler(this.PIC_VIEW_Paint);
            this.PIC_VIEW.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PIC_VIEW_MouseDoubleClick);
            this.PIC_VIEW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PIC_VIEW_MouseDown);
            this.PIC_VIEW.MouseEnter += new System.EventHandler(this.PIC_VIEW_MouseEnter);
            this.PIC_VIEW.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PIC_VIEW_MouseMove);
            this.PIC_VIEW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PIC_VIEW_MouseUp);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label53.Location = new System.Drawing.Point(532, 61);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(60, 12);
            this.label53.TabIndex = 1;
            this.label53.Text = "SAVE_OVR";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(469, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "SAVE_IMG";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label3.Location = new System.Drawing.Point(427, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "LOAD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(364, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "ZOOM -";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(192, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "CLEAR";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label32.Location = new System.Drawing.Point(62, 61);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(61, 12);
            this.label32.TabIndex = 1;
            this.label32.Text = "DRAW OFF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label6.Location = new System.Drawing.Point(129, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "OVR ON";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label5.Location = new System.Drawing.Point(4, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "PANNING";
            // 
            // LB_VIEW_ZOOM_ORIGIN
            // 
            this.LB_VIEW_ZOOM_ORIGIN.AutoSize = true;
            this.LB_VIEW_ZOOM_ORIGIN.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_VIEW_ZOOM_ORIGIN.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LB_VIEW_ZOOM_ORIGIN.Location = new System.Drawing.Point(248, 61);
            this.LB_VIEW_ZOOM_ORIGIN.Name = "LB_VIEW_ZOOM_ORIGIN";
            this.LB_VIEW_ZOOM_ORIGIN.Size = new System.Drawing.Size(44, 12);
            this.LB_VIEW_ZOOM_ORIGIN.TabIndex = 1;
            this.LB_VIEW_ZOOM_ORIGIN.Text = "ORIGIN";
            // 
            // BTN_VIEW_OVERLAY_CLEAR
            // 
            this.BTN_VIEW_OVERLAY_CLEAR.AutoEllipsis = true;
            this.BTN_VIEW_OVERLAY_CLEAR.BackColor = System.Drawing.Color.Transparent;
            this.BTN_VIEW_OVERLAY_CLEAR.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_OVERLAY_CLEAR.BackgroundImage")));
            this.BTN_VIEW_OVERLAY_CLEAR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_OVERLAY_CLEAR.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_OVERLAY_CLEAR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_OVERLAY_CLEAR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_OVERLAY_CLEAR.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_OVERLAY_CLEAR.Location = new System.Drawing.Point(184, 8);
            this.BTN_VIEW_OVERLAY_CLEAR.Name = "BTN_VIEW_OVERLAY_CLEAR";
            this.BTN_VIEW_OVERLAY_CLEAR.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_OVERLAY_CLEAR.TabIndex = 0;
            this.BTN_VIEW_OVERLAY_CLEAR.UseVisualStyleBackColor = false;
            this.BTN_VIEW_OVERLAY_CLEAR.Click += new System.EventHandler(this.BTN_VIEW_OVERLAY_CLEAR_Click);
            // 
            // PNL_TOOL_COMMON
            // 
            this.PNL_TOOL_COMMON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PNL_TOOL_COMMON.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNL_TOOL_COMMON.Controls.Add(this.label53);
            this.PNL_TOOL_COMMON.Controls.Add(this.label4);
            this.PNL_TOOL_COMMON.Controls.Add(this.label3);
            this.PNL_TOOL_COMMON.Controls.Add(this.label2);
            this.PNL_TOOL_COMMON.Controls.Add(this.label1);
            this.PNL_TOOL_COMMON.Controls.Add(this.label7);
            this.PNL_TOOL_COMMON.Controls.Add(this.label32);
            this.PNL_TOOL_COMMON.Controls.Add(this.label6);
            this.PNL_TOOL_COMMON.Controls.Add(this.label5);
            this.PNL_TOOL_COMMON.Controls.Add(this.LB_VIEW_ZOOM_ORIGIN);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_OVERLAY_CLEAR);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_PANNING);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_SET_OVERLAY);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_DRAW_FIGURE);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_OVERLAY_MAG_ORG);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_MAG_PLUS);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_MAG_MINUS);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_IMAGE_LOAD);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_SAVE_OVERLAY);
            this.PNL_TOOL_COMMON.Controls.Add(this.BTN_VIEW_IMAGE_SAVE);
            this.PNL_TOOL_COMMON.Location = new System.Drawing.Point(0, 401);
            this.PNL_TOOL_COMMON.Name = "PNL_TOOL_COMMON";
            this.PNL_TOOL_COMMON.Size = new System.Drawing.Size(600, 76);
            this.PNL_TOOL_COMMON.TabIndex = 153;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(304, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ZOOM +";
            // 
            // BTN_VIEW_PANNING
            // 
            this.BTN_VIEW_PANNING.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_PANNING.BackgroundImage")));
            this.BTN_VIEW_PANNING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_PANNING.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_PANNING.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_PANNING.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_PANNING.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_PANNING.Location = new System.Drawing.Point(7, 8);
            this.BTN_VIEW_PANNING.Name = "BTN_VIEW_PANNING";
            this.BTN_VIEW_PANNING.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_PANNING.TabIndex = 0;
            this.BTN_VIEW_PANNING.UseVisualStyleBackColor = true;
            this.BTN_VIEW_PANNING.Click += new System.EventHandler(this.BTN_VIEW_PANNING_Click);
            // 
            // BTN_VIEW_SET_OVERLAY
            // 
            this.BTN_VIEW_SET_OVERLAY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_SET_OVERLAY.BackgroundImage")));
            this.BTN_VIEW_SET_OVERLAY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_SET_OVERLAY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_SET_OVERLAY.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_SET_OVERLAY.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_SET_OVERLAY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_SET_OVERLAY.Location = new System.Drawing.Point(125, 8);
            this.BTN_VIEW_SET_OVERLAY.Name = "BTN_VIEW_SET_OVERLAY";
            this.BTN_VIEW_SET_OVERLAY.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_SET_OVERLAY.TabIndex = 0;
            this.BTN_VIEW_SET_OVERLAY.UseVisualStyleBackColor = true;
            this.BTN_VIEW_SET_OVERLAY.Click += new System.EventHandler(this.BTN_VIEW_SET_OVERLAY_Click);
            // 
            // BTN_VIEW_DRAW_FIGURE
            // 
            this.BTN_VIEW_DRAW_FIGURE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BTN_VIEW_DRAW_FIGURE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_DRAW_FIGURE.BackgroundImage")));
            this.BTN_VIEW_DRAW_FIGURE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_DRAW_FIGURE.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_DRAW_FIGURE.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_DRAW_FIGURE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_DRAW_FIGURE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_DRAW_FIGURE.Location = new System.Drawing.Point(66, 8);
            this.BTN_VIEW_DRAW_FIGURE.Name = "BTN_VIEW_DRAW_FIGURE";
            this.BTN_VIEW_DRAW_FIGURE.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_DRAW_FIGURE.TabIndex = 0;
            this.BTN_VIEW_DRAW_FIGURE.UseVisualStyleBackColor = false;
            this.BTN_VIEW_DRAW_FIGURE.Click += new System.EventHandler(this.BTN_VIEW_DRAW_FIGURE_Click);
            // 
            // BTN_VIEW_OVERLAY_MAG_ORG
            // 
            this.BTN_VIEW_OVERLAY_MAG_ORG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BTN_VIEW_OVERLAY_MAG_ORG.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_OVERLAY_MAG_ORG.BackgroundImage")));
            this.BTN_VIEW_OVERLAY_MAG_ORG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_OVERLAY_MAG_ORG.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_OVERLAY_MAG_ORG.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_OVERLAY_MAG_ORG.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_OVERLAY_MAG_ORG.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_OVERLAY_MAG_ORG.Location = new System.Drawing.Point(243, 8);
            this.BTN_VIEW_OVERLAY_MAG_ORG.Name = "BTN_VIEW_OVERLAY_MAG_ORG";
            this.BTN_VIEW_OVERLAY_MAG_ORG.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_OVERLAY_MAG_ORG.TabIndex = 0;
            this.BTN_VIEW_OVERLAY_MAG_ORG.UseVisualStyleBackColor = false;
            this.BTN_VIEW_OVERLAY_MAG_ORG.Click += new System.EventHandler(this.BTN_VIEW_OVERLAY_MAG_ORG_Click);
            // 
            // BTN_VIEW_MAG_PLUS
            // 
            this.BTN_VIEW_MAG_PLUS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_MAG_PLUS.BackgroundImage")));
            this.BTN_VIEW_MAG_PLUS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_MAG_PLUS.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_MAG_PLUS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_MAG_PLUS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_MAG_PLUS.Font = new System.Drawing.Font("Verdana", 9F);
            this.BTN_VIEW_MAG_PLUS.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_MAG_PLUS.Location = new System.Drawing.Point(302, 8);
            this.BTN_VIEW_MAG_PLUS.Name = "BTN_VIEW_MAG_PLUS";
            this.BTN_VIEW_MAG_PLUS.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_MAG_PLUS.TabIndex = 0;
            this.BTN_VIEW_MAG_PLUS.UseVisualStyleBackColor = true;
            this.BTN_VIEW_MAG_PLUS.Click += new System.EventHandler(this.BTN_VIEW_MAG_PLUS_Click);
            // 
            // BTN_VIEW_MAG_MINUS
            // 
            this.BTN_VIEW_MAG_MINUS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_MAG_MINUS.BackgroundImage")));
            this.BTN_VIEW_MAG_MINUS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_MAG_MINUS.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_MAG_MINUS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_MAG_MINUS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_MAG_MINUS.Font = new System.Drawing.Font("Verdana", 9F);
            this.BTN_VIEW_MAG_MINUS.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_MAG_MINUS.Location = new System.Drawing.Point(361, 8);
            this.BTN_VIEW_MAG_MINUS.Name = "BTN_VIEW_MAG_MINUS";
            this.BTN_VIEW_MAG_MINUS.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_MAG_MINUS.TabIndex = 0;
            this.BTN_VIEW_MAG_MINUS.UseVisualStyleBackColor = true;
            this.BTN_VIEW_MAG_MINUS.Click += new System.EventHandler(this.BTN_VIEW_MAG_MINUS_Click);
            // 
            // BTN_VIEW_IMAGE_LOAD
            // 
            this.BTN_VIEW_IMAGE_LOAD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_IMAGE_LOAD.BackgroundImage")));
            this.BTN_VIEW_IMAGE_LOAD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_IMAGE_LOAD.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_IMAGE_LOAD.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_IMAGE_LOAD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_IMAGE_LOAD.Font = new System.Drawing.Font("Verdana", 9F);
            this.BTN_VIEW_IMAGE_LOAD.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_IMAGE_LOAD.Location = new System.Drawing.Point(420, 8);
            this.BTN_VIEW_IMAGE_LOAD.Name = "BTN_VIEW_IMAGE_LOAD";
            this.BTN_VIEW_IMAGE_LOAD.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_IMAGE_LOAD.TabIndex = 0;
            this.BTN_VIEW_IMAGE_LOAD.UseVisualStyleBackColor = true;
            this.BTN_VIEW_IMAGE_LOAD.Click += new System.EventHandler(this.BTN_VIEW_IMAGE_LOAD_Click);
            // 
            // BTN_VIEW_SAVE_OVERLAY
            // 
            this.BTN_VIEW_SAVE_OVERLAY.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_SAVE_OVERLAY.BackgroundImage")));
            this.BTN_VIEW_SAVE_OVERLAY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_SAVE_OVERLAY.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_SAVE_OVERLAY.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_SAVE_OVERLAY.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_SAVE_OVERLAY.Font = new System.Drawing.Font("Verdana", 9F);
            this.BTN_VIEW_SAVE_OVERLAY.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_SAVE_OVERLAY.Location = new System.Drawing.Point(538, 8);
            this.BTN_VIEW_SAVE_OVERLAY.Name = "BTN_VIEW_SAVE_OVERLAY";
            this.BTN_VIEW_SAVE_OVERLAY.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_SAVE_OVERLAY.TabIndex = 0;
            this.BTN_VIEW_SAVE_OVERLAY.UseVisualStyleBackColor = true;
            this.BTN_VIEW_SAVE_OVERLAY.Click += new System.EventHandler(this.BTN_VIEW_SAVE_OVERLAY_Click);
            // 
            // BTN_VIEW_IMAGE_SAVE
            // 
            this.BTN_VIEW_IMAGE_SAVE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BTN_VIEW_IMAGE_SAVE.BackgroundImage")));
            this.BTN_VIEW_IMAGE_SAVE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_VIEW_IMAGE_SAVE.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_IMAGE_SAVE.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BTN_VIEW_IMAGE_SAVE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BTN_VIEW_IMAGE_SAVE.Font = new System.Drawing.Font("Verdana", 9F);
            this.BTN_VIEW_IMAGE_SAVE.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BTN_VIEW_IMAGE_SAVE.Location = new System.Drawing.Point(479, 8);
            this.BTN_VIEW_IMAGE_SAVE.Name = "BTN_VIEW_IMAGE_SAVE";
            this.BTN_VIEW_IMAGE_SAVE.Size = new System.Drawing.Size(50, 50);
            this.BTN_VIEW_IMAGE_SAVE.TabIndex = 0;
            this.BTN_VIEW_IMAGE_SAVE.UseVisualStyleBackColor = true;
            this.BTN_VIEW_IMAGE_SAVE.Click += new System.EventHandler(this.BTN_VIEW_IMAGE_SAVE_Click);
            // 
            // PNL_TOOL_BASIC
            // 
            this.PNL_TOOL_BASIC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PNL_TOOL_BASIC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNL_TOOL_BASIC.Controls.Add(this.label9);
            this.PNL_TOOL_BASIC.Controls.Add(this.label10);
            this.PNL_TOOL_BASIC.Controls.Add(this.label16);
            this.PNL_TOOL_BASIC.Controls.Add(this.label17);
            this.PNL_TOOL_BASIC.Controls.Add(this.button2);
            this.PNL_TOOL_BASIC.Controls.Add(this.button5);
            this.PNL_TOOL_BASIC.Controls.Add(this.button8);
            this.PNL_TOOL_BASIC.Controls.Add(this.button10);
            this.PNL_TOOL_BASIC.Location = new System.Drawing.Point(0, 483);
            this.PNL_TOOL_BASIC.Name = "PNL_TOOL_BASIC";
            this.PNL_TOOL_BASIC.Size = new System.Drawing.Size(245, 75);
            this.PNL_TOOL_BASIC.TabIndex = 153;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label9.Location = new System.Drawing.Point(170, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "SAVE_IMG";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label10.Location = new System.Drawing.Point(128, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "LOAD";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label16.Location = new System.Drawing.Point(4, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "PANNING";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label17.Location = new System.Drawing.Point(69, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "ORIGIN";
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button2.Location = new System.Drawing.Point(7, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 50);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.BTN_VIEW_PANNING_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button5.Location = new System.Drawing.Point(64, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 50);
            this.button5.TabIndex = 0;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.BTN_VIEW_OVERLAY_MAG_ORG_Click);
            // 
            // button8
            // 
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button8.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button8.Font = new System.Drawing.Font("Verdana", 9F);
            this.button8.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button8.Location = new System.Drawing.Point(121, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 50);
            this.button8.TabIndex = 0;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.BTN_VIEW_IMAGE_LOAD_Click);
            // 
            // button10
            // 
            this.button10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button10.BackgroundImage")));
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button10.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button10.Font = new System.Drawing.Font("Verdana", 9F);
            this.button10.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button10.Location = new System.Drawing.Point(180, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(50, 50);
            this.button10.TabIndex = 0;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.BTN_VIEW_IMAGE_SAVE_Click);
            // 
            // PNL_VIEW
            // 
            this.PNL_VIEW.Controls.Add(this.PIC_VIEW);
            this.PNL_VIEW.Location = new System.Drawing.Point(0, 0);
            this.PNL_VIEW.Name = "PNL_VIEW";
            this.PNL_VIEW.Size = new System.Drawing.Size(600, 600);
            this.PNL_VIEW.TabIndex = 154;
            // 
            // UC_CD_VIEWER
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.PNL_TOOL_BASIC);
            this.Controls.Add(this.PNL_TOOL_COMMON);
            this.Controls.Add(this.LB_VIEW_MOUSE_POS_Y);
            this.Controls.Add(this.LB_VIEW_PIXEL_VALUE);
            this.Controls.Add(this.LB_VIEW_MOUSE_POS_X);
            this.Controls.Add(this.PNL_VIEW);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Lime;
            this.Name = "UC_CD_VIEWER";
            this.Size = new System.Drawing.Size(600, 685);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Viewer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Viewer_DragEnter);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UC_CD_VIEWER_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.PIC_VIEW)).EndInit();
            this.PNL_TOOL_COMMON.ResumeLayout(false);
            this.PNL_TOOL_COMMON.PerformLayout();
            this.PNL_TOOL_BASIC.ResumeLayout(false);
            this.PNL_TOOL_BASIC.PerformLayout();
            this.PNL_VIEW.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PIC_VIEW;
        private System.Windows.Forms.Label LB_VIEW_MOUSE_POS_X;
        private System.Windows.Forms.Label LB_VIEW_MOUSE_POS_Y;
        private System.Windows.Forms.Label LB_VIEW_PIXEL_VALUE;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LB_VIEW_ZOOM_ORIGIN;
        private System.Windows.Forms.Button BTN_VIEW_OVERLAY_CLEAR;
        private System.Windows.Forms.Panel PNL_TOOL_COMMON;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_VIEW_PANNING;
        private System.Windows.Forms.Button BTN_VIEW_SET_OVERLAY;
        private System.Windows.Forms.Button BTN_VIEW_DRAW_FIGURE;
        private System.Windows.Forms.Button BTN_VIEW_OVERLAY_MAG_ORG;
        private System.Windows.Forms.Button BTN_VIEW_MAG_PLUS;
        private System.Windows.Forms.Button BTN_VIEW_MAG_MINUS;
        private System.Windows.Forms.Button BTN_VIEW_IMAGE_LOAD;
        private System.Windows.Forms.Button BTN_VIEW_SAVE_OVERLAY;
        private System.Windows.Forms.Button BTN_VIEW_IMAGE_SAVE;
        private System.Windows.Forms.Panel PNL_TOOL_BASIC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel PNL_VIEW;
    }
}

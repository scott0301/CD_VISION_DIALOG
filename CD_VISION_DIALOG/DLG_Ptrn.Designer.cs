﻿namespace CD_VISION_DIALOG
{
    partial class DLG_Ptrn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DLG_Ptrn));
            this.LV_PTRN = new System.Windows.Forms.ListView();
            this.IDX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FIELS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PIC_PTRN_NORMAL = new System.Windows.Forms.PictureBox();
            this.BTN_UPDATE_PTRN_LIST = new System.Windows.Forms.Button();
            this.BTN_MATCHING = new System.Windows.Forms.Button();
            this.TXT_PTRN_FILE_NAME = new System.Windows.Forms.TextBox();
            this.BTN_PTRN_CANCEL = new System.Windows.Forms.Button();
            this.BTN_PTRN_APPLY = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN_SYNC_WITH_RECP_NAME = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BTN_RGN_INFO_PTRN = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.CHK_PTRN_SEARCH_GLOBAL = new System.Windows.Forms.CheckBox();
            this.TXT_PTRN_TEACH_ROI_W = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.TXT_PTRN_TEACH_ROI_H = new System.Windows.Forms.TextBox();
            this.TXT_PTRN_ACC_RATIO = new System.Windows.Forms.TextBox();
            this.TXT_PTRN_TEACH_ROI_X = new System.Windows.Forms.TextBox();
            this.TXT_PTRN_TEACH_ROI_Y = new System.Windows.Forms.TextBox();
            this.GB_PTRN_SEARCH_RGN = new System.Windows.Forms.GroupBox();
            this.BTN_RGN_INFO_SEARCH = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN = new System.Windows.Forms.Button();
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN = new System.Windows.Forms.Button();
            this.TXT_LOCAL_SEARCH_RGN_X = new System.Windows.Forms.TextBox();
            this.TXT_LOCAL_SEARCH_RGN_H = new System.Windows.Forms.TextBox();
            this.TXT_LOCAL_SEARCH_RGN_Y = new System.Windows.Forms.TextBox();
            this.BTN_SET_EXTERNAL_IMAGE = new System.Windows.Forms.Button();
            this.TXT_LOCAL_SEARCH_RGN_W = new System.Windows.Forms.TextBox();
            this.CHK_PTRN_APPLY_EDGE_BASED = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PIC_PTRN_EDGE = new System.Windows.Forms.PictureBox();
            this.BTN_PTRN_TEACH_OVERWRITE = new System.Windows.Forms.Button();
            this.BTN_PTRN_TEACH_NEW = new System.Windows.Forms.Button();
            this.BTN_PTRN_DRAW = new System.Windows.Forms.Button();
            this.RICH_MESSAGE = new System.Windows.Forms.RichTextBox();
            this.BTN_SET_INTERNAL_IMAGE = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTN_SHOW_AND_HIDE = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.uc_view_ptrn = new CD_View.UC_CD_VIEWER();
            this.label13 = new System.Windows.Forms.Label();
            this.BTN_UPDATE_HISTORY = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.LV_HISTORY_PTRN_ERROR = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.PIC_PTRN_NORMAL)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GB_PTRN_SEARCH_RGN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_PTRN_EDGE)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LV_PTRN
            // 
            this.LV_PTRN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.LV_PTRN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDX,
            this.FIELS});
            this.LV_PTRN.ForeColor = System.Drawing.Color.White;
            this.LV_PTRN.FullRowSelect = true;
            this.LV_PTRN.GridLines = true;
            this.LV_PTRN.Location = new System.Drawing.Point(265, 24);
            this.LV_PTRN.Margin = new System.Windows.Forms.Padding(4);
            this.LV_PTRN.MultiSelect = false;
            this.LV_PTRN.Name = "LV_PTRN";
            this.LV_PTRN.Size = new System.Drawing.Size(400, 191);
            this.LV_PTRN.TabIndex = 29;
            this.LV_PTRN.UseCompatibleStateImageBehavior = false;
            this.LV_PTRN.View = System.Windows.Forms.View.Details;
            this.LV_PTRN.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_PTRN_ColumnClick);
            this.LV_PTRN.SelectedIndexChanged += new System.EventHandler(this.LV_PTRN_SelectedIndexChanged);
            this.LV_PTRN.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LV_PTRN_MouseDoubleClick);
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
            // PIC_PTRN_NORMAL
            // 
            this.PIC_PTRN_NORMAL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.PIC_PTRN_NORMAL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PIC_PTRN_NORMAL.Location = new System.Drawing.Point(7, 23);
            this.PIC_PTRN_NORMAL.Margin = new System.Windows.Forms.Padding(4);
            this.PIC_PTRN_NORMAL.Name = "PIC_PTRN_NORMAL";
            this.PIC_PTRN_NORMAL.Size = new System.Drawing.Size(250, 250);
            this.PIC_PTRN_NORMAL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PIC_PTRN_NORMAL.TabIndex = 30;
            this.PIC_PTRN_NORMAL.TabStop = false;
            // 
            // BTN_UPDATE_PTRN_LIST
            // 
            this.BTN_UPDATE_PTRN_LIST.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.view_refresh;
            this.BTN_UPDATE_PTRN_LIST.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BTN_UPDATE_PTRN_LIST.ForeColor = System.Drawing.Color.Black;
            this.BTN_UPDATE_PTRN_LIST.Location = new System.Drawing.Point(597, 252);
            this.BTN_UPDATE_PTRN_LIST.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_UPDATE_PTRN_LIST.Name = "BTN_UPDATE_PTRN_LIST";
            this.BTN_UPDATE_PTRN_LIST.Size = new System.Drawing.Size(60, 60);
            this.BTN_UPDATE_PTRN_LIST.TabIndex = 31;
            this.BTN_UPDATE_PTRN_LIST.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_PTRN_LIST.Click += new System.EventHandler(this.BTN_UPDATE_PTRN_LIST_Click);
            // 
            // BTN_MATCHING
            // 
            this.BTN_MATCHING.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.matchfind;
            this.BTN_MATCHING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_MATCHING.ForeColor = System.Drawing.Color.Black;
            this.BTN_MATCHING.Location = new System.Drawing.Point(533, 254);
            this.BTN_MATCHING.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_MATCHING.Name = "BTN_MATCHING";
            this.BTN_MATCHING.Size = new System.Drawing.Size(60, 60);
            this.BTN_MATCHING.TabIndex = 31;
            this.BTN_MATCHING.UseVisualStyleBackColor = true;
            this.BTN_MATCHING.Click += new System.EventHandler(this.BTN_MATCHING_Click);
            // 
            // TXT_PTRN_FILE_NAME
            // 
            this.TXT_PTRN_FILE_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PTRN_FILE_NAME.ForeColor = System.Drawing.Color.White;
            this.TXT_PTRN_FILE_NAME.Location = new System.Drawing.Point(357, 222);
            this.TXT_PTRN_FILE_NAME.Name = "TXT_PTRN_FILE_NAME";
            this.TXT_PTRN_FILE_NAME.Size = new System.Drawing.Size(234, 24);
            this.TXT_PTRN_FILE_NAME.TabIndex = 32;
            // 
            // BTN_PTRN_CANCEL
            // 
            this.BTN_PTRN_CANCEL.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_cancel;
            this.BTN_PTRN_CANCEL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_CANCEL.Location = new System.Drawing.Point(1084, 597);
            this.BTN_PTRN_CANCEL.Name = "BTN_PTRN_CANCEL";
            this.BTN_PTRN_CANCEL.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_CANCEL.TabIndex = 33;
            this.BTN_PTRN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_PTRN_CANCEL.Click += new System.EventHandler(this.BTN_PTRN_CANCEL_Click);
            // 
            // BTN_PTRN_APPLY
            // 
            this.BTN_PTRN_APPLY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.button_ok;
            this.BTN_PTRN_APPLY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_APPLY.Location = new System.Drawing.Point(1150, 597);
            this.BTN_PTRN_APPLY.Name = "BTN_PTRN_APPLY";
            this.BTN_PTRN_APPLY.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_APPLY.TabIndex = 33;
            this.BTN_PTRN_APPLY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTN_PTRN_APPLY.UseVisualStyleBackColor = true;
            this.BTN_PTRN_APPLY.Click += new System.EventHandler(this.BTN_PTRN_APPLY_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BTN_SYNC_WITH_RECP_NAME);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.GB_PTRN_SEARCH_RGN);
            this.groupBox1.Controls.Add(this.CHK_PTRN_APPLY_EDGE_BASED);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TXT_PTRN_FILE_NAME);
            this.groupBox1.Controls.Add(this.PIC_PTRN_EDGE);
            this.groupBox1.Controls.Add(this.PIC_PTRN_NORMAL);
            this.groupBox1.Controls.Add(this.BTN_PTRN_TEACH_OVERWRITE);
            this.groupBox1.Controls.Add(this.BTN_PTRN_TEACH_NEW);
            this.groupBox1.Controls.Add(this.BTN_PTRN_DRAW);
            this.groupBox1.Controls.Add(this.BTN_MATCHING);
            this.groupBox1.Controls.Add(this.BTN_UPDATE_PTRN_LIST);
            this.groupBox1.Controls.Add(this.LV_PTRN);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(614, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 594);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PARAMETERS";
            // 
            // BTN_SYNC_WITH_RECP_NAME
            // 
            this.BTN_SYNC_WITH_RECP_NAME.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTN_SYNC_WITH_RECP_NAME.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.BTN_SYNC_WITH_RECP_NAME.Location = new System.Drawing.Point(596, 222);
            this.BTN_SYNC_WITH_RECP_NAME.Name = "BTN_SYNC_WITH_RECP_NAME";
            this.BTN_SYNC_WITH_RECP_NAME.Size = new System.Drawing.Size(63, 23);
            this.BTN_SYNC_WITH_RECP_NAME.TabIndex = 78;
            this.BTN_SYNC_WITH_RECP_NAME.Text = "↔=↔";
            this.BTN_SYNC_WITH_RECP_NAME.UseVisualStyleBackColor = true;
            this.BTN_SYNC_WITH_RECP_NAME.Click += new System.EventHandler(this.BTN_SYNC_WITH_RECP_NAME_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(270, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 77;
            this.label7.Text = "DRAW";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(419, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 14);
            this.label12.TabIndex = 77;
            this.label12.Text = "OVERWRITE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(336, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 14);
            this.label6.TabIndex = 77;
            this.label6.Text = "SAVE NEW";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(539, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 77;
            this.label5.Text = "MATCH";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(600, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 14);
            this.label4.TabIndex = 77;
            this.label4.Text = "UPDATE";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BTN_RGN_INFO_PTRN);
            this.groupBox3.Controls.Add(this.label51);
            this.groupBox3.Controls.Add(this.CHK_PTRN_SEARCH_GLOBAL);
            this.groupBox3.Controls.Add(this.TXT_PTRN_TEACH_ROI_W);
            this.groupBox3.Controls.Add(this.label52);
            this.groupBox3.Controls.Add(this.TXT_PTRN_TEACH_ROI_H);
            this.groupBox3.Controls.Add(this.TXT_PTRN_ACC_RATIO);
            this.groupBox3.Controls.Add(this.TXT_PTRN_TEACH_ROI_X);
            this.groupBox3.Controls.Add(this.TXT_PTRN_TEACH_ROI_Y);
            this.groupBox3.Location = new System.Drawing.Point(262, 330);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 114);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            // 
            // BTN_RGN_INFO_PTRN
            // 
            this.BTN_RGN_INFO_PTRN.ForeColor = System.Drawing.Color.Black;
            this.BTN_RGN_INFO_PTRN.Location = new System.Drawing.Point(8, 23);
            this.BTN_RGN_INFO_PTRN.Name = "BTN_RGN_INFO_PTRN";
            this.BTN_RGN_INFO_PTRN.Size = new System.Drawing.Size(113, 23);
            this.BTN_RGN_INFO_PTRN.TabIndex = 39;
            this.BTN_RGN_INFO_PTRN.Text = "RGN INFO";
            this.BTN_RGN_INFO_PTRN.UseVisualStyleBackColor = true;
            this.BTN_RGN_INFO_PTRN.Click += new System.EventHandler(this.BTN_RGN_INFO_PTRN_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ForeColor = System.Drawing.Color.White;
            this.label51.Location = new System.Drawing.Point(250, 66);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(41, 17);
            this.label51.TabIndex = 36;
            this.label51.Text = "AR :";
            // 
            // CHK_PTRN_SEARCH_GLOBAL
            // 
            this.CHK_PTRN_SEARCH_GLOBAL.AutoSize = true;
            this.CHK_PTRN_SEARCH_GLOBAL.Checked = true;
            this.CHK_PTRN_SEARCH_GLOBAL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_PTRN_SEARCH_GLOBAL.ForeColor = System.Drawing.Color.White;
            this.CHK_PTRN_SEARCH_GLOBAL.Location = new System.Drawing.Point(15, 70);
            this.CHK_PTRN_SEARCH_GLOBAL.Name = "CHK_PTRN_SEARCH_GLOBAL";
            this.CHK_PTRN_SEARCH_GLOBAL.Size = new System.Drawing.Size(209, 21);
            this.CHK_PTRN_SEARCH_GLOBAL.TabIndex = 38;
            this.CHK_PTRN_SEARCH_GLOBAL.Text = "Apply Global Searching";
            this.CHK_PTRN_SEARCH_GLOBAL.UseVisualStyleBackColor = true;
            this.CHK_PTRN_SEARCH_GLOBAL.CheckedChanged += new System.EventHandler(this.CHK_PTRN_SEARCH_GLOBAL_CheckedChanged);
            // 
            // TXT_PTRN_TEACH_ROI_W
            // 
            this.TXT_PTRN_TEACH_ROI_W.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PTRN_TEACH_ROI_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_TEACH_ROI_W.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_TEACH_ROI_W.ForeColor = System.Drawing.Color.White;
            this.TXT_PTRN_TEACH_ROI_W.Location = new System.Drawing.Point(263, 23);
            this.TXT_PTRN_TEACH_ROI_W.Name = "TXT_PTRN_TEACH_ROI_W";
            this.TXT_PTRN_TEACH_ROI_W.Size = new System.Drawing.Size(57, 24);
            this.TXT_PTRN_TEACH_ROI_W.TabIndex = 37;
            this.TXT_PTRN_TEACH_ROI_W.Text = "0";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Verdana", 5F, System.Drawing.FontStyle.Bold);
            this.label52.ForeColor = System.Drawing.Color.White;
            this.label52.Location = new System.Drawing.Point(374, 70);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(16, 12);
            this.label52.TabIndex = 35;
            this.label52.Text = "%";
            // 
            // TXT_PTRN_TEACH_ROI_H
            // 
            this.TXT_PTRN_TEACH_ROI_H.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PTRN_TEACH_ROI_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_TEACH_ROI_H.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_TEACH_ROI_H.ForeColor = System.Drawing.Color.White;
            this.TXT_PTRN_TEACH_ROI_H.Location = new System.Drawing.Point(331, 23);
            this.TXT_PTRN_TEACH_ROI_H.Name = "TXT_PTRN_TEACH_ROI_H";
            this.TXT_PTRN_TEACH_ROI_H.Size = new System.Drawing.Size(57, 24);
            this.TXT_PTRN_TEACH_ROI_H.TabIndex = 37;
            this.TXT_PTRN_TEACH_ROI_H.Text = "0";
            // 
            // TXT_PTRN_ACC_RATIO
            // 
            this.TXT_PTRN_ACC_RATIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PTRN_ACC_RATIO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_ACC_RATIO.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_ACC_RATIO.ForeColor = System.Drawing.Color.White;
            this.TXT_PTRN_ACC_RATIO.Location = new System.Drawing.Point(294, 64);
            this.TXT_PTRN_ACC_RATIO.Name = "TXT_PTRN_ACC_RATIO";
            this.TXT_PTRN_ACC_RATIO.Size = new System.Drawing.Size(74, 24);
            this.TXT_PTRN_ACC_RATIO.TabIndex = 37;
            this.TXT_PTRN_ACC_RATIO.Text = "55";
            // 
            // TXT_PTRN_TEACH_ROI_X
            // 
            this.TXT_PTRN_TEACH_ROI_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PTRN_TEACH_ROI_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_TEACH_ROI_X.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_TEACH_ROI_X.ForeColor = System.Drawing.Color.White;
            this.TXT_PTRN_TEACH_ROI_X.Location = new System.Drawing.Point(127, 23);
            this.TXT_PTRN_TEACH_ROI_X.Name = "TXT_PTRN_TEACH_ROI_X";
            this.TXT_PTRN_TEACH_ROI_X.Size = new System.Drawing.Size(57, 24);
            this.TXT_PTRN_TEACH_ROI_X.TabIndex = 37;
            this.TXT_PTRN_TEACH_ROI_X.Text = "0";
            // 
            // TXT_PTRN_TEACH_ROI_Y
            // 
            this.TXT_PTRN_TEACH_ROI_Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_PTRN_TEACH_ROI_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_PTRN_TEACH_ROI_Y.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_PTRN_TEACH_ROI_Y.ForeColor = System.Drawing.Color.White;
            this.TXT_PTRN_TEACH_ROI_Y.Location = new System.Drawing.Point(195, 23);
            this.TXT_PTRN_TEACH_ROI_Y.Name = "TXT_PTRN_TEACH_ROI_Y";
            this.TXT_PTRN_TEACH_ROI_Y.Size = new System.Drawing.Size(57, 24);
            this.TXT_PTRN_TEACH_ROI_Y.TabIndex = 37;
            this.TXT_PTRN_TEACH_ROI_Y.Text = "0";
            // 
            // GB_PTRN_SEARCH_RGN
            // 
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.BTN_RGN_INFO_SEARCH);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.label9);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.label8);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.label11);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.TXT_LOCAL_SEARCH_RGN_X);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.TXT_LOCAL_SEARCH_RGN_H);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.TXT_LOCAL_SEARCH_RGN_Y);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.BTN_SET_EXTERNAL_IMAGE);
            this.GB_PTRN_SEARCH_RGN.Controls.Add(this.TXT_LOCAL_SEARCH_RGN_W);
            this.GB_PTRN_SEARCH_RGN.Location = new System.Drawing.Point(262, 450);
            this.GB_PTRN_SEARCH_RGN.Name = "GB_PTRN_SEARCH_RGN";
            this.GB_PTRN_SEARCH_RGN.Size = new System.Drawing.Size(398, 137);
            this.GB_PTRN_SEARCH_RGN.TabIndex = 40;
            this.GB_PTRN_SEARCH_RGN.TabStop = false;
            this.GB_PTRN_SEARCH_RGN.Visible = false;
            // 
            // BTN_RGN_INFO_SEARCH
            // 
            this.BTN_RGN_INFO_SEARCH.ForeColor = System.Drawing.Color.Black;
            this.BTN_RGN_INFO_SEARCH.Location = new System.Drawing.Point(8, 102);
            this.BTN_RGN_INFO_SEARCH.Name = "BTN_RGN_INFO_SEARCH";
            this.BTN_RGN_INFO_SEARCH.Size = new System.Drawing.Size(113, 23);
            this.BTN_RGN_INFO_SEARCH.TabIndex = 39;
            this.BTN_RGN_INFO_SEARCH.Text = "RGN INFO";
            this.BTN_RGN_INFO_SEARCH.UseVisualStyleBackColor = true;
            this.BTN_RGN_INFO_SEARCH.Click += new System.EventHandler(this.BTN_RGN_INFO_SEARCH_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(92, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 14);
            this.label9.TabIndex = 77;
            this.label9.Text = "SET";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(19, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 77;
            this.label8.Text = "DRAW";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(228, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 78;
            this.label11.Text = "SET IMG";
            this.label11.Visible = false;
            // 
            // BTN_PTRN_GET_LOCAL_SEARCHING_RGN
            // 
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.apiply2;
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.ForeColor = System.Drawing.Color.Black;
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.Location = new System.Drawing.Point(77, 17);
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.Name = "BTN_PTRN_GET_LOCAL_SEARCHING_RGN";
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.TabIndex = 39;
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.UseVisualStyleBackColor = true;
            this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN.Click += new System.EventHandler(this.BTN_PTRN_GET_LOCAL_SEARCHING_RGN_Click);
            // 
            // BTN_PTRN_SET_LOCAL_SEARCHING_RGN
            // 
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.measure;
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.ForeColor = System.Drawing.Color.Black;
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.Location = new System.Drawing.Point(10, 17);
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.Name = "BTN_PTRN_SET_LOCAL_SEARCHING_RGN";
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.TabIndex = 39;
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.UseVisualStyleBackColor = true;
            this.BTN_PTRN_SET_LOCAL_SEARCHING_RGN.Click += new System.EventHandler(this.BTN_SET_SEARCHING_RGN_Click);
            // 
            // TXT_LOCAL_SEARCH_RGN_X
            // 
            this.TXT_LOCAL_SEARCH_RGN_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_LOCAL_SEARCH_RGN_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_LOCAL_SEARCH_RGN_X.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_LOCAL_SEARCH_RGN_X.ForeColor = System.Drawing.Color.White;
            this.TXT_LOCAL_SEARCH_RGN_X.Location = new System.Drawing.Point(127, 102);
            this.TXT_LOCAL_SEARCH_RGN_X.Name = "TXT_LOCAL_SEARCH_RGN_X";
            this.TXT_LOCAL_SEARCH_RGN_X.Size = new System.Drawing.Size(57, 24);
            this.TXT_LOCAL_SEARCH_RGN_X.TabIndex = 37;
            this.TXT_LOCAL_SEARCH_RGN_X.Text = "0";
            // 
            // TXT_LOCAL_SEARCH_RGN_H
            // 
            this.TXT_LOCAL_SEARCH_RGN_H.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_LOCAL_SEARCH_RGN_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_LOCAL_SEARCH_RGN_H.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_LOCAL_SEARCH_RGN_H.ForeColor = System.Drawing.Color.White;
            this.TXT_LOCAL_SEARCH_RGN_H.Location = new System.Drawing.Point(331, 102);
            this.TXT_LOCAL_SEARCH_RGN_H.Name = "TXT_LOCAL_SEARCH_RGN_H";
            this.TXT_LOCAL_SEARCH_RGN_H.Size = new System.Drawing.Size(57, 24);
            this.TXT_LOCAL_SEARCH_RGN_H.TabIndex = 37;
            this.TXT_LOCAL_SEARCH_RGN_H.Text = "0";
            // 
            // TXT_LOCAL_SEARCH_RGN_Y
            // 
            this.TXT_LOCAL_SEARCH_RGN_Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_LOCAL_SEARCH_RGN_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_LOCAL_SEARCH_RGN_Y.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_LOCAL_SEARCH_RGN_Y.ForeColor = System.Drawing.Color.White;
            this.TXT_LOCAL_SEARCH_RGN_Y.Location = new System.Drawing.Point(195, 102);
            this.TXT_LOCAL_SEARCH_RGN_Y.Name = "TXT_LOCAL_SEARCH_RGN_Y";
            this.TXT_LOCAL_SEARCH_RGN_Y.Size = new System.Drawing.Size(57, 24);
            this.TXT_LOCAL_SEARCH_RGN_Y.TabIndex = 37;
            this.TXT_LOCAL_SEARCH_RGN_Y.Text = "0";
            // 
            // BTN_SET_EXTERNAL_IMAGE
            // 
            this.BTN_SET_EXTERNAL_IMAGE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.process;
            this.BTN_SET_EXTERNAL_IMAGE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_SET_EXTERNAL_IMAGE.Location = new System.Drawing.Point(226, 17);
            this.BTN_SET_EXTERNAL_IMAGE.Name = "BTN_SET_EXTERNAL_IMAGE";
            this.BTN_SET_EXTERNAL_IMAGE.Size = new System.Drawing.Size(60, 60);
            this.BTN_SET_EXTERNAL_IMAGE.TabIndex = 33;
            this.BTN_SET_EXTERNAL_IMAGE.UseVisualStyleBackColor = true;
            this.BTN_SET_EXTERNAL_IMAGE.Visible = false;
            this.BTN_SET_EXTERNAL_IMAGE.Click += new System.EventHandler(this.BTN_SET_EXTERNAL_IMAGE_Click);
            // 
            // TXT_LOCAL_SEARCH_RGN_W
            // 
            this.TXT_LOCAL_SEARCH_RGN_W.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TXT_LOCAL_SEARCH_RGN_W.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_LOCAL_SEARCH_RGN_W.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.TXT_LOCAL_SEARCH_RGN_W.ForeColor = System.Drawing.Color.White;
            this.TXT_LOCAL_SEARCH_RGN_W.Location = new System.Drawing.Point(263, 102);
            this.TXT_LOCAL_SEARCH_RGN_W.Name = "TXT_LOCAL_SEARCH_RGN_W";
            this.TXT_LOCAL_SEARCH_RGN_W.Size = new System.Drawing.Size(57, 24);
            this.TXT_LOCAL_SEARCH_RGN_W.TabIndex = 37;
            this.TXT_LOCAL_SEARCH_RGN_W.Text = "0";
            // 
            // CHK_PTRN_APPLY_EDGE_BASED
            // 
            this.CHK_PTRN_APPLY_EDGE_BASED.AutoSize = true;
            this.CHK_PTRN_APPLY_EDGE_BASED.ForeColor = System.Drawing.Color.White;
            this.CHK_PTRN_APPLY_EDGE_BASED.Location = new System.Drawing.Point(7, 293);
            this.CHK_PTRN_APPLY_EDGE_BASED.Name = "CHK_PTRN_APPLY_EDGE_BASED";
            this.CHK_PTRN_APPLY_EDGE_BASED.Size = new System.Drawing.Size(247, 21);
            this.CHK_PTRN_APPLY_EDGE_BASED.TabIndex = 38;
            this.CHK_PTRN_APPLY_EDGE_BASED.Text = "Apply Edge-Based Matching";
            this.CHK_PTRN_APPLY_EDGE_BASED.UseVisualStyleBackColor = true;
            this.CHK_PTRN_APPLY_EDGE_BASED.CheckedChanged += new System.EventHandler(this.CHK_PTRN_APPLY_EDGE_BASED_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(267, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "TARGET :";
            // 
            // PIC_PTRN_EDGE
            // 
            this.PIC_PTRN_EDGE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.PIC_PTRN_EDGE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PIC_PTRN_EDGE.Location = new System.Drawing.Point(5, 337);
            this.PIC_PTRN_EDGE.Margin = new System.Windows.Forms.Padding(4);
            this.PIC_PTRN_EDGE.Name = "PIC_PTRN_EDGE";
            this.PIC_PTRN_EDGE.Size = new System.Drawing.Size(250, 250);
            this.PIC_PTRN_EDGE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PIC_PTRN_EDGE.TabIndex = 30;
            this.PIC_PTRN_EDGE.TabStop = false;
            // 
            // BTN_PTRN_TEACH_OVERWRITE
            // 
            this.BTN_PTRN_TEACH_OVERWRITE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.new_recp;
            this.BTN_PTRN_TEACH_OVERWRITE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_TEACH_OVERWRITE.ForeColor = System.Drawing.Color.Black;
            this.BTN_PTRN_TEACH_OVERWRITE.Location = new System.Drawing.Point(431, 252);
            this.BTN_PTRN_TEACH_OVERWRITE.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_PTRN_TEACH_OVERWRITE.Name = "BTN_PTRN_TEACH_OVERWRITE";
            this.BTN_PTRN_TEACH_OVERWRITE.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_TEACH_OVERWRITE.TabIndex = 31;
            this.BTN_PTRN_TEACH_OVERWRITE.UseVisualStyleBackColor = true;
            this.BTN_PTRN_TEACH_OVERWRITE.Click += new System.EventHandler(this.BTN_PTRN_TEACH_OVERWRITE_Click);
            // 
            // BTN_PTRN_TEACH_NEW
            // 
            this.BTN_PTRN_TEACH_NEW.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.new_recp;
            this.BTN_PTRN_TEACH_NEW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_TEACH_NEW.ForeColor = System.Drawing.Color.Black;
            this.BTN_PTRN_TEACH_NEW.Location = new System.Drawing.Point(348, 252);
            this.BTN_PTRN_TEACH_NEW.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_PTRN_TEACH_NEW.Name = "BTN_PTRN_TEACH_NEW";
            this.BTN_PTRN_TEACH_NEW.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_TEACH_NEW.TabIndex = 31;
            this.BTN_PTRN_TEACH_NEW.UseVisualStyleBackColor = true;
            this.BTN_PTRN_TEACH_NEW.Click += new System.EventHandler(this.BTN_PTRN_TEACH_NEW_Click);
            // 
            // BTN_PTRN_DRAW
            // 
            this.BTN_PTRN_DRAW.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.measure;
            this.BTN_PTRN_DRAW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_PTRN_DRAW.ForeColor = System.Drawing.Color.Black;
            this.BTN_PTRN_DRAW.Location = new System.Drawing.Point(265, 252);
            this.BTN_PTRN_DRAW.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_PTRN_DRAW.Name = "BTN_PTRN_DRAW";
            this.BTN_PTRN_DRAW.Size = new System.Drawing.Size(60, 60);
            this.BTN_PTRN_DRAW.TabIndex = 31;
            this.BTN_PTRN_DRAW.UseVisualStyleBackColor = true;
            this.BTN_PTRN_DRAW.Click += new System.EventHandler(this.BTN_DRAW_PTRN_Click);
            // 
            // RICH_MESSAGE
            // 
            this.RICH_MESSAGE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.RICH_MESSAGE.ForeColor = System.Drawing.Color.White;
            this.RICH_MESSAGE.Location = new System.Drawing.Point(614, 597);
            this.RICH_MESSAGE.Name = "RICH_MESSAGE";
            this.RICH_MESSAGE.Size = new System.Drawing.Size(393, 145);
            this.RICH_MESSAGE.TabIndex = 39;
            this.RICH_MESSAGE.Text = "";
            // 
            // BTN_SET_INTERNAL_IMAGE
            // 
            this.BTN_SET_INTERNAL_IMAGE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.camera_info;
            this.BTN_SET_INTERNAL_IMAGE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_SET_INTERNAL_IMAGE.Location = new System.Drawing.Point(1013, 597);
            this.BTN_SET_INTERNAL_IMAGE.Name = "BTN_SET_INTERNAL_IMAGE";
            this.BTN_SET_INTERNAL_IMAGE.Size = new System.Drawing.Size(60, 60);
            this.BTN_SET_INTERNAL_IMAGE.TabIndex = 33;
            this.BTN_SET_INTERNAL_IMAGE.UseVisualStyleBackColor = true;
            this.BTN_SET_INTERNAL_IMAGE.Click += new System.EventHandler(this.BTN_SET_INTERNAL_IMAGE_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BTN_SHOW_AND_HIDE);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.uc_view_ptrn);
            this.panel1.Controls.Add(this.BTN_PTRN_APPLY);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.RICH_MESSAGE);
            this.panel1.Controls.Add(this.BTN_SET_INTERNAL_IMAGE);
            this.panel1.Controls.Add(this.BTN_PTRN_CANCEL);
            this.panel1.Location = new System.Drawing.Point(8, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1290, 750);
            this.panel1.TabIndex = 40;
            // 
            // BTN_SHOW_AND_HIDE
            // 
            this.BTN_SHOW_AND_HIDE.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.recipe_right;
            this.BTN_SHOW_AND_HIDE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_SHOW_AND_HIDE.Location = new System.Drawing.Point(1249, 714);
            this.BTN_SHOW_AND_HIDE.Name = "BTN_SHOW_AND_HIDE";
            this.BTN_SHOW_AND_HIDE.Size = new System.Drawing.Size(30, 30);
            this.BTN_SHOW_AND_HIDE.TabIndex = 90;
            this.BTN_SHOW_AND_HIDE.UseVisualStyleBackColor = true;
            this.BTN_SHOW_AND_HIDE.Click += new System.EventHandler(this.BTN_SHOW_AND_HIDE_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(1154, 660);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 14);
            this.label16.TabIndex = 77;
            this.label16.Text = "APPLY";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(1009, 659);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 78;
            this.label10.Text = "LOAD_IN";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(1086, 660);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 14);
            this.label17.TabIndex = 78;
            this.label17.Text = "CANCEL";
            // 
            // uc_view_ptrn
            // 
            this.uc_view_ptrn.AllowDrop = true;
            this.uc_view_ptrn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uc_view_ptrn.BOOL_DRAW_CROSS = true;
            this.uc_view_ptrn.BOOL_DRAW_FOCUS_ROI = true;
            this.uc_view_ptrn.BOOL_DRAW_PTRN_ROI = true;
            this.uc_view_ptrn.BOOL_TEACHING_ACTIVATION = false;
            this.uc_view_ptrn.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_view_ptrn.ForeColor = System.Drawing.Color.Lime;
            this.uc_view_ptrn.Location = new System.Drawing.Point(0, 0);
            this.uc_view_ptrn.Margin = new System.Windows.Forms.Padding(4);
            this.uc_view_ptrn.Name = "uc_view_ptrn";
            this.uc_view_ptrn.PT_FIGURE_TO_DRAW = ((System.Drawing.PointF)(resources.GetObject("uc_view_ptrn.PT_FIGURE_TO_DRAW")));
            this.uc_view_ptrn.ROI_INDEX = -1;
            this.uc_view_ptrn.Size = new System.Drawing.Size(600, 700);
            this.uc_view_ptrn.TabIndex = 57;
            this.uc_view_ptrn.Load += new System.EventHandler(this.uc_view_history_Load);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1714, 732);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 14);
            this.label13.TabIndex = 80;
            this.label13.Text = "UPDATE";
            // 
            // BTN_UPDATE_HISTORY
            // 
            this.BTN_UPDATE_HISTORY.BackgroundImage = global::CD_VISION_DIALOG.Properties.Resources.quick_restart;
            this.BTN_UPDATE_HISTORY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BTN_UPDATE_HISTORY.Location = new System.Drawing.Point(1714, 671);
            this.BTN_UPDATE_HISTORY.Name = "BTN_UPDATE_HISTORY";
            this.BTN_UPDATE_HISTORY.Size = new System.Drawing.Size(60, 60);
            this.BTN_UPDATE_HISTORY.TabIndex = 79;
            this.BTN_UPDATE_HISTORY.UseVisualStyleBackColor = true;
            this.BTN_UPDATE_HISTORY.Click += new System.EventHandler(this.BTN_UPDATE_HISTORY_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label14.Location = new System.Drawing.Point(1312, 17);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 17);
            this.label14.TabIndex = 83;
            this.label14.Text = ":- HISTORY";
            // 
            // LV_HISTORY_PTRN_ERROR
            // 
            this.LV_HISTORY_PTRN_ERROR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.LV_HISTORY_PTRN_ERROR.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.LV_HISTORY_PTRN_ERROR.ForeColor = System.Drawing.Color.White;
            this.LV_HISTORY_PTRN_ERROR.FullRowSelect = true;
            this.LV_HISTORY_PTRN_ERROR.GridLines = true;
            this.LV_HISTORY_PTRN_ERROR.Location = new System.Drawing.Point(1304, 44);
            this.LV_HISTORY_PTRN_ERROR.MultiSelect = false;
            this.LV_HISTORY_PTRN_ERROR.Name = "LV_HISTORY_PTRN_ERROR";
            this.LV_HISTORY_PTRN_ERROR.Size = new System.Drawing.Size(468, 621);
            this.LV_HISTORY_PTRN_ERROR.TabIndex = 84;
            this.LV_HISTORY_PTRN_ERROR.UseCompatibleStateImageBehavior = false;
            this.LV_HISTORY_PTRN_ERROR.View = System.Windows.Forms.View.Details;
            this.LV_HISTORY_PTRN_ERROR.SelectedIndexChanged += new System.EventHandler(this.LV_HISTORY_PTRN_ERROR_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IDX";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "DATE";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 113;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DATA";
            this.columnHeader4.Width = 225;
            // 
            // DLG_Ptrn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(1784, 762);
            this.Controls.Add(this.LV_HISTORY_PTRN_ERROR);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.BTN_UPDATE_HISTORY);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DLG_Ptrn";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "SETTING FOR PATTERN MATCHING";
            this.Load += new System.EventHandler(this.DLG_Ptrn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PIC_PTRN_NORMAL)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GB_PTRN_SEARCH_RGN.ResumeLayout(false);
            this.GB_PTRN_SEARCH_RGN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_PTRN_EDGE)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LV_PTRN;
        private System.Windows.Forms.ColumnHeader IDX;
        private System.Windows.Forms.ColumnHeader FIELS;
        private System.Windows.Forms.PictureBox PIC_PTRN_NORMAL;
        private System.Windows.Forms.Button BTN_UPDATE_PTRN_LIST;
        private System.Windows.Forms.Button BTN_MATCHING;
        private System.Windows.Forms.TextBox TXT_PTRN_FILE_NAME;
        private System.Windows.Forms.Button BTN_PTRN_CANCEL;
        private System.Windows.Forms.Button BTN_PTRN_APPLY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CHK_PTRN_APPLY_EDGE_BASED;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.PictureBox PIC_PTRN_EDGE;
        private System.Windows.Forms.TextBox TXT_PTRN_ACC_RATIO;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.RichTextBox RICH_MESSAGE;
        private System.Windows.Forms.Button BTN_PTRN_DRAW;
        private System.Windows.Forms.Button BTN_PTRN_TEACH_NEW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CHK_PTRN_SEARCH_GLOBAL;
        private System.Windows.Forms.TextBox TXT_PTRN_TEACH_ROI_H;
        private System.Windows.Forms.TextBox TXT_PTRN_TEACH_ROI_W;
        private System.Windows.Forms.TextBox TXT_PTRN_TEACH_ROI_Y;
        private System.Windows.Forms.TextBox TXT_PTRN_TEACH_ROI_X;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox GB_PTRN_SEARCH_RGN;
        private System.Windows.Forms.TextBox TXT_LOCAL_SEARCH_RGN_X;
        private System.Windows.Forms.TextBox TXT_LOCAL_SEARCH_RGN_H;
        private System.Windows.Forms.TextBox TXT_LOCAL_SEARCH_RGN_Y;
        private System.Windows.Forms.TextBox TXT_LOCAL_SEARCH_RGN_W;
        private System.Windows.Forms.Button BTN_PTRN_SET_LOCAL_SEARCHING_RGN;
        private System.Windows.Forms.Button BTN_PTRN_GET_LOCAL_SEARCHING_RGN;
        private System.Windows.Forms.Button BTN_SET_INTERNAL_IMAGE;
        private System.Windows.Forms.Panel panel1;
        private CD_View.UC_CD_VIEWER uc_view_ptrn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BTN_PTRN_TEACH_OVERWRITE;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BTN_SYNC_WITH_RECP_NAME;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BTN_SET_EXTERNAL_IMAGE;
        private System.Windows.Forms.Button BTN_SHOW_AND_HIDE;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button BTN_UPDATE_HISTORY;
        public System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListView LV_HISTORY_PTRN_ERROR;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button BTN_RGN_INFO_PTRN;
        private System.Windows.Forms.Button BTN_RGN_INFO_SEARCH;
    }
}
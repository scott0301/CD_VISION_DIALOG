namespace CD_Paramter
{
    partial class pnl_parameter
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
            this.property = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // property
            // 
            this.property.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.property.CategoryForeColor = System.Drawing.Color.Black;
            this.property.CommandsBackColor = System.Drawing.SystemColors.Desktop;
            this.property.CommandsBorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.property.CommandsDisabledLinkColor = System.Drawing.Color.Red;
            this.property.CommandsForeColor = System.Drawing.Color.Purple;
            this.property.CommandsLinkColor = System.Drawing.Color.White;
            this.property.DisabledItemForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property.Font = new System.Drawing.Font("Verdana", 9F);
            this.property.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.property.HelpBorderColor = System.Drawing.SystemColors.Control;
            this.property.HelpForeColor = System.Drawing.Color.LightCoral;
            this.property.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.property.LineColor = System.Drawing.SystemColors.Control;
            this.property.Location = new System.Drawing.Point(0, 0);
            this.property.Name = "property";
            this.property.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.property.SelectedItemWithFocusForeColor = System.Drawing.Color.Black;
            this.property.Size = new System.Drawing.Size(361, 426);
            this.property.TabIndex = 1;
            this.property.ViewBackColor = System.Drawing.Color.Silver;
            this.property.ViewForeColor = System.Drawing.SystemColors.Desktop;
            this.property.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.property_PropertyValueChanged);
            // 
            // pnl_parameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.property);
            this.Name = "pnl_parameter";
            this.Size = new System.Drawing.Size(361, 426);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid property;
    }
}

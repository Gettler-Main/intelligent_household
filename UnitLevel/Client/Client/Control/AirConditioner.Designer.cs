namespace Client.Control
{
    partial class AirConditioner
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiSwitch1 = new Sunny.UI.UISwitch();
            this.uiIntegerUpDown1 = new Sunny.UI.UIIntegerUpDown();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.uiLabel3.Location = new System.Drawing.Point(77, 114);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(85, 44);
            this.uiLabel3.TabIndex = 9;
            this.uiLabel3.Text = "操作";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.uiLabel1.Location = new System.Drawing.Point(150, 15);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(131, 59);
            this.uiLabel1.TabIndex = 6;
            this.uiLabel1.Text = "空调";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiSwitch1
            // 
            this.uiSwitch1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitch1.Location = new System.Drawing.Point(219, 114);
            this.uiSwitch1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitch1.Name = "uiSwitch1";
            this.uiSwitch1.Size = new System.Drawing.Size(102, 51);
            this.uiSwitch1.TabIndex = 5;
            this.uiSwitch1.Text = "uiSwitch1";
            this.uiSwitch1.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
            // 
            // uiIntegerUpDown1
            // 
            this.uiIntegerUpDown1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiIntegerUpDown1.Location = new System.Drawing.Point(187, 197);
            this.uiIntegerUpDown1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiIntegerUpDown1.MinimumSize = new System.Drawing.Size(100, 0);
            this.uiIntegerUpDown1.Name = "uiIntegerUpDown1";
            this.uiIntegerUpDown1.ReadOnly = true;
            this.uiIntegerUpDown1.ShowText = false;
            this.uiIntegerUpDown1.Size = new System.Drawing.Size(111, 36);
            this.uiIntegerUpDown1.TabIndex = 10;
            this.uiIntegerUpDown1.Text = "uiIntegerUpDown1";
            this.uiIntegerUpDown1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiIntegerUpDown1.Value = 26;
            this.uiIntegerUpDown1.ValueChanged += new Sunny.UI.UIIntegerUpDown.OnValueChanged(this.uiIntegerUpDown1_ValueChanged);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.uiLabel4.Location = new System.Drawing.Point(77, 189);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(74, 44);
            this.uiLabel4.TabIndex = 11;
            this.uiLabel4.Text = "温度";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.uiLabel5.Location = new System.Drawing.Point(305, 197);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(66, 36);
            this.uiLabel5.TabIndex = 12;
            this.uiLabel5.Text = "℃";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AirConditioner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiLabel5);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.uiIntegerUpDown1);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiSwitch1);
            this.Name = "AirConditioner";
            this.Size = new System.Drawing.Size(412, 322);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISwitch uiSwitch1;
        private Sunny.UI.UIIntegerUpDown uiIntegerUpDown1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel5;
    }
}

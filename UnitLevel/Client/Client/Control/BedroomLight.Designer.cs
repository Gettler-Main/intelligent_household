namespace Client.Control
{
    partial class BedroomLight
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
            this.uiSwitch1 = new Sunny.UI.UISwitch();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLight1 = new Sunny.UI.UILight();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // uiSwitch1
            // 
            this.uiSwitch1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitch1.Location = new System.Drawing.Point(215, 205);
            this.uiSwitch1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitch1.Name = "uiSwitch1";
            this.uiSwitch1.Size = new System.Drawing.Size(102, 51);
            this.uiSwitch1.TabIndex = 0;
            this.uiSwitch1.Text = "uiSwitch1";
            this.uiSwitch1.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.uiLabel1.Location = new System.Drawing.Point(123, 29);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(167, 69);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "卧室灯";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.Click += new System.EventHandler(this.uiLabel1_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.uiLabel2.Location = new System.Drawing.Point(60, 127);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(126, 44);
            this.uiLabel2.TabIndex = 2;
            this.uiLabel2.Text = "连接状态";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.Click += new System.EventHandler(this.uiLabel2_Click);
            // 
            // uiLight1
            // 
            this.uiLight1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLight1.Location = new System.Drawing.Point(240, 135);
            this.uiLight1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLight1.Name = "uiLight1";
            this.uiLight1.Radius = 36;
            this.uiLight1.Size = new System.Drawing.Size(41, 36);
            this.uiLight1.State = Sunny.UI.UILightState.Off;
            this.uiLight1.TabIndex = 3;
            this.uiLight1.Text = "uiLight1";
            this.uiLight1.Click += new System.EventHandler(this.uiLight1_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.uiLabel3.Location = new System.Drawing.Point(73, 205);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(85, 44);
            this.uiLabel3.TabIndex = 4;
            this.uiLabel3.Text = "操作";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.Click += new System.EventHandler(this.uiLabel3_Click);
            // 
            // BedroomLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLight1);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiSwitch1);
            this.Name = "BedroomLight";
            this.Size = new System.Drawing.Size(412, 322);
            this.Load += new System.EventHandler(this.BedroomLight_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISwitch uiSwitch1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILight uiLight1;
        private Sunny.UI.UILabel uiLabel3;
    }
}

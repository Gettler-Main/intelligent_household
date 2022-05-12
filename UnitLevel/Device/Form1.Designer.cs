namespace Device
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.AirCondition_pictureBox = new System.Windows.Forms.PictureBox();
            this.AirCondition_label1 = new System.Windows.Forms.Label();
            this.AirCondition_label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AirCondition_label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AirCondition_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AirCondition_pictureBox
            // 
            this.AirCondition_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AirCondition_pictureBox.Image = global::Device.Properties.Resources.AirConditionOff;
            this.AirCondition_pictureBox.Location = new System.Drawing.Point(354, 92);
            this.AirCondition_pictureBox.Name = "AirCondition_pictureBox";
            this.AirCondition_pictureBox.Size = new System.Drawing.Size(629, 349);
            this.AirCondition_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AirCondition_pictureBox.TabIndex = 0;
            this.AirCondition_pictureBox.TabStop = false;
            // 
            // AirCondition_label1
            // 
            this.AirCondition_label1.AutoSize = true;
            this.AirCondition_label1.Font = new System.Drawing.Font("宋体", 16F);
            this.AirCondition_label1.Location = new System.Drawing.Point(415, 486);
            this.AirCondition_label1.Name = "AirCondition_label1";
            this.AirCondition_label1.Size = new System.Drawing.Size(66, 27);
            this.AirCondition_label1.TabIndex = 1;
            this.AirCondition_label1.Text = "空调";
            // 
            // AirCondition_label2
            // 
            this.AirCondition_label2.AutoSize = true;
            this.AirCondition_label2.Font = new System.Drawing.Font("宋体", 16F);
            this.AirCondition_label2.Location = new System.Drawing.Point(786, 486);
            this.AirCondition_label2.Name = "AirCondition_label2";
            this.AirCondition_label2.Size = new System.Drawing.Size(120, 27);
            this.AirCondition_label2.TabIndex = 2;
            this.AirCondition_label2.Text = "状态：关";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 36F);
            this.label1.Location = new System.Drawing.Point(498, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 60);
            this.label1.TabIndex = 24;
            this.label1.Text = "Devices";
            // 
            // AirCondition_label3
            // 
            this.AirCondition_label3.AutoSize = true;
            this.AirCondition_label3.Font = new System.Drawing.Font("宋体", 16F);
            this.AirCondition_label3.Location = new System.Drawing.Point(487, 486);
            this.AirCondition_label3.Name = "AirCondition_label3";
            this.AirCondition_label3.Size = new System.Drawing.Size(67, 27);
            this.AirCondition_label3.TabIndex = 25;
            this.AirCondition_label3.Text = "26℃";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1449, 767);
            this.Controls.Add(this.AirCondition_label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AirCondition_label2);
            this.Controls.Add(this.AirCondition_label1);
            this.Controls.Add(this.AirCondition_pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AirCondition_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AirCondition_pictureBox;
        private System.Windows.Forms.Label AirCondition_label1;
        private System.Windows.Forms.Label AirCondition_label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label AirCondition_label3;
    }
}


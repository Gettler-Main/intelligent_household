using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Control;
using Sunny.UI;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.ImageIndex = 0;
            tabPage2.ImageIndex = 0;
            BedroomLight bedroomLight = new BedroomLight();
            bedroomLight.Dock = DockStyle.Fill;
            KitchenLight kitchenLight = new KitchenLight();
            kitchenLight.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(bedroomLight);
            tabPage2.Controls.Add(kitchenLight);
            uiTabControlMenu1.SizeMode = TabSizeMode.Normal;

            //设置选项卡属性为fixed
        }

    }
}

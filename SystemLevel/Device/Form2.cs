using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Device
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        Dictionary<Socket, string> socketNames = new Dictionary<Socket, string>();

        string[,] devices = new string[10, 5]//名称，端口，连接，开关，详情
        {
            {"LivingroomCurtain"," ", "0", "0"," "},
            {"LivingroomLight"," ", "0", "0"," "},
            {"LivingroomAirCondition"," ", "0", "0"," "},
            {"KitchenLight"," ", "0", "0"," "},
            {"Cooker"," ", "0", "0"," "},
            {"BedroomCurtain"," ", "0", "0"," "},
            {"BedroomAirCondition"," ", "0", "0"," "},
            {"BedroomLight"," ", "0", "0"," "},
            {"Strip"," ", "0", "0"," "},
            {"Calorifier"," ", "0", "0"," "},

        };

        public string nowshowDevice;
        private void Form2_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; // 窗体控件不检查线程操作
        }

        public string changeEtoC(string Ename)
        {
            string Cname = "";
            switch (Ename)
            {
                case "LivingroomCurtain":
                    Cname = "客厅窗帘";
                    break;
                case "LivingroomLight":
                    Cname = "客厅灯";
                    break;
                case "LivingroomAirCondition":
                    Cname = "客厅空调";
                    break;
                case "KitchenLight":
                    Cname = "厨房灯";
                    break;
                case "Cooker":
                    Cname = "电饭煲";
                    break;
                case "BedroomCurtain":
                    Cname = "卧室窗帘";
                    break;
                case "BedroomAirCondition":
                    Cname = "卧室空调";
                    break;
                case "BedroomLight":
                    Cname = "卧室灯";
                    break;
                case "Strip":
                    Cname = "插排";
                    break;
                case "Calorifier":
                    Cname = "热水器";
                    break;
            }
            return Cname;
        }
        public string changeCtoE(string Cname)
        {
            string Ename = "";
            switch (Cname)
            {
                case "客厅窗帘":
                    Ename = "LivingroomCurtain";
                    break;
                case "客厅灯":
                    Ename = "LivingroomLight";
                    break;
                case "客厅空调":
                    Ename = "LivingroomAirCondition";
                    break;
                case "厨房灯":
                    Ename = "KitchenLight";
                    break;
                case "电饭煲":
                    Ename = "Cooker";
                    break;
                case "卧室窗帘":
                    Ename = "BedroomCurtain";
                    break;
                case "卧室空调":
                    Ename = "BedroomAirCondition";
                    break;
                case "卧室灯":
                    Ename = "BedroomLight";
                    break;
                case "插排":
                    Ename = "Strip";
                    break;
                case "热水器":
                    Ename = "Calorifier";
                    break;
            }
            return Ename;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
        //客厅窗帘
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "客厅窗帘";
            if (devices[0,2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[0, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a%255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[0, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[0, 4];
        }
        //客厅灯
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "客厅灯";
            if (devices[1, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[1, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[1, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[1, 4];
            
        }
        //客厅空调
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "客厅空调";
            if (devices[2, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[2, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[2, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[2, 4];
        }
        //厨房灯
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "厨房灯";
            if (devices[3, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[3, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[3, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[3, 4];
        }
        //电饭煲
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "电饭煲";
            if (devices[4, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[4, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[4, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[4, 4];
        }
        //卧室窗帘
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "卧室窗帘";
            if (devices[5, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[5, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[5, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[5, 4];
        }
        //卧室空调
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "卧室空调";
            if (devices[6, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[6, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[6, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[6, 4];
        }
        //卧室灯
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "卧室灯";
            if (devices[7, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[7, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[7, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[7, 4];
        }
        //插排
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "插排";
            if (devices[8, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[8, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[8, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[8, 4];
        }
        //热水器
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "热水器";
            if (devices[9, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[9, 1]);
                uiLight1.OnColor = Color.FromArgb(a % 254, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[9, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[9, 4];
        }
    }
}

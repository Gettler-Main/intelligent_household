using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace enviroment
{
    public partial class Form1 : Form
    {
        public Socket socket;
        string[] patterns = { "风和日丽", "骄阳似火", "秋高气爽", "大雪纷飞" };
        int shine = 0, time = 0, temp = 0;

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < patterns.Length; i++)
            {
                comboBox1.Items.Add(patterns[i]);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void send(string msg)
        {
            //构造字节数组
            Byte[] byteNum = new Byte[1024];
            Byte[] byteNum2 = Encoding.UTF8.GetBytes(msg.ToCharArray());
            //将字符串转换为字节数组
            int idx = 0;
            for (int i = 0; i < byteNum2.Length; i++)
            {
                byteNum[idx] = byteNum2[i];
                idx++;
            }
            //发送数据
            socket.Send(byteNum, byteNum.Length, 0);
        }
        void initConnect(int port)
        {
            try
            {
                //1、创建socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定ip和端口
                //String ip = "127.0.10.1";
                String ip = "47.93.12.205";
                socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                //Byte[] byteNum = new Byte[64];
                //byteNum = System.Text.Encoding.UTF8.GetBytes(("Name-" + deviceName).ToCharArray());
                //socket.Send(byteNum, byteNum.Length, 0); // 发送身份信息
                send("Name-Environment");
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化失败" + ex.ToString());
            }
        }

        public int getRandom(int minNum, int maxNum)
        {
            Random rd = new Random((int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
            return rd.Next(minNum, maxNum + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = Convert.ToInt32(textBox1.Text);
            if (port != 0)
            {
                initConnect(port);
                MessageBox.Show(port.ToString() + "连接成功");
                if (temp > 32)
                {
                    send("LivingroomAirCondition:OP1");
                    Thread.Sleep(1000);
                    send("BedroomAirCondition:OP1");
                    Thread.Sleep(1000);
                }
                if (shine > 1)
                {
                    send("LivingroomLight:OP0");
                    Thread.Sleep(1000);
                    send("KitchenLight:OP0");
                    Thread.Sleep(1000);
                    send("BedroomLight:OP0");
                    Thread.Sleep(1000);
                }
                if (shine <= 1)
                {
                    send("LivingroomLight:OP1");
                    Thread.Sleep(1000);
                    send("KitchenLight:OP1");
                    Thread.Sleep(1000);
                    send("BedroomLight:OP1");
                    Thread.Sleep(1000);
                }
                if (time > 23 || time < 7)
                {
                    send("LivingroomLight:OP0");
                    Thread.Sleep(1000);
                    send("KitchenLight:OP0");
                    Thread.Sleep(1000);
                    send("BedroomLight:OP0");
                    Thread.Sleep(1000);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    shine = getRandom(1, 3);
                    temp = getRandom(20, 26);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    shine = getRandom(3, 5);
                    temp = getRandom(26, 36);
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    shine = getRandom(1, 3);
                    temp = getRandom(20, 26);
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    shine = getRandom(0, 1);
                    temp = getRandom(10, 20);
                }
                label2.Text = "光照:" + shine.ToString() + "级";
                label3.Text = "温度:" + temp.ToString() + "℃";
                time = getRandom(0, 23);
                label4.Text = "时间 " + string.Format("{0:00}", time) + ":" + string.Format("{0:00}", getRandom(0, 60));

            }
        }
    }
}

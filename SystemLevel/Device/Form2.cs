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
using System.Threading;
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
            {"LivingroomCurtain","", "0", "0"," "},
            {"LivingroomLight","", "0", "0"," "},
            {"LivingroomAirCondition","", "0", "0"," "},
            {"KitchenLight","", "0", "0"," "},
            {"Cooker","", "0", "0"," "},
            {"BedroomCurtain","", "0", "0"," "},
            {"BedroomAirCondition","", "0", "0"," "},
            {"BedroomLight","", "0", "0"," "},
            {"Strip","", "0", "0"," "},
            {"Calorifier","", "0", "0"," "},

        };

        public int nowshowDevice=0;
        private void Form2_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; // 窗体控件不检查线程操作
        }
        public int changeEtoN(string Ename)
        {
            int num = 0;
            switch (Ename)
            {
                case "LivingroomCurtain":
                    num = 0;
                    break;
                case "LivingroomLight":
                    num = 1;
                    break;
                case "LivingroomAirCondition":
                    num = 2;
                    break;
                case "KitchenLight":
                    num = 3;
                    break;
                case "Cooker":
                    num = 4;
                    break;
                case "BedroomCurtain":
                    num = 5;
                    break;
                case "BedroomAirCondition":
                    num = 6;
                    break;
                case "BedroomLight":
                    num = 7;
                    break;
                case "Strip":
                    num = 8;
                    break;
                case "Calorifier":
                    num = 9;
                    break;
            }
            return num;

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
        
        void openDevice(string deviceName)
        {
            int num = changeEtoN(deviceName);
            devices[num, 3] = "1";
            PictureBox pb = (PictureBox)Controls[deviceName + "_pictureBox"];
            if (deviceName == "LivingroomAirCondition" || deviceName == "BedroomAirCondition")
            {
                deviceName = "AirCondition";
            }
            else if (deviceName == "LivingroomLight" || deviceName == "KitchenLight")
            {
                deviceName = "Light";
            }
            else if(deviceName== "BedroomCurtain"||deviceName== "LivingroomCurtain")
            {
                deviceName = "Curtain";
            }
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(deviceName + "On", Properties.Resources.Culture);
            if (nowshowDevice == num)
            {
                showchange(num);
            }
        }

        void closeDevice(string deviceName)
        {
            int num = changeEtoN(deviceName);
            devices[num, 3] = "0";
            PictureBox pb = (PictureBox)Controls[deviceName + "_pictureBox"];
            if (deviceName == "LivingroomAirCondition" || deviceName == "BedroomAirCondition")
            {
                deviceName = "AirCondition";
            }
            else if (deviceName == "LivingroomLight" || deviceName == "KitchenLight")
            {
                deviceName = "Light";
            }
            else if (deviceName == "BedroomCurtain" || deviceName == "LivingroomCurtain")
            {
                deviceName = "Curtain";
            }
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(deviceName + "Off", Properties.Resources.Culture);
            if (nowshowDevice == num)
            {
                showchange(num);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initConnect(nowshowDevice);
        }

        void initConnect(int nowshowDevice)
        {
            string deviceName = devices[nowshowDevice, 0];
            try
            {
                //1、创建socket
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定ip和端口
                //String ip = "127.0.10.1";
                String ip = "47.93.12.205";
                devices[nowshowDevice, 1] = textBox1.Text;
                int port = Convert.ToInt32(devices[nowshowDevice, 1]);
                socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                Byte[] byteNum = new Byte[64];
                byteNum = System.Text.Encoding.UTF8.GetBytes(("Name-" + deviceName).ToCharArray());
                socket.Send(byteNum, byteNum.Length, 0); // 发送身份信息
                Thread thread = new Thread(Receive); // 开启一个线程
                thread.IsBackground = true;
                thread.Start(socket);
                sockets.Add(deviceName, socket);
                socketNames.Add(socket, deviceName);
                devices[nowshowDevice, 2] = "1";
                showchange(nowshowDevice);
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化失败" + ex.ToString());
            }
        }

        /// <summary>
        /// 接受服务端发送过来的消息
        /// </summary>
        /// <param name="o"></param>
        void Receive(object o)
        {
            Socket socketSend = o as Socket;
            string deviceName = socketNames[socketSend];
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 2];
                int r = socketSend.Receive(buffer);
                devices[changeEtoN(deviceName), 2] = "1";
                while (true)
                {
                    if (r != 0)
                    {
                        string str = Encoding.UTF8.GetString(buffer, 0, r);
                        //MessageBox.Show(str);
                        if (str[0] == '1')
                        {
                            if (deviceName == "LivingroomAirCondition" || deviceName == "BedroomAirCondition")
                            {
                                int temp =26;
                                devices[changeEtoN(deviceName), 4] = temp.ToString() + "℃";
                            }
                            else if (deviceName == "LivingroomLight" || deviceName == "KitchenLight")
                            {
                                devices[changeEtoN(deviceName), 4] = "弱光照";
                            }
                            else if (deviceName == "BedroomLight")
                            {
                                devices[changeEtoN(deviceName), 4] = "柔和";
                            }
                            openDevice(deviceName);
                        }
                        else if (str[0] == '0')
                            closeDevice(deviceName);
                        else if (str[0] == '2' && str.Length >= 2)
                        {
                            if(deviceName== "LivingroomAirCondition"|| deviceName == "BedroomAirCondition")
                            {
                                int temp = (str[1] - '0') * 10 + (str[2] - '0');
                                devices[changeEtoN(deviceName), 4] = temp.ToString() + "℃";
                            }
                            else if (deviceName == "LivingroomLight" || deviceName == "KitchenLight")
                            {

                                PictureBox pb = (PictureBox)Controls[deviceName + "_pictureBox"];
                                switch (str[1])
                                {
                                    case '0':
                                        devices[changeEtoN(deviceName), 4] = "弱光照";
                                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("LightOn1", Properties.Resources.Culture);
                                        break;
                                    case '1':
                                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("LightOn2", Properties.Resources.Culture);
                                        devices[changeEtoN(deviceName), 4] = "中光照";
                                        break;
                                    case '2':
                                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("LightOn3", Properties.Resources.Culture);
                                        devices[changeEtoN(deviceName), 4] = "强光照";
                                        break;
                                }
                            }
                            else if (deviceName == "BedroomLight")
                            {
                                PictureBox pb = (PictureBox)Controls[deviceName + "_pictureBox"];
                                switch (str[1])
                                {
                                    case '0':
                                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("LightSoft", Properties.Resources.Culture);
                                        devices[changeEtoN(deviceName), 4] = "柔和";
                                        break;
                                    case '1':
                                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("LightWhite", Properties.Resources.Culture);
                                        devices[changeEtoN(deviceName), 4] = "白光";
                                        break;
                                    case '2':
                                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject( "LightYellow", Properties.Resources.Culture);
                                        devices[changeEtoN(deviceName), 4] = "黄光";
                                        break;
                                }
                            }
                            showchange(changeEtoN(deviceName));
                        }
                        else if (str == "check")
                        {
                            send(socketSend, "Name-" + deviceName);//返回设备名
                        }
                        r = socketSend.Receive(buffer);
                    }
                }
            }
            catch
            {

            }

        }


        private void send(Socket socket, string msg)
        {
            //构造字节数组
            Byte[] byteNum = new Byte[64];
            //将字符串转换为字节数组
            byteNum = System.Text.Encoding.UTF8.GetBytes(msg.ToCharArray());
            //发送数据
            socket.Send(byteNum, byteNum.Length, 0);
        }



        public void showchange(int num)
        {
            nowshowDevice = num;
            groupBox1.Text = changeEtoC(devices[num,0]);
            if (devices[num, 2] == "0")
                uiLight1.State = UILightState.Off;
            else
            {
                int a = Convert.ToInt32(devices[num, 1]);
                uiLight1.OnColor = Color.FromArgb((a % 10)*20, 190, a % 255);
                uiLight1.State = UILightState.Blink;
            }
            if (devices[num, 3] == "0")
                uiSwitch1.Active = false;
            else
                uiSwitch1.Active = true;
            label4.Text = devices[num, 4];
            textBox1.Text= devices[num, 1];
            if (textBox1.Text != "")
            {
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
            }

            string name = devices[num, 0];
            if (name == "LivingroomAirCondition" || name == "BedroomAirCondition")
            {
                name = "AirCondition";
            }
            else if (name == "LivingroomLight" || name == "KitchenLight")
            {
                name = "Light";
            }
            else if (name == "BedroomCurtain" || name == "LivingroomCurtain")
            {
                name = "Curtain";
            }

            show_pictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(name + "Off", Properties.Resources.Culture);
        }



        //客厅窗帘
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            showchange(0);
        }
        //客厅灯
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            showchange(1);

        }
        //客厅空调
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            showchange(2);
        }
        //厨房灯
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            showchange(3);
        }
        //电饭煲
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            showchange(4);
        }
        //卧室窗帘
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            showchange(5);
        }
        //卧室空调
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            showchange(6);
        }
        //卧室灯
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            showchange(7);
        }
        //插排
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            showchange(8);
        }
        //热水器
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            showchange(9);
        }
    }
}

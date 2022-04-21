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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        Dictionary<Socket, string> socketNames = new Dictionary<Socket, string>();

        void openDevice(string deviceName)
        {
            Controls[deviceName + "_label2"].Text = "状态：开";
            PictureBox pb = (PictureBox)Controls[deviceName + "_pictureBox"];
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(deviceName + "On", Properties.Resources.Culture);
        }

        void closeDevice(string deviceName)
        {
            Controls[deviceName + "_label2"].Text = "状态：关";
            PictureBox pb = (PictureBox)Controls[deviceName + "_pictureBox"];
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(deviceName + "Off", Properties.Resources.Culture);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            initConnect("AirCondition");
        }

        void initConnect(string deviceName)
        {
            try
            {
                //1、创建socket
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定ip和端口
                //String ip = "127.0.10.1";
                String ip = "47.93.12.205";
                int port = 50000;
                socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                Thread thread = new Thread(Receive);
                thread.IsBackground = true;
                thread.Start(socket);
                sockets.Add(deviceName, socket);
                socketNames.Add(socket, deviceName);
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
                UILight uil = (UILight)Controls[deviceName + "_uiLight"];
                uil.State = UILightState.On;
                while (true)
                {
                    if (r != 0)
                    {

                        string str = Encoding.UTF8.GetString(buffer, 0, r);
                        uil.State = UILightState.Blink;
                        //MessageBox.Show(str);
                        if (str[0] == '1')
                            openDevice(deviceName);
                        else if (str[0] == '0')
                            closeDevice(deviceName);
                        else if (str[0] == '2' && str.Length >= 3)
                        {
                            int temp = (str[1] - '0') * 10 + (str[2] - '0');
                            Controls[deviceName + "_label3"].Text = temp.ToString() + "℃";
                        }
                        else if (str == "check")
                        {
                            send(socketSend, "Name-" + deviceName);//返回设备名
                        }
                        r = socketSend.Receive(buffer);
                    }
                }
                uil.State = UILightState.On;
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
    }
}

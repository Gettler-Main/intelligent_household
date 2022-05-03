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
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static Socket scoketClient;

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.ImageIndex = 0;
            tabPage2.ImageIndex = 0;
            BedroomLight bedroomLight = new BedroomLight();
            bedroomLight.Dock = DockStyle.Fill;
            KitchenLight kitchenLight = new KitchenLight();
            kitchenLight.Dock = DockStyle.Fill;
            AirConditioner airConditioner = new AirConditioner();
            airConditioner.Dock = DockStyle.Fill;
            Strip strip = new Strip();
            strip.Dock = DockStyle.Fill;
            LivingLight livingLight = new LivingLight();
            livingLight.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(bedroomLight);
            Logs logs = new Logs();
            logs.Dock = DockStyle.Fill;
            tabLogs.Controls.Add(logs);
            tabPage2.Controls.Add(kitchenLight);
            tabPage4.Controls.Add(airConditioner);
            uiTabControlMenu1.SizeMode = TabSizeMode.Normal;

            try
            {

                //1、创建socket
                scoketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定ip和端口
                //String ip = "127.0.10.1";
                String ip = "47.93.12.205";
                int port = 50000;
                scoketClient.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                Thread thread = new Thread(Receive);
                thread.IsBackground = true;
                thread.Start(scoketClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Receive(object o)
        {
            Socket socketSend = o as Socket;
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 2];
                int r = socketSend.Receive(buffer);
                while (true)
                {
                    if (r != 0)
                    {

                        string str = Encoding.UTF8.GetString(buffer, 0, r);
                        if (str == "check")
                        {
                            send(socketSend, "Name-Client");//返回设备名
                        }
                        r = socketSend.Receive(buffer);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void send(Socket socket, string msg)
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

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

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                //1、创建socket
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定ip和端口
                String ip = textBoxIP.Text;
                int port = Convert.ToInt32(textBoxPort.Text);
                socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                //3、开启监听
                socket.Listen(10);//等待连接队列的最大值
                //4、开始接受客户端的链接
                showMsg("服务器正常启动，开始接受客户端的数据");
                Thread thread = new Thread(listen);
                thread.IsBackground = true;
                thread.Start(socket);

            }
            catch (Exception ex)
            {
                MessageBox.Show("启动服务器失败" + ex.ToString());
            }
        }

        private void showMsg(String msg)
        {
            this.textBoxLog.Text += msg + "\r\n";
        }

        /// <summary>
        /// 等待客户端的链接，并创建与其通信的Socket
        /// </summary>
        /// <param name="socket"></param>
        private void listen(object socket)
        {
            try
            {
                Socket serverSockert = socket as Socket;//强制转换
                while (true)
                {
                    Socket socketSend = serverSockert.Accept();//接受连接
                    showMsg(socketSend.RemoteEndPoint.ToString() + ":连接成功！");
                    //开启新线程不停的接受当前链接的客户端发送的消息
                    Thread thread = new Thread(Receive);
                    thread.IsBackground = true;
                    thread.Start(socketSend);
                }
            }
            catch
            {

            }

        }
        /// <summary>
        /// 接受客户端发送过来的消息
        /// </summary>
        /// <param name="o"></param>
        void Receive(object o)
        {
            Socket socketSend = o as Socket;
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 2];
                int r = socketSend.Receive(buffer);
                while (r != 0)
                {
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    showMsg(socketSend.RemoteEndPoint + ":" + str);
                    r = socketSend.Receive(buffer);
                }
            }
            catch
            {

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}

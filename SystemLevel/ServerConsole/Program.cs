using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerConsole
{
    internal class Program
    {

        public static Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        public static Dictionary<Socket, string> socketNames = new Dictionary<Socket, string>();


        /// <summary>
        /// 等待客户端的链接，并创建与其通信的Socket
        /// </summary>
        /// <param name="socket"></param>
        private static void listen(object socket)
        {
            try
            {
                Socket serverSockert = socket as Socket;//强制转换
                while (true)
                {
                    Socket socketSend = serverSockert.Accept();//接受连接
                    //开启新线程不停的接受当前链接的客户端发送的消息
                    Thread thread = new Thread(Receive);
                    thread.IsBackground = true;
                    thread.Start(socketSend);
                    Console.WriteLine(socketSend.RemoteEndPoint.ToString() + ":连接成功！");
                    Byte[] byteNum = new Byte[64];
                    byteNum = System.Text.Encoding.UTF8.GetBytes("check".ToCharArray());
                    socketSend.Send(byteNum, byteNum.Length, 0);
                }
            }
            catch
            {

            }

        }


        private static void send(Socket socket, string msg)
        {
            //构造字节数组
            Byte[] byteNum = new Byte[64];
            //发送内容
            string send = "Send " + socketNames[socket] + ": " + msg + "\r\n";
            //将字符串转换为字节数组
            Console.WriteLine(send);

            byteNum = System.Text.Encoding.UTF8.GetBytes(msg.ToCharArray());
            //发送数据
            socket.Send(byteNum, byteNum.Length, 0);
        }



        /// <summary>
        /// 检查连接
        /// </summary>
        /// <param name="o"></param>
        static void checkLink(object o)
        {
            Socket socketSend = o as Socket;
            string ip = ((IPEndPoint)socketSend.LocalEndPoint).Address.ToString();
            while (true)
            {
                if (CanConnect(ip)) continue;
                else
                {
                    // 修改显示
                    break;
                }

            }
        }

        /// <summary>
        /// 接受客户端发送过来的消息
        /// </summary>
        /// <param name="o"></param>
        static void Receive(object o)
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
                        if (str.Substring(0, 5) == "Name-") // 传回的是身份信息
                        {
                            string name = str.Substring(5);
                            // 存储身份信息（设备名）
                            if (socketNames.ContainsKey(socketSend))
                                socketNames.Remove(socketSend);
                            if (sockets.ContainsKey(name))
                                sockets.Remove(name);
                            sockets.Add(name, socketSend);
                            socketNames.Add(socketSend, name);
                            Console.WriteLine(name + "已连接");
                        }
                        int t = str.IndexOf(':'); // 传输信息格式为 AirCondition:OP225
                        if (t != -1)
                        {
                            string tar = str.Substring(0, t);

                            Console.WriteLine(socketNames[socketSend] + " to " + tar + " : " + str.Substring(t + 1));

                            if (str.Length >= t + 4 && str.Substring(t + 1, 2) == "OP") // OP标注是否对设备端操作
                            {
                                string op = "";
                                op += str[t + 3];
                                if (str[t + 3] == '2' && str.Length >= t + 6) // OP后为2表示调整温度
                                {
                                    op += str.Substring(t + 4, 2);
                                }
                                // 向设备端发送命令
                                send(sockets[tar], op);
                            }
                        }
                    }
                    r = socketSend.Receive(buffer);
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// 判断是否连接正常
        /// </summary>
        /// <param name="ipstr"></param>
        /// <returns></returns>
        static bool CanConnect(string ipstr)
        {
            IPAddress ip = IPAddress.Parse(ipstr);
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(ip);
                return (reply.Status == IPStatus.Success);
            }
            catch (PingException)
            {
                return false;
            }
        }


        static void Main(string[] args)
        {
            try
            {
                //1、创建socket
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2、绑定ip和端口
                String ip = "0.0.0.0";
                int port = 50000;
                socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                //3、开启监听
                socket.Listen(20);//等待连接队列的最大值
                //4、开始接受客户端的链接
                Thread thread = new Thread(listen);
                thread.IsBackground = true;
                thread.Start(socket);
                Console.WriteLine("服务器正常启动，0.0.0.0:50000 开始接受客户端的数据");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            while (true)
            {
                Thread.Sleep(1);
            }
        }
    }
}
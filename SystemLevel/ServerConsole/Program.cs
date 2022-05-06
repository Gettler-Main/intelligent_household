using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ServerConsole
{
    internal class Program
    {

        public static Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        public static Dictionary<Socket, string> socketNames = new Dictionary<Socket, string>();
        public static string logs = "Logs：";

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

                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// 打包服务器数据
        /// </summary>
        /// <param name="message">数据</param>
        /// <returns>数据包</returns>
        private static byte[] PackData(string message)
        {
            byte[] contentBytes = null;
            byte[] temp = Encoding.UTF8.GetBytes(message);

            if (temp.Length < 126)
            {
                contentBytes = new byte[temp.Length + 2];
                contentBytes[0] = 0x81;
                contentBytes[1] = (byte)temp.Length;
                Array.Copy(temp, 0, contentBytes, 2, temp.Length);
            }
            //else if (temp.Length < 0xFFFF)
            //{
            //contentBytes = new byte[temp.Length + 4];
            //contentBytes[0] = 0x81;
            //contentBytes[1] = 126;
            //contentBytes[2] = (byte)(temp.Length & 0xFF);
            //contentBytes[3] = (byte)(temp.Length >> 8 & 0xFF);
            //Array.Copy(temp, 0, contentBytes, 4, temp.Length);
            //}
            return contentBytes;
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

        private static void PackeSend(Socket socket, string message)
        {

            byte[] contentBytes = null;
            byte[] temp = Encoding.UTF8.GetBytes(message);
            if (temp.Length < 126)
            {
                contentBytes = new byte[temp.Length + 2];
                contentBytes[0] = 0x81;
                contentBytes[1] = (byte)temp.Length;
                Array.Copy(temp, 0, contentBytes, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                contentBytes = new byte[temp.Length + 4];
                contentBytes[0] = 0x81;
                contentBytes[1] = 126;
                contentBytes[2] = (byte)(temp.Length >> 8);
                contentBytes[3] = (byte)(temp.Length & 0xFF);
                Array.Copy(temp, 0, contentBytes, 4, temp.Length);
            }
            else
            {
                contentBytes = new byte[temp.Length + 10];
                contentBytes[0] = 0x81;
                contentBytes[1] = 127;
                contentBytes[2] = 0;
                contentBytes[3] = 0;
                contentBytes[4] = 0;
                contentBytes[5] = 0;
                contentBytes[6] = (byte)(temp.Length >> 24);
                contentBytes[7] = (byte)(temp.Length >> 16);
                contentBytes[8] = (byte)(temp.Length >> 8);
                contentBytes[9] = (byte)(temp.Length & 0xFF);
                Array.Copy(temp, 0, contentBytes, 10, temp.Length);
            }

            //构造字节数组    
            //发送内容
            //将字符串转换为字节数组
            Console.WriteLine(contentBytes);
            //发送数据
            socket.Send(contentBytes);
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
            IPEndPoint clientipe = (IPEndPoint)socketSend.RemoteEndPoint;
            
            byte[] buffer = new byte[1024];
            int length = socketSend.Receive(buffer);
            string str = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine(str);
            if (str.Contains("WebSocket"))
            {
                Console.WriteLine("收到来自网页端的信息");
                socketSend.Send(PackHandShakeData(GetSecKeyAccetp(buffer, length)));
                Console.WriteLine("已经发送握手协议了");
                logs += "服务端发送握手协议...\n";
                //logs += name + "已连接\n";
                try
                {
                    while (true)
                    {
                        //接受客户端数据
                        try
                        {
                            length = socketSend.Receive(buffer);//接受客户端信息
                            if (length != 0)
                            {
                                string clientMsg = AnalyticData(buffer, length);
                                str = "" + clientMsg;
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
                                    logs += name + "已连接\n";
                                }
                                int t = str.IndexOf(':'); // 传输信息格式为 AirCondition:OP225
                                if (t != -1)
                                {
                                    string tar = str.Substring(0, t);
                                    Console.WriteLine(socketNames[socketSend] + " to " + tar + " : " + str.Substring(t + 1));
                                    logs += socketNames[socketSend] + " to " + tar + " : " + str.Substring(t + 1) + "\n";
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
                                else if (str.Length >= 7 && str.Substring(0, 7) == "getLogs")
                                {
                                    logs += socketNames[socketSend] + " : want to get logs\n";
                                    PackeSend(sockets["Client"], logs);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                catch (Exception e)
                {
                }

            }
            else // C#客户端发来的Socket
            {
                try
                {
                    Console.WriteLine(socketSend.RemoteEndPoint.ToString() + ":连接成功！");
                    Byte[] byteNum = new Byte[64];
                    byteNum = System.Text.Encoding.UTF8.GetBytes("check".ToCharArray());
                    socketSend.Send(byteNum, byteNum.Length, 0);
                    buffer = new byte[1024 * 1024 * 2];
                    //int r = socketSend.Receive(buffer);
                    while (true)
                    {
                        if (length != 0)
                        {
                            str = Encoding.UTF8.GetString(buffer, 0, length);
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
                                logs += name + "已连接\n";
                            }
                            int t = str.IndexOf(':'); // 传输信息格式为 AirCondition:OP225
                            if (t != -1)
                            {
                                string tar = str.Substring(0, t);
                                Console.WriteLine(socketNames[socketSend] + " to " + tar + " : " + str.Substring(t + 1));
                                logs += socketNames[socketSend] + " to " + tar + " : " + str.Substring(t + 1) + "\n";
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
                            else if (str.Length >= 7 && str.Substring(0, 7) == "getLogs")
                            {
                                logs += socketNames[socketSend] + " : want to get logs\n";
                                send(sockets["Client"], logs);
                            }
                        }
                        length = socketSend.Receive(buffer);
                    }
                }
                catch
                {

                }

            }
        }


        /// <summary>
        /// 打包握手信息
        /// </summary>
        /// <param name="secKeyAccept">Sec-WebSocket-Accept</param>
        /// <returns>数据包</returns>
        private static byte[] PackHandShakeData(string secKeyAccept)
        {
            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + Environment.NewLine);
            responseBuilder.Append("Upgrade: websocket" + Environment.NewLine);
            responseBuilder.Append("Connection: Upgrade" + Environment.NewLine);
            responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine + Environment.NewLine);

            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }

        /// <summary>
        /// 生成Sec-WebSocket-Accept
        /// </summary>
        /// <param name="handShakeText">客户端握手信息</param>
        /// <returns>Sec-WebSocket-Accept</returns>
        private static string GetSecKeyAccetp(byte[] handShakeBytes, int bytesLength)
        {
            string handShakeText = Encoding.UTF8.GetString(handShakeBytes, 0, bytesLength);
            string key = string.Empty;
            Regex r = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n");
            Match m = r.Match(handShakeText);
            if (m.Groups.Count != 0)
            {
                key = Regex.Replace(m.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }
            byte[] encryptionString = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"));
            return Convert.ToBase64String(encryptionString);
        }

        /// <summary>
        /// 解析客户端数据包
        /// </summary>
        /// <param name="recBytes">服务器接收的数据包</param>
        /// <param name="recByteLength">有效数据长度</param>
        /// <returns></returns>
        private static string AnalyticData(byte[] recBytes, int recByteLength)
        {
            if (recByteLength < 2) { return string.Empty; }

            bool fin = (recBytes[0] & 0x80) == 0x80; // 1bit，1表示最后一帧  
            if (!fin)
            {
                return string.Empty;// 超过一帧暂不处理 
            }

            bool mask_flag = (recBytes[1] & 0x80) == 0x80; // 是否包含掩码  
            if (!mask_flag)
            {
                return string.Empty;// 不包含掩码的暂不处理
            }

            int payload_len = recBytes[1] & 0x7F; // 数据长度  

            byte[] masks = new byte[4];
            byte[] payload_data;

            if (payload_len == 126)
            {
                Array.Copy(recBytes, 4, masks, 0, 4);
                payload_len = (UInt16)(recBytes[2] << 8 | recBytes[3]);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 8, payload_data, 0, payload_len);

            }
            else if (payload_len == 127)
            {
                Array.Copy(recBytes, 10, masks, 0, 4);
                byte[] uInt64Bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    uInt64Bytes[i] = recBytes[9 - i];
                }
                UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                payload_data = new byte[len];
                for (UInt64 i = 0; i < len; i++)
                {
                    payload_data[i] = recBytes[i + 14];
                }
            }
            else
            {
                Array.Copy(recBytes, 2, masks, 0, 4);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 6, payload_data, 0, payload_len);

            }

            for (var i = 0; i < payload_len; i++)
            {
                payload_data[i] = (byte)(payload_data[i] ^ masks[i % 4]);
            }

            return Encoding.UTF8.GetString(payload_data);
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
                //String ip = "127.0.10.1";
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
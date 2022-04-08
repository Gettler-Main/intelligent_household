#  Intelligent_household

由C#编写基于Socket的大聪明家具模拟系统



## Scoket方法

#### 相关类

- lIPAddress类：包含了一个IP地址
- lIPEndPoint类：包含了一对IP地址和端口号

#### 方法

- lSocket (): 创建一个Socket
- lBind(): 绑定一个本地的IP和端口号(IPEndPoint)
- lListen(): 让Socket侦听传入的连接尝试，并指定侦听队列容量
- lConnect(): 初始化与另一个Socket的连接
- lAccept(): 接收连接并返回一个新的socket
- lSend(): 输出数据到Socket
- lReceive(): 从Socket中读取数据
- lClose(): 关闭Socket (销毁连接)



## 单元级项目

### 发送和接收 TCP 数据包

1. TCP 数据包结构设计 
2. TCP 数据包发送和接收过程

### TCP客户端构建流程

1.创建socket
2.链接服务器
3.接收数据
4.关闭套接字

![image-20220408112131307](README.assets/image-20220408112131307.png)



## 系统级项目

大聪明家居

客户端——发送操作指令

服务端——解析操作指令，向设备端发送命令

设备端——接受并执行命令

### 功能模块

1. 开关灯
2. 拉窗帘
3. 电饭煲
4. 空调
5. 电视
6. 电源插座
7. 热水器

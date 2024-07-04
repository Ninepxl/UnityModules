# Unity Python UDP通信框架

该框架实现了一个精简的upd客户端和服务器。主要功能是使用UDPClient封装函数实现同一台电脑上两个应用之间传输数据

## 代码介绍
框架代码保存在 Asset/Script/FrameWork
- UdpConnection 实现了数据同步发送函数Send和异步接收数据函数Listen
- ConnectionClient 为客户端代码
- ConnectionManager 服务器代码可进行扩展实现消息的转发

## Python 代码
main.py python实现了一个udp client客户端可以接收和发送数据

## 本地运行
将ConnectionManager和ConnectionClient挂载到一个空物体上，设定服务器接收消息的端口，和客户端发送消息的端口
![](https://gitee.com/Nine_Npxl/markdown_img/raw/master/20240704143605.png)
运行Python代码发送数据，Unity显示
![](https://gitee.com/Nine_Npxl/markdown_img/raw/master/20240704143930.png)
![](https://gitee.com/Nine_Npxl/markdown_img/raw/master/20240704143857.png)
Unity发送数据Python显示
![](https://gitee.com/Nine_Npxl/markdown_img/raw/master/20240704144021.png)
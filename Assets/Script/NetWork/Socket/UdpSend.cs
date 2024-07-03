using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class UdpSend
{
    string ip;
    int port;

    public UdpSend(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
    }

    /// <summary>
    /// 异步发送方法
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public async Task SendMsgAsync(string msg)
    {
        // 创建UDP客户端
        using (UdpClient udpClient = new UdpClient())
        {
            try
            {
                // 目标IP地址和端口
                udpClient.Connect(ip, port);

                // 将要发送的消息
                byte[] sendBytes = Encoding.UTF8.GetBytes(msg);

                // 发送消息
                await udpClient.SendAsync(sendBytes, sendBytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

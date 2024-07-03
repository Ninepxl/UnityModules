using System;
using System.Net;
using System.Net.Sockets;

public class UdpConnection
{
    public UdpClient client { get; private set; }
    public int port { get; private set; } // 自身端口号

    public UdpConnection(int port)
    {
        this.port = port;
        client = new UdpClient(port);
    }

    /// <summary>
    /// 发送数据到远程主机
    /// 因为UDP不保证数据一定发送到目标主机，所以不存在阻塞问题
    /// </summary>
    /// <param name="remoteAddress">远程主机的IP和端口</param>
    /// <param name="message">发送的数据</param>
    public void Send(IPEndPoint remoteAddress, byte[] message)
    {
        client.Send(message, message.Length, remoteAddress);
    }

    /// <summary>
    /// 监听收到的消息
    /// </summary>
    /// <param name="action">收到消息触发的事件</param>
    /// <param name="timeout"></param>
    public void Listen(Action<IPEndPoint, byte[]> action, int timeout = 0)
    {
        IAsyncResult result = client.BeginReceive(ReceiveBack, action);
        if (timeout > 0)
        {
            result.AsyncWaitHandle.WaitOne(timeout);
        }
    }

    /// <summary>
    /// 异步接收到消息的回调函数
    /// </summary>
    /// <param name="ar">异步接收数据的状态</param>
    private void ReceiveBack(IAsyncResult ar)
    {
        Action<IPEndPoint, byte[]> action = (Action<IPEndPoint, byte[]>)ar.AsyncState;
        if (ar.IsCompleted)
        {
            IPEndPoint remoteIp = null;
            var bytes = client.EndReceive(ar, ref remoteIp);
            action?.Invoke(remoteIp, bytes);
        }
        else
        {
            action?.Invoke(null, null);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private UdpConnection _connect;
    public int serverPort = 54123; // 服务器的端口
    private Dictionary<String, ConnectedClient> _clients = new Dictionary<string, ConnectedClient>();
    private static Queue<String> messageQueue = new Queue<string>();
    void Awake()
    {
        _connect = new UdpConnection(serverPort);
        _connect.Listen(CommandReceived, 5000);
    }

    /// <summary>
    /// 这个函数在接收到消息时触发
    /// </summary>
    /// <param name="remoteProt">远程客户端的Ip</param>
    /// <param name="data">远程客户端发送的数据</param>
    /// <exception cref="NotImplementedException"></exception>
    private void CommandReceived(IPEndPoint remoteProt, byte[] data)
    {
        string mes = Encoding.ASCII.GetString(data); // 解析中文换成UTF-8
        Debug.Log(mes);
        // TODO: 因为发送的数据不能直接在主线程中复制，现在暂时使用Queue来存储消息队列
        messageQueue.Enqueue(remoteProt.ToString() + ": " + mes);
        _connect.Listen(CommandReceived, 5000);
    }
    public static string GetMessage()
    {
        if (messageQueue.Count > 0)
        {
            return messageQueue.Dequeue();
        }
        else
        {
            return null;
        }
    }
}

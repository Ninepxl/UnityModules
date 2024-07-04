using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class ConnectionClient : MonoBehaviour
{
    private UdpConnection _connect;
    public int localPort = 54124;
    public int sendPort = 54123; // 要发送的应用的端口
    private string serverIp = "127.0.0.1";

    private void Awake()
    {
        _connect = new UdpConnection(localPort);
    }

    public void SendString(string message)
    {
        var bytes = Encoding.ASCII.GetBytes(message);
        _connect.Send(new IPEndPoint(IPAddress.Parse(serverIp), sendPort), bytes);
    }
}

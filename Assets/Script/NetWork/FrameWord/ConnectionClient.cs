using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class ConnectionClient : MonoBehaviour
{
    private UdpConnection _connect;
    public int loctProt = 54124;
    public int serverPort = 54123; // 服务器的端口
    private string serverIp = "127,0.0.1";
    private void Awake()
    {
        _connect = new UdpConnection(loctProt);
    }
    public void SendString(string message)
    {
        var bytes = Encoding.ASCII.GetBytes(message);
        _connect.Send(new IPEndPoint(IPAddress.Parse(serverIp), serverPort), bytes);
    }
}

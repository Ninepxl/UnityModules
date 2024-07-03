using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class UdpReceive
{
    UdpClient udpClient;
    int listenPort = 8803;
    Queue<(string Message, IPEndPoint Sender)> messageQueue = new Queue<(string, IPEndPoint)>();

    public UdpReceive(int listenPort = 8803)
    {
        this.listenPort = listenPort;
        udpClient = new UdpClient(listenPort);
        udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    void ReceiveCallback(IAsyncResult ar)
    {
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, listenPort);
        byte[] received = udpClient.EndReceive(ar, ref remoteEP);
        string message = Encoding.UTF8.GetString(received);
        // System.Console.WriteLine(message);
        lock (messageQueue)
        {
            // System.Console.WriteLine(message + remoteEP);
            messageQueue.Enqueue((message, remoteEP));
        }

        udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    public (string Message, IPEndPoint Sender) GetNextMessage()
    {
        lock (messageQueue)
        {
            if (messageQueue.Count > 0)
            {
                return messageQueue.Dequeue();
            }
            return (null, null);
        }
    }

    public bool HasMessages()
    {
        lock (messageQueue)
        {
            return messageQueue.Count > 0;
        }
    }

    public void Close()
    {
        udpClient.Close();
    }
}
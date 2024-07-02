using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class UdpReceive
{
    UdpClient udpClient;
    int listenPort = 8888;
    Queue<string> messageQueue = new Queue<string>();

    public UdpReceive(int listenPort = 8888)
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

        lock (messageQueue)
        {
            messageQueue.Enqueue(message);
        }

        udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    public string GetNextMessage()
    {
        lock (messageQueue)
        {
            if (messageQueue.Count > 0)
            {
                return messageQueue.Dequeue();
            }
            return null;
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



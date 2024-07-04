using System;
using System.Net;
public class ConnectedClient
{
    public IPEndPoint Remote;
    public DateTime LastMessage;
    public string Username;

    public ConnectedClient(IPEndPoint remote)
    {
        this.Remote = remote;
        LastMessage = DateTime.Now;
        Username = remote.ToString();
    }
}

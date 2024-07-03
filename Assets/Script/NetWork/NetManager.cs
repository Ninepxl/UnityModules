using Unity.VisualScripting;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    public UdpReceive udpReceive;
    public UdpSend udpSend;

    private void Awake()
    {
        udpReceive = new UdpReceive();
        udpSend = new UdpSend("127.0.0.1",8801);
    }
    void OnDestroy()
    {
        if (udpReceive != null)
        {
            udpReceive.Close();
        }
    }
}
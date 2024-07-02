using UnityEngine;

public class NetManager : MonoBehaviour
{
    public UdpReceive udpReceive;

    void Start()
    {
        udpReceive = new UdpReceive();
    }

    void OnDestroy()
    {
        if (udpReceive != null)
        {
            udpReceive.Close();
        }
    }
}
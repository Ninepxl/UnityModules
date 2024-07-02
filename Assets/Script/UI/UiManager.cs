using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text text;
    public NetManager netManager;

    private IEnumerator ReceiveMessages()
    {
        while (true)
        {
            if (netManager.udpReceive.HasMessages())
            {
                string message = netManager.udpReceive.GetNextMessage();
                if (message != null)
                {
                    // 在主线程中更新 UI 文本
                    text.text = message;
                }
            }
            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(ReceiveMessages());
    }
}
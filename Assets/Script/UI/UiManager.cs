using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text showTex;
    public NetManager netManager;
    public InputField mesInput;
    public Button senButton;
    private async void SendMes()
    {
        string mes = mesInput.text;
        Debug.Log(mes);
        await netManager.udpSend.SendMsgAsync(mes);
    }

    private IEnumerator ReceiveMessages()
    {
        while (true)
        {
            if (netManager.udpReceive.HasMessages())
            {
                var (message, sender) = netManager.udpReceive.GetNextMessage();
                Debug.Log(message);
                if (message != null)
                {
                    showTex.text += sender + " : " +  message + "\n";
                }
            }
            yield return null;
        }
    }

    private void Start()
    {
        senButton.onClick.AddListener(SendMes);
        StartCoroutine(ReceiveMessages());
    }
}
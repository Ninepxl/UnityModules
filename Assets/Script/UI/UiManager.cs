using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text showTex;
    public InputField mesInput;
    public Button senButton;
    public ConnectionManager connectionManager;
    public ConnectionClient connectionClient;
    private float checkMessageInterval = 0.2f; // 每1秒检查一次消息
    private float nextCheckTime = 0f;

    private void Start()
    {
        senButton.onClick.AddListener(sendMessage);
    }

    private void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkMessageInterval;
            StartCoroutine(GetMes());
        }
    }

    IEnumerator GetMes()
    {
        string mes = ConnectionManager.GetMessage();
        if (!string.IsNullOrEmpty(mes))
        {
            Debug.Log("mes is " + mes);
            showTex.text += mes + "\n";
        }
        yield return null;
    }

    private void sendMessage()
    {
        connectionClient.SendString(mesInput.text);
    }
}

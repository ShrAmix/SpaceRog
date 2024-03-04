using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkBaseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStatus;
    [SerializeField] private TMP_InputField ipInput;
    [SerializeField] private TMP_InputField portInput;

    private void Start()
    {
        // Заповнити IP-адресу за замовчуванням локальним IPv4
        ipInput.text = GetLocalIPv4();
    }

    public void StartGame()
    {
        var utpTransport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        if (utpTransport != null)
        {
            utpTransport.SetConnectionData(Sanitize(ipInput.text), GetPort()); // Встановлюємо адресу та порт для сервера
        }

        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("StartHost");
            textStatus.text=ipInput.text+":"+portInput.text;
        }
        else
        {
            Debug.LogError("ErrorHOST");
        }
    }

    public void JoinGame()
    {
        var utpTransport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        if (utpTransport != null)
        {
            utpTransport.SetConnectionData(Sanitize(ipInput.text), GetPort()); // Встановлюємо адресу та порт для клієнта
        }

        if (!NetworkManager.Singleton.StartClient())
        {
            Debug.LogError("Error CLIENT");
        }
    }

    private ushort GetPort()
    {
        // Використати значення за замовчуванням, якщо введено некоректне значення або порожній рядок
        ushort port = 7777;
        ushort.TryParse(portInput.text, out port);
        return port;
    }

    public string GetLocalIPv4()
    {
        return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }

    static string Sanitize(string text)
    {
        return System.Text.RegularExpressions.Regex.Replace(text, "[^A-Za-z0-9.]", "");
    }
}

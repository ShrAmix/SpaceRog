using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkBaseUI : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _joinButton;

    private void Awake()
    {
        _hostButton.onClick.AddListener(OnStartHost);
        _joinButton.onClick.AddListener(OnStartJoin);

        // Додаємо слухачів подій Netcode
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected; // Додайте цей рядок
    }



    private void OnDestroy()
    {
        // Видаляємо слухачів подій при знищенні об'єкта
        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected; // Додайте цей рядок
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log($"Client Disconnected: {clientId}");
    }

    private void OnStartHost()
    {
        Debug.Log("Starting host...");
        NetworkManager.Singleton.StartHost();
    }

    private void OnStartJoin()
    {
        Debug.Log("Starting client...");
        NetworkManager.Singleton.StartClient();
    }


    private void OnServerStarted()
    {
        Debug.Log("Server Started");
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client Connected: {clientId}");

        // Тут ви можете виконати додаткову логіку для взаємодії з підключеним клієнтом
        if (NetworkManager.Singleton.IsServer && NetworkManager.Singleton.ConnectedClientsList.Count == 2)
        {
            Debug.Log("Both players connected!");
            // Ось де ви можете запустити гру або викликати подію, що обидва гравці підключені
        }
    }
}

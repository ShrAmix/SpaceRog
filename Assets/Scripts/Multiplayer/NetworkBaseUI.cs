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
    }

    private void OnStartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    private void OnStartJoin()
    {
        NetworkManager.Singleton.StartClient();
    }
}

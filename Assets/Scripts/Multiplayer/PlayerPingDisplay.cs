using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class PlayerPingDisplay : NetworkBehaviour
{
    public TextMeshProUGUI pingText;

    private void Update()
    {
        pingText.text=(NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetCurrentRtt(NetworkManager.Singleton.NetworkConfig.NetworkTransport.ServerClientId)).ToString();
    }

   
}

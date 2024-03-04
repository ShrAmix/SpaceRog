using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerSelect : MonoBehaviour
{
    public GameObject[] playerPrefabs; // �������� ������� ������� ��� (Player1, Player2, Player3, Player4)

    public Joystick JoystickPlayer;
    public Joystick JoystickGun;

    private void Start()
    {
        int index = PlayerPrefs.GetInt("SelectPlayer");

        if (index < 0 || index >= playerPrefabs.Length)
        {
            // ���� ������ �������� �� ��� ������, ���������� ���� �� 0, ��� �� ������������� ������� ������ �������
            index = 0;
        }

        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            NetworkManager networkManager = FindObjectOfType<NetworkManager>();

            if (networkManager != null )
            {
                // ��� ����� ��� ���� ��������� � NetworkManager
                networkManager.NetworkConfig.PlayerPrefab = playerPrefabs[index];
            }
            
        }
        else
        {
            GameObject selectedPlayerPrefab = playerPrefabs[index];

            // ��������� ��'��� ��������� ������ �� ����
            GameObject playerInstance = Instantiate(selectedPlayerPrefab, transform.position, Quaternion.identity);
        }

    }
}

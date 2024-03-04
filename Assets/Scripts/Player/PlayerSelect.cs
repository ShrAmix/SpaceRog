using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerSelect : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Зберігаємо префаби гравців тут (Player1, Player2, Player3, Player4)

    public Joystick JoystickPlayer;
    public Joystick JoystickGun;

    private void Start()
    {
        int index = PlayerPrefs.GetInt("SelectPlayer");

        if (index < 0 || index >= playerPrefabs.Length)
        {
            // Якщо індекс виходить за межі масиву, встановимо його на 0, щоб за замовчуванням вибрати перший гравець
            index = 0;
        }

        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            NetworkManager networkManager = FindObjectOfType<NetworkManager>();

            if (networkManager != null )
            {
                // Ваш метод для зміни параметра в NetworkManager
                networkManager.NetworkConfig.PlayerPrefab = playerPrefabs[index];
            }
            
        }
        else
        {
            GameObject selectedPlayerPrefab = playerPrefabs[index];

            // Створюємо об'єкт вибраного гравця на сцені
            GameObject playerInstance = Instantiate(selectedPlayerPrefab, transform.position, Quaternion.identity);
        }

    }
}

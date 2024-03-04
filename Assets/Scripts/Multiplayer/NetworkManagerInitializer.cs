using UnityEngine;
using Unity.Netcode;

public class NetworkManagerInitializer : MonoBehaviour
{
    void Start()
    {
        // Отримання всіх об'єктів Network Manager в сцені
        NetworkManager[] existingManagers = FindObjectsOfType<NetworkManager>();

        // Перевірка кількості знайдених об'єктів
        if (existingManagers.Length > 1)
        {
            // Видалення останнього знайденого об'єкта (якщо їх більше 2)
            Destroy(existingManagers[existingManagers.Length - 1].gameObject);
        }
    }
}

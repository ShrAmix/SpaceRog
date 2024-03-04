using UnityEngine;
using Unity.Netcode;

public class NetworkManagerInitializer : MonoBehaviour
{
    void Start()
    {
        // ��������� ��� ��'���� Network Manager � ����
        NetworkManager[] existingManagers = FindObjectsOfType<NetworkManager>();

        // �������� ������� ��������� ��'����
        if (existingManagers.Length > 1)
        {
            // ��������� ���������� ���������� ��'���� (���� �� ����� 2)
            Destroy(existingManagers[existingManagers.Length - 1].gameObject);
        }
    }
}

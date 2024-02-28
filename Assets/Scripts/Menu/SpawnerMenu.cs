using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerMenu : MonoBehaviour
{
    [SerializeField] private float TimeToSpawn;
    [SerializeField] private GameObject[] PipePrefabs = new GameObject[3];
    [SerializeField] private Transform backgroundData; // ������� ������ � ���������

    private void Start()
    {
        // ������ ��������� ����� SpawnEnemy ���� TimeToSpawn ������
        InvokeRepeating("SpawnEnemy", 0, TimeToSpawn);
    }

    private void SpawnEnemy()
    {
        // ��������� ������� ������� ����� (���������� ���������� � 0)
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ��������, �� ������ ������� ����� �� ������� 1
        if (activeSceneIndex != 1 && activeSceneIndex != 8)
        {
            Instantiate(PipePrefabs[Random.Range(0, PipePrefabs.Length)], backgroundData);
        }
    }
}

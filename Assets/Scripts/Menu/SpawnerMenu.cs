using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerMenu : MonoBehaviour
{
    [SerializeField] private float TimeToSpawn;
    [SerializeField] private GameObject[] PipePrefabs = new GameObject[3];
    [SerializeField] private Transform backgroundData; // Залиште пустим у інспекторі

    private void Start()
    {
        // Почати викликати метод SpawnEnemy кожні TimeToSpawn секунд
        InvokeRepeating("SpawnEnemy", 0, TimeToSpawn);
    }

    private void SpawnEnemy()
    {
        // Отримання індексу активної сцени (індексація починається з 0)
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Перевірка, чи індекс активної сцени не дорівнює 1
        if (activeSceneIndex != 1 && activeSceneIndex != 8)
        {
            Instantiate(PipePrefabs[Random.Range(0, PipePrefabs.Length)], backgroundData);
        }
    }
}

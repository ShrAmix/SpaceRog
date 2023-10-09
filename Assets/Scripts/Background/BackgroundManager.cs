using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public float moveDistanceX = 24f; // Відстань руху по X
    public float moveSpeed = 1f; // Швидкість руху

    private Transform background1;
    private Transform background2;

    private float startX1; // Початкова позиція першого фону
    private float startX2; // Початкова позиція другого фону

    private void Start()
    {
        background1 = transform.Find("Background1");
        background2 = transform.Find("Background2");

        // Перевіряємо, чи є збережені дані для позицій фонів
        if (PlayerPrefs.HasKey("startX1") && PlayerPrefs.HasKey("startX2"))
        {
            startX1 = PlayerPrefs.GetFloat("startX1");
            startX2 = PlayerPrefs.GetFloat("startX2");
        }
        else
        {
            startX1 = background1.position.x + 3f;
            startX2 = background2.position.x + 3f;
            PlayerPrefs.SetFloat("startX1", startX1); // Зберігаємо початкову позицію першого фону в PlayerPrefs
            PlayerPrefs.SetFloat("startX2", startX2); // Зберігаємо початкову позицію другого фону в PlayerPrefs
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Підписуємо метод OnSceneLoaded на подію SceneManager.sceneLoaded
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Перезапускаємо фони на збережені координати, тільки якщо вони знаходяться в поточній сцені
        if (scene.buildIndex != 0)
        {
            background1.position = new Vector3(startX1, background1.position.y, background1.position.z);
            background2.position = new Vector3(startX2, background2.position.y, background2.position.z);
        }
    }

    private void FixedUpdate()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        // Рухаємо фони
        Vector3 targetPosition1 = new Vector3(startX1 - moveDistanceX, background1.position.y, background1.position.z);
        Vector3 targetPosition2 = new Vector3(startX2 - moveDistanceX, background2.position.y, background2.position.z);
        float step = moveSpeed * Time.fixedDeltaTime;
        background1.position = Vector3.MoveTowards(background1.position, targetPosition1, step);
        background2.position = Vector3.MoveTowards(background2.position, targetPosition2, step);

        // Перевіряємо, чи досягнута кінцева позиція першого фону, і перезапускаємо його
        if (background1.position.x <= startX1 - moveDistanceX)
        {
            background1.position = new Vector3(startX1, background1.position.y, background1.position.z);
        }

        // Перевіряємо, чи досягнута кінцева позиція другого фону, і перезапускаємо його
        if (background2.position.x <= startX2 - moveDistanceX)
        {
            background2.position = new Vector3(startX2, background2.position.y, background2.position.z);
        }
    }
}

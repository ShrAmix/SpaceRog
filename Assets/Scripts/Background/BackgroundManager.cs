using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public float moveDistanceX = 24f; // ³������ ���� �� X
    public float moveSpeed = 1f; // �������� ����

    private Transform background1;
    private Transform background2;

    private float startX1; // ��������� ������� ������� ����
    private float startX2; // ��������� ������� ������� ����

    private void Start()
    {
        background1 = transform.Find("Background1");
        background2 = transform.Find("Background2");

        // ����������, �� � �������� ��� ��� ������� ����
        if (PlayerPrefs.HasKey("startX1") && PlayerPrefs.HasKey("startX2"))
        {
            startX1 = PlayerPrefs.GetFloat("startX1");
            startX2 = PlayerPrefs.GetFloat("startX2");
        }
        else
        {
            startX1 = background1.position.x + 3f;
            startX2 = background2.position.x + 3f;
            PlayerPrefs.SetFloat("startX1", startX1); // �������� ��������� ������� ������� ���� � PlayerPrefs
            PlayerPrefs.SetFloat("startX2", startX2); // �������� ��������� ������� ������� ���� � PlayerPrefs
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // ϳ������� ����� OnSceneLoaded �� ���� SceneManager.sceneLoaded
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ������������� ���� �� �������� ����������, ����� ���� ���� ����������� � ������� ����
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
        // ������ ����
        Vector3 targetPosition1 = new Vector3(startX1 - moveDistanceX, background1.position.y, background1.position.z);
        Vector3 targetPosition2 = new Vector3(startX2 - moveDistanceX, background2.position.y, background2.position.z);
        float step = moveSpeed * Time.fixedDeltaTime;
        background1.position = Vector3.MoveTowards(background1.position, targetPosition1, step);
        background2.position = Vector3.MoveTowards(background2.position, targetPosition2, step);

        // ����������, �� ��������� ������ ������� ������� ����, � ������������� ����
        if (background1.position.x <= startX1 - moveDistanceX)
        {
            background1.position = new Vector3(startX1, background1.position.y, background1.position.z);
        }

        // ����������, �� ��������� ������ ������� ������� ����, � ������������� ����
        if (background2.position.x <= startX2 - moveDistanceX)
        {
            background2.position = new Vector3(startX2, background2.position.y, background2.position.z);
        }
    }
}

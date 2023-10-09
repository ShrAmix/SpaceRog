using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private RectTransform[] objects;  // ����� ��'����
    [SerializeField] private float[] y;  // ����� ��������� Y
    [SerializeField] private bool[] isMovingUp;  // ����� ����, ����� �������� ���� �� �� Y (true - ����� �����, false - ������ ����)
    [SerializeField] private float speed = 1f;  // �������� ���� ��'����

    [SerializeField] private RectTransform[] xObjects;  // ����� ��'����, �� ��������� �� �� X
    [SerializeField] private float[] x;  // ����� ��������� X ��� ���� �� �� X
    [SerializeField] private bool isMovingRight;  // �����, ����� �������� ���� �� �� X (true - ���� �������, false - ������ �����)
    [SerializeField] private float xSpeed = 1f;  // �������� ���� �� �� X
    [SerializeField] private bool[] xGo;

    [SerializeField] private int sceneNowWhy;

    private void Start()
    {
        SpawnObjects();
    }

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(sceneNowWhy)) { 
        MoveObjects();
        MoveXObjects();}
        else GetComponent<ObjectMovement>().enabled = false;
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 spawnPosition = new Vector2(objects[i].anchoredPosition.x, isMovingUp[i] ? -1000f : 1000f);
            objects[i].anchoredPosition = spawnPosition;
        }
        for (int i = 0; i < xObjects.Length; i++)
        {
            Vector2 spawnPosition = new Vector2(xObjects[i].anchoredPosition.x + 1300f + (200 * (i + 1)), xObjects[i].anchoredPosition.y);
            xObjects[i].anchoredPosition = spawnPosition;
            xGo[i] = true;
        }

    }

    private void MoveObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 targetPosition = new Vector2(objects[i].anchoredPosition.x, y[i]);
            float step = speed * Time.deltaTime;

            if (Mathf.Abs(objects[i].anchoredPosition.y - targetPosition.y) <= 100f)
            {
                // ��������� ���� �� ������� 100 �������
                step *= Mathf.Abs(objects[i].anchoredPosition.y - targetPosition.y) / 100f;
            }

            objects[i].anchoredPosition = Vector2.MoveTowards(objects[i].anchoredPosition, targetPosition, step);

            if (objects[i].anchoredPosition == targetPosition)
            {
                // ��'��� �������� �������, ��������� ���� ���
                isMovingUp[i] = false;
            }
        }
    }

    private void MoveXObjects()
    {
        for (int i = 0; i < xObjects.Length; i++)
        {
            Vector2 targetPosition = new Vector2(x[i], xObjects[i].anchoredPosition.y);
            float step = xSpeed * Time.deltaTime;
            if (Mathf.Abs(xObjects[i].anchoredPosition.x - targetPosition.x) <= 100f && xGo[i])
            {
                // ��������� ���� �� ������� 100 �������
                step *= Mathf.Abs(xObjects[i].anchoredPosition.x - targetPosition.x) / 100f;
            }
            if (xObjects[i].anchoredPosition.x <= x[i] + 1f)
            {
                xGo[i] = false;
            }



            if (isMovingRight)
            {
                if (xObjects[i].anchoredPosition.x >= targetPosition.x)
                {
                    // ��'��� �������� �������, ������� �������� ����
                    isMovingRight = false;
                }
            }
            if (xGo[i])
            {
                xObjects[i].anchoredPosition = Vector2.MoveTowards(xObjects[i].anchoredPosition, targetPosition, step);
            }

        }
    }
}


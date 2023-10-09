using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectMover : MonoBehaviour
{
    public RectTransform[] moveLeftObjects; // ��'����, �� ������ �������� ����
    public RectTransform[] moveRightObjects; // ��'����, �� ������ �������� ������
    public float moveSpeed = 5f; // �������� ����
    [SerializeField] private GameObject ObjectMove1;

    private bool isMovingLeft;
    private bool isMovingRight;



    private void Start()
    {
        isMovingLeft = true;
        isMovingRight = true;
    }

    private void FixedUpdate()
    {
        if (isMovingLeft)
        {
            MoveLeftObjects();
        }
        if (isMovingRight)
        {
            MoveRightObjects();
        }



        if (moveLeftObjects[0].anchoredPosition.x <= -2000f)
        {
            // ��������� ���, ���� ����� ����� ��'���� ������� ���� ���������
            isMovingLeft = false;
            isMovingRight = false;

            // ���������� ���� ����� ���
            SceneManager.LoadScene(1); // ����� '1' �� ������ �����, ��� �� ������ �����������
        }
    }


    private void MoveLeftObjects()
    {
        for (int i = 0; i < moveLeftObjects.Length; i++)
        {
            Vector2 targetPosition = new Vector2(moveLeftObjects[i].anchoredPosition.x - 20f, moveLeftObjects[i].anchoredPosition.y);
            float step = moveSpeed * Time.fixedDeltaTime;
            moveLeftObjects[i].anchoredPosition = Vector2.MoveTowards(moveLeftObjects[i].anchoredPosition, targetPosition, step);
        }
    }

    private void MoveRightObjects()
    {
        for (int i = 0; i < moveRightObjects.Length; i++)
        {
            Vector2 targetPosition = new Vector2(moveRightObjects[i].anchoredPosition.x + 20f, moveRightObjects[i].anchoredPosition.y);
            float step = moveSpeed * Time.fixedDeltaTime;
            moveRightObjects[i].anchoredPosition = Vector2.MoveTowards(moveRightObjects[i].anchoredPosition, targetPosition, step);
        }
    }
    public void BoolGo()
    {
        // �������� ������� ObjectMover
        ObjectMove1.GetComponent<ObjectMovement>().enabled = false;
        this.enabled = true;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectMover : MonoBehaviour
{
    public RectTransform[] moveLeftObjects; // Об'єкти, які будуть рухатися вліво
    public RectTransform[] moveRightObjects; // Об'єкти, які будуть рухатися вправо
    public float moveSpeed = 5f; // Швидкість руху
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
            // Зупиняємо рух, якщо обидві групи об'єктів досягли своїх координат
            isMovingLeft = false;
            isMovingRight = false;

            // Завантажте нову сцену тут
            SceneManager.LoadScene(1); // Змініть '1' на індекс сцени, яку ви хочете завантажити
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
        // Вмикання скрипту ObjectMover
        ObjectMove1.GetComponent<ObjectMovement>().enabled = false;
        this.enabled = true;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSpawner : MonoBehaviour
{

    private Rigidbody2D rb;                     // Rigidbody2D компонент ворожого корабля
    private Vector2 targetPosition;             // Позиція, до якої корабель рухається
    private float timer = 1.75f, timerB = 0;                // Таймер для контролю стрільби

    private Transform player;
    [SerializeField] private Sprite[] imageVariations;
    [SerializeField] private float speedRotate = 5f;   // Швидкість обертання корабля
    private float screenWidth, screenHeight;
    private float visibleWidth, visibleHeight;
    private int goToBords = 0;

    [SerializeField] private float speedHorizontal;    // Швидкість руху корабля по горизонталі
    [SerializeField] private float speedVertical;      // Швидкість руху корабля по вертикалі

    [SerializeField] private GameObject enemy;         // Сам корабель (потрібний для вимкнення коллайдера при старті)
    [SerializeField] private Transform enemyR;         // Transform компонент корабля, що відповідає за його обертання


    [SerializeField] private int RotatePosition;

    private void Start()
    {
        speedHorizontal = Random.Range(6, 12);
        speedVertical = Random.Range(6, 9);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();                     // Отримуємо посилання на Rigidbody2D
        SetRandomTargetPosition();

        // Вибрати випадкову картинку
        Sprite randomImage = imageVariations[Random.Range(0, imageVariations.Length)];

        // Присвоїти вибрану картинку об'єкту
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = randomImage;
        

        // Получить размеры экрана
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Получить размеры видимой области
        visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight / 2;
        visibleHeight = Camera.main.orthographicSize * 2.0f / 2;

        

        // Встановлюємо початкову позицію корабля залежно від цільової позиції
        int t = Random.Range(0, 100);

        if (t <= 50) 
        { 
            if (targetPosition.x > 0)
              enemy.transform.position = new Vector3(-12, enemy.transform.position.y, enemy.transform.position.z);
            else
              enemy.transform.position = new Vector3(12, enemy.transform.position.y, enemy.transform.position.z);

            if (targetPosition.y > 0)
               enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(-5, 0), enemy.transform.position.z);
            else
              enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(0, 5), enemy.transform.position.z);
        }
        else
        {
            if (targetPosition.x < 0)
                enemy.transform.position = new Vector3(Random.Range(-12, -1), enemy.transform.position.y, enemy.transform.position.z);
            else
                enemy.transform.position = new Vector3(Random.Range(1, 12), enemy.transform.position.y, enemy.transform.position.z);

            if (targetPosition.y < 0)
                enemy.transform.position = new Vector3(enemy.transform.position.x, -7, enemy.transform.position.z);
            else
                enemy.transform.position = new Vector3(enemy.transform.position.x, 7, enemy.transform.position.z);
        }
        SetRandomTargetPosition();
        // Вывести результаты
        float x1 = (visibleWidth * (-1)) + 1.5f, x2 = (visibleHeight * (-1)) + 1.5f;
        LookAtTargetPosition();  // Направляємо корабель на гравця при старті
    }

    private void FixedUpdate()
    {
        MoveToTargetPosition();        // Рухаємо корабель до цільової позиції
        if (enemy.transform.position.x < -20 || enemy.transform.position.x > 20 || enemy.transform.position.y < -10 || enemy.transform.position.y > 10)
            Destroy(enemy) ;
    }
    private void SetRandomTargetPosition()
    {
        float targetX = Random.Range(-7f, 7f);
        float targetY = Random.Range(-5f, 1f);
        targetPosition = new Vector2(targetX, targetY);

        
    }

    private void MoveToTargetPosition()
    {
        Vector2 currentPosition = transform.position;           // Поточна позиція корабля
        Vector2 forwardDirection = enemyR.up;                   // Напрямок, в який дивиться корабель
        Vector2 movement = forwardDirection * speedVertical;    // Вектор руху вперед

        rb.velocity = movement;                                 // Встановлюємо швидкість руху корабля
    }

    private void LookAtTargetPosition()
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;  // Вектор напрямку до цільової позиції
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Кут між вектором напрямку і віссю X

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle+ RotatePosition); // Обертання по Z-осі
        enemyR.rotation = rotation; // Встановлюємо поворот корабля до кута rotation
    }
}

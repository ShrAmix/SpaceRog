using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSpawner : MonoBehaviour
{

    private Rigidbody2D rb;                     // Rigidbody2D ��������� �������� �������
    private Vector2 targetPosition;             // �������, �� ��� �������� ��������
    private float timer = 1.75f, timerB = 0;                // ������ ��� �������� �������

    private Transform player;
    [SerializeField] private Sprite[] imageVariations;
    [SerializeField] private float speedRotate = 5f;   // �������� ��������� �������
    private float screenWidth, screenHeight;
    private float visibleWidth, visibleHeight;
    private int goToBords = 0;

    [SerializeField] private float speedHorizontal;    // �������� ���� ������� �� ����������
    [SerializeField] private float speedVertical;      // �������� ���� ������� �� ��������

    [SerializeField] private GameObject enemy;         // ��� �������� (�������� ��� ��������� ���������� ��� �����)
    [SerializeField] private Transform enemyR;         // Transform ��������� �������, �� ������� �� ���� ���������


    [SerializeField] private int RotatePosition;

    private void Start()
    {
        speedHorizontal = Random.Range(6, 12);
        speedVertical = Random.Range(6, 9);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();                     // �������� ��������� �� Rigidbody2D
        SetRandomTargetPosition();

        // ������� ��������� ��������
        Sprite randomImage = imageVariations[Random.Range(0, imageVariations.Length)];

        // �������� ������� �������� ��'����
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = randomImage;
        

        // �������� ������� ������
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // �������� ������� ������� �������
        visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight / 2;
        visibleHeight = Camera.main.orthographicSize * 2.0f / 2;

        

        // ������������ ��������� ������� ������� ������� �� ������� �������
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
        // ������� ����������
        float x1 = (visibleWidth * (-1)) + 1.5f, x2 = (visibleHeight * (-1)) + 1.5f;
        LookAtTargetPosition();  // ����������� �������� �� ������ ��� �����
    }

    private void FixedUpdate()
    {
        MoveToTargetPosition();        // ������ �������� �� ������� �������
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
        Vector2 currentPosition = transform.position;           // ������� ������� �������
        Vector2 forwardDirection = enemyR.up;                   // ��������, � ���� �������� ��������
        Vector2 movement = forwardDirection * speedVertical;    // ������ ���� ������

        rb.velocity = movement;                                 // ������������ �������� ���� �������
    }

    private void LookAtTargetPosition()
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;  // ������ �������� �� ������� �������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ��� �� �������� �������� � ���� X

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle+ RotatePosition); // ��������� �� Z-��
        enemyR.rotation = rotation; // ������������ ������� ������� �� ���� rotation
    }
}

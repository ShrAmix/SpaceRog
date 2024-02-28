using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyDestroyers : NetworkBehaviour
{
    [SerializeField] private int health = 3;  // ʳ������ �����
    [SerializeField] private GameObject explosionPrefab;  // ������ ������

    private Rigidbody2D rb;                     // Rigidbody2D ��������� �������� �������
    private Vector2 targetPosition;             // �������, �� ��� �������� ��������
    private float timer = 1.75f, timerB = 0;                // ������ ��� �������� �������

    private Transform player;                   // ��������� �� ��'��� ������
    [SerializeField] private float speedRotate = 5f;   // �������� ��������� �������
    private float screenWidth, screenHeight;
    private float visibleWidth, visibleHeight;
    private int goToBords = 0;

    [SerializeField] private float speedHorizontal;    // �������� ���� ������� �� ����������
    [SerializeField] private float speedVertical;      // �������� ���� ������� �� ��������

    [SerializeField] private GameObject enemy;         // ��� �������� (�������� ��� ��������� ���������� ��� �����)
    [SerializeField] private int scoreForDead = 3;     // ʳ������ ����, �� ������� ������ �� �������� ������
    [SerializeField] private int maxMoneyForDead = 2;     // ʳ������ ������, �� ������� ������ �� �������� ������
    [SerializeField] private Transform enemyR;         // Transform ��������� �������, �� ������� �� ���� ���������

    [SerializeField] private GameObject coinPrefab; // ������ �������
    [SerializeField] private Vector3 coinScale = new Vector3(1f, 1f, 1f); // ����� ������
    private bool leave = true;
    private Spawner spawner;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource gunshotAudioSource;
    private void Start()
    {
        float[] IndexD = new float[2];
        if (PlayerPrefs.GetInt("Difficult") == 0)
        {
            IndexD[0] = -2.25f;
            IndexD[1] = -2.25f;
        }
        else if (PlayerPrefs.GetInt("Difficult") == 2)
        {
            IndexD[0] = 2.25f;
            IndexD[1] = 2.25f;
        }
        else
        {
            IndexD[0] = 0;
            IndexD[1] = 0;
        }

        speedHorizontal += IndexD[0];
        speedVertical += IndexD[1];

        // �������� ��������� �� ��'��� ����� Spawner
        spawner = FindObjectOfType<Spawner>();
        rb = GetComponent<Rigidbody2D>();                     // �������� ��������� �� Rigidbody2D
        player = GameObject.FindGameObjectWithTag("Player").transform;   // �������� ��������� �� ������ �� ����� "Player"
        SetRandomTargetPosition();


        // ������������ ��������� ������� ������� ������� �� ������� �������
        if (targetPosition.x > 0)
            enemy.transform.position = new Vector3(-12, enemy.transform.position.y, enemy.transform.position.z);
        else
            enemy.transform.position = new Vector3(12, enemy.transform.position.y, enemy.transform.position.z);

        if (player.position.y > 0)
            enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(-5, 0), enemy.transform.position.z);
        else
            enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(0, 5), enemy.transform.position.z);
        AudioSource foundAudioSource = FindAudioSource("Sounds");
        if (foundAudioSource != null)
        {
            gunshotAudioSource = foundAudioSource;
        }

        // �������� ������� ������
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // �������� ������� ������� �������
        visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight / 2;
        visibleHeight = Camera.main.orthographicSize * 2.0f / 2;

        // ������� ����������
        float x1 = (visibleWidth * (-1)) + 1.5f, x2 = (visibleHeight * (-1)) + 1.5f;
        LookAtPlayer(1000);  // ����������� �������� �� ������ ��� �����
    }
    private AudioSource FindAudioSource(string objectName)
    {
        AudioController[] musicManagers = FindObjectsOfType<AudioController>();
        foreach (AudioController musicManager in musicManagers)
        {
            if (musicManager.gameObject.name == objectName)
            {
                return musicManager.audio_;
            }
        }
        return null;
    }

    private void FixedUpdate()
    {

        MoveToTargetPosition();        // ������ �������� �� ������� �������
        if (Mathf.Abs(enemyR.position.x) < visibleWidth && Mathf.Abs(enemyR.position.y) < visibleHeight)
        {
            if (leave)
                enemy.GetComponent<CapsuleCollider2D>().enabled = true;   // ��������� ����������, ���� �������� ����������� � ������� ������� �������
        }


        if (goToBords == 2)
        {
            LookAtPlayer(1000);                  // ����������� �������� �� ������
            goToBords = 1;
        }
        timerB -= Time.deltaTime;
    }
    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector2(Random.Range((visibleWidth * (-1)) + 1.5f, (visibleWidth * (1)) - 1.5f), Random.Range((visibleHeight * (-1)) + 1.5f, (visibleHeight * (1)) - 1.5f));   // ������������ ��������� ������� ������� � ����� ������� �������
    }
    private void MoveToTargetPosition()
    {
        Vector2 currentPosition = transform.position;           // ������� ������� �������
        Vector2 forwardDirection = enemyR.up;                   // ��������, � ���� �������� ��������
        Vector2 movement = forwardDirection * speedVertical;    // ������ ���� ������

        rb.velocity = movement;                                 // ������������ �������� ���� �������
    }

    private void LookAtPlayer(int speed)
    {
        Vector2 direction = player.position - transform.position;  // ������ �������� �� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;   // ��� �� �������� �������� � ���� X � ��������� ������

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);     // ��������� �� Z-��
        enemyR.rotation = Quaternion.RotateTowards(enemyR.rotation, rotation, speedRotate * speed * Time.deltaTime);   // ����������� ��������� �������
    }
    private void PlayDeathSound()
    {
        if (deathSound != null && gunshotAudioSource != null)
        {
            gunshotAudioSource.PlayOneShot(deathSound);
        }
    }
    private void SpawnCoins(int count)
    {
        float minX = -0.4f; // ̳������� �������� �� x
        float maxX = 0.4f;  // ����������� �������� �� x
        float minY = -0.1f; // ̳������� �������� �� y
        float maxY = 0.2f;  // ����������� �������� �� y
        if (count > 0)
        {
            GameObject[] coins = new GameObject[count]; // ����� ��� ���������� ��������� �����

            for (int i = 0; i < count; i++)
            {
                // �������� �������� ���������� ��� ������ � ������� ��������
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);

                // ��������� ������ � ����������� ������������
                GameObject coinInstance = Instantiate(coinPrefab, transform.position + new Vector3(randomX, randomY, 0f), Quaternion.identity);
                coinInstance.transform.localScale = coinScale; // ������ ����� ������
                coins[i] = coinInstance; // ������ ������ �� ������

                // �������� ������� ������ � ScoreManager
                ScoreManager.Instance.SetMoney(1);
                PlayerPrefs.SetInt("MoneyDay" + $"{PlayerPrefs.GetInt("Difficult")}", PlayerPrefs.GetInt("MoneyDay" + $"{PlayerPrefs.GetInt("Difficult")}") + 1);
                PlayerPrefs.SetInt("MaxMoney" + $"{PlayerPrefs.GetInt("Difficult")}", PlayerPrefs.GetInt("MaxMoney" + $"{PlayerPrefs.GetInt("Difficult")}") + 1);
            }

            // ��������� ����� ������
            foreach (GameObject coin in coins)
            {
                Destroy(coin, 0.4f); // ������� ����� ������ ����� 0.4 �������
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        health--;  // �������� ������� �����

        if (health <= 0)
        {
            if (other.gameObject.CompareTag("Bullet") && leave)
            {
                leave = false;
                //enemy.GetComponent<CapsuleCollider2D>().enabled = false;
                // �������� ��� �������� �������� �������
                float enemyRotation = Random.Range(0, 360);

                // ��������� ��������� ������� ������ � ���������� ���������
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0f, 0f, enemyRotation));
                PlayDeathSound();
                Destroy(explosion, 0.6f);  // ������� ������� ������ ����� 0.75 �������


                Destroy(gameObject);  // ������� �������� ��������� �������� �������
                ScoreManager.Instance.SetScore(scoreForDead);
                int t = Random.Range(0, (maxMoneyForDead + PlayerPrefs.GetInt("Difficult")));

                SpawnCoins(t);
                PlayerPrefs.SetInt("Enemys" + $"{PlayerPrefs.GetInt("Difficult")}", PlayerPrefs.GetInt("Enemys" + $"{PlayerPrefs.GetInt("Difficult")}") + 1);
                PlayerPrefs.SetInt("EnemysDay" + $"{PlayerPrefs.GetInt("Difficult")}", PlayerPrefs.GetInt("EnemysDay" + $"{PlayerPrefs.GetInt("Difficult")}") + 1);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Polia"))
        {
            goToBords++;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            goToBords = 2;
        }

    }
}

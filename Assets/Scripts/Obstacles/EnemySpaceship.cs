using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpaceship : NetworkBehaviour
{
    [SerializeField] private int health = 1;  // ʳ������ �����
    [SerializeField] private GameObject explosionPrefab;  // ������ ������


    private Rigidbody2D rb;                     // Rigidbody2D ��������� �������� �������
    private Vector2 targetPosition;             // �������, �� ��� �������� ��������
    private float timer = 1.75f;                // ������ ��� �������� �������
    private Transform player;                   // ��������� �� ��'��� ������
    [SerializeField] private float speedRotate = 5f;   // �������� ��������� �������
    private float screenWidth, screenHeight;
    private float visibleWidth, visibleHeight;

    [SerializeField] private float speedHorizontal;    // �������� ���� ������� �� ����������
    [SerializeField] private float speedVertical;      // �������� ���� ������� �� ��������
    [SerializeField] private GameObject bullet;        // ������ ���
    [SerializeField] private GameObject enemy;         // ��� �������� (�������� ��� ��������� ���������� ��� �����)
    [SerializeField] private int scoreForDead = 3;     // ʳ������ ����, �� ������� ������ �� �������� ������
    [SerializeField] private int maxMoneyForDead = 2;     // ʳ������ ������, �� ������� ������ �� �������� ������
    [SerializeField] private Transform enemyR;         // Transform ��������� �������, �� ������� �� ���� ���������

    [SerializeField] private GameObject coinPrefab; // ������ �������
    [SerializeField] private Vector3 coinScale = new Vector3(1f, 1f, 1f); // ����� ������

    private bool leave = true;


    private Spawner spawner;

    [SerializeField] private AudioSource gunshotAudioSource;
    [SerializeField] private AudioClip gunshotSound;
    [SerializeField] private AudioClip deathSound;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Difficult") == 0)
            timer = 2.25f;
        else if(PlayerPrefs.GetInt("Difficult") == 2)
            timer = 1.25f;
        // �������� ��������� �� ��'��� ����� Spawner
        spawner = FindObjectOfType<Spawner>();

        rb = GetComponent<Rigidbody2D>();                     // �������� ��������� �� Rigidbody2D
        player = GameObject.FindGameObjectWithTag("Player").transform;   // �������� ��������� �� ������ �� ����� "Player"
        SetRandomTargetPosition();                             // ������������ ��������� ������� �������



        // ������������ ��������� ������� ������� ������� �� ������� �������
        if (targetPosition.x < 0)
            enemy.transform.position = new Vector3(Random.Range(-12, -1), enemy.transform.position.y, enemy.transform.position.z);
        else
            enemy.transform.position = new Vector3(Random.Range(1, 12), enemy.transform.position.y, enemy.transform.position.z);

        if (player.position.y > 0)
            enemy.transform.position = new Vector3(enemy.transform.position.x, -7, enemy.transform.position.z);
        else
            enemy.transform.position = new Vector3(enemy.transform.position.x, 7, enemy.transform.position.z);

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
        // Debug.Log("������ ������� �������: " + x1);
        // Debug.Log("������ ������� �������: " + x2);
        LookAtPlayer(100);// ����������� �������� �� ������ ��� �����
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
        if (Mathf.Abs(enemyR.position.x - targetPosition.x) > 0.1f && Mathf.Abs(enemyR.position.y - targetPosition.y) > 0.1f)
        {
            MoveToTargetPosition();                             // ������ �������� �� ������� �������
        }
        if (Mathf.Abs(enemyR.position.x) < visibleWidth && Mathf.Abs(enemyR.position.y) < visibleHeight)
        {
            if (leave)
                enemy.GetComponent<CapsuleCollider2D>().enabled = true;   // ��������� ����������, ���� �������� ����������� � ������� ������� �������
        }

        LookAtPlayer(1);                                        // ����������� �������� �� ������
        Shoot();                                                // �������� �������
    }

    private void MoveToTargetPosition()
    {
        Vector2 currentPosition = transform.position;           // ������� ������� �������
        Vector2 direction = (targetPosition - currentPosition).normalized;   // �������� ���� �� ������� �������
        Vector2 movement = new Vector2(direction.x * speedHorizontal, direction.y * speedVertical);   // ������ ����
        rb.velocity = movement;                                 // ������������ �������� ���� �������
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector2(Random.Range((visibleWidth * (-1)) + 0.2f, (visibleWidth * (1)) - 0.2f), Random.Range((visibleHeight * (-1)) + 0.2f, (visibleHeight * (1)) - 0.2f));   // ������������ ��������� ������� ������� � ����� ������� �������
    }
    private void LookAtPlayer(int speed)
    {
        Vector2 direction = player.position - transform.position;  // ������ �������� �� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;   // ��� �� �������� �������� � ���� X
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);     // ������������ �������� ��� ���������� ����:
        enemyR.rotation = Quaternion.Slerp(enemyR.rotation, rotation, speedRotate * speed * Time.deltaTime);   // ������ ��������� �������
    }
    private void Shoot()
    {
        if (timer <= 0)
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, enemyR.rotation);
            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
            PlayGunshotSound();
            timer = 1.5f;
        }
        timer -= Time.deltaTime;
    }
    private void PlayGunshotSound()
    {
        if (gunshotSound != null && gunshotAudioSource != null)
        {
            gunshotAudioSource.PlayOneShot(gunshotSound);
        }
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
                int t = Random.Range(0, (maxMoneyForDead+PlayerPrefs.GetInt("Difficult")));
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
            enemy.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

}

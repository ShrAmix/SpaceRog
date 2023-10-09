using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBrush : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private string nameBudd;
    [SerializeField] private GameObject buffObj;

    [SerializeField] private bool Testov = false;
    [SerializeField] private float speedVertical = 5f;
    [SerializeField] private float maxAngle = 30f;
    [SerializeField] private Sprite[] spriteBuff;
    private float angle, moveDirection;

    [SerializeField] private double timeBrush=10;




    private static BuffBrush instance;
    public static BuffBrush Instance
    {
        get { return instance; }
    }


    public double BuffTimeValue { get; set; } = 1; // ���������� � ����� ������
    
    private void Awake()
    {
        Sprite randomImage = spriteBuff[PlayerPrefs.GetInt(nameBudd+ "BuffBuy")];
        SpriteRenderer spriteRenderer = buffObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = randomImage;
        if (PlayerPrefs.GetInt(nameBudd + "BuffBuy") == 1)
            timeBrush = 13;
        else if (PlayerPrefs.GetInt(nameBudd + "BuffBuy") == 2)
            timeBrush = 16;
        else if (PlayerPrefs.GetInt(nameBudd + "BuffBuy") == 3)
            timeBrush = 20;
        else
            timeBrush = 10;


        BuffTimeValue = timeBrush; // ����� ����� ����� �� BuffTimeValue
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }



    private void Start()
    {
        if (!Testov)
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            // ��������� ������� �� -10 �� 12 �� �� � �� 5 �� �� Y
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
            visibleWidth /= 2;
            transform.position = new Vector3(Random.Range(-visibleWidth+1, visibleWidth-1), 5f, 0f);

            // ��������� �������� ���� ��'���� � ��������� �� ������
            moveDirection = transform.position.x < 0f ? 1f : -1f;

            // ��������� ��� ���� ��'���� � ��������� �� �������� ������
            angle = transform.position.x < 0f ? Random.Range(-90f, -90f + maxAngle) : Random.Range(-maxAngle + 90f, 90f);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (!Testov)
        {
            // ������������ ��� � ������ ��������
            Vector2 velocity = Quaternion.Euler(0f, 0f, angle) * Vector2.right * moveDirection * speedVertical;
            rb.velocity = velocity;
            if (transform.position.y < -6)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

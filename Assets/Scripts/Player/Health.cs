using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject healthSpritePrefab;
    [SerializeField] private Transform HelTrans;
    private GameObject[] heartImages;


    [SerializeField] private bool chekHealth=false;

    public int currentHealth;

    private void Start()
    {
        instance = this;
        // Получить размеры экрана
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Получить размеры видимой области
        float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
        float visibleHeight = Camera.main.orthographicSize * 2.0f;

        /////////
        
        maxHealth =((int)PlayerPrefs.GetFloat("maxHealth"));
        /////////

        currentHealth = maxHealth;
        heartImages = new GameObject[maxHealth];
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heartObj = Instantiate(healthSpritePrefab, transform);
            SpriteRenderer spriteRenderer = heartObj.GetComponent<SpriteRenderer>();
            float heartSize = spriteRenderer.bounds.size.x;

            heartObj.transform.position = new Vector3(HelTrans.transform.position.x + (i * heartSize), HelTrans.transform.position.y, 0);

            heartImages[i] = heartObj;
        }

        UpdateLifeCounter();
    }
    public void AddHeart()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateLifeCounter();
        }
    }
    public void UpdateLifeCounter()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].SetActive(true);
            }
            else
            {
                heartImages[i].SetActive(false);
            }
        }
    }

}

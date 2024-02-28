using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerFly : NetworkBehaviour
{
    public static PlayerFly playerInstance;
    private float Timer = 2;



    [SerializeField] private float SpeedHorizontal;
    [SerializeField] private float SpeedVertical;
    public Joystick JoistickPlayer;
    public Joystick JoistickGun;
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform PlayerR;
    [SerializeField] private Transform Gun1;
    [SerializeField] private Transform Gun2;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float idleRotationSpeed = 4f;



    private Health health;
    private float timeShoot = 0.3f;



    [SerializeField] private double liveSteel = 1;

    [SerializeField] private GameObject Brush;

    private BuffBrush buffBrush;

    private bool BrushIs = false;
    private double buffBrushTime = 10;
    private double brushSteel = 0;

    private bool PowerIs = false;
    private double buffPowerTime = 1;
    private double powerSteel = 0;

    private bool SniperIs = false;
    private double buffSniperTime = 1;
    private double sniperSteel = 0;


    private bool startGo = false;



    [SerializeField] private bool chekCharactars = false;


    [SerializeField] private float zeroZoneRadiusP = 0.1f; // Зона нульового вводу
    [SerializeField] private float zeroZoneRadiusG = 0.1f; // Зона нульового вводу

    [SerializeField] private AudioSource gunshotAudioSource;
    [SerializeField] private AudioClip gunshotSound;

    [SerializeField] private AudioClip deathSound;

    private void Start()
    {
        playerInstance = this;
        if (!chekCharactars)
        {
            SpeedHorizontal = PlayerPrefs.GetFloat("SpeedHorizontal");
            SpeedVertical = PlayerPrefs.GetFloat("SpeedVertical");
            rotationSpeed = PlayerPrefs.GetFloat("rotationSpeed");


        }
        AudioSource foundAudioSource = FindAudioSource("Sounds");
        if (foundAudioSource != null)
        {
            gunshotAudioSource = foundAudioSource;
        }

        zeroZoneRadiusP = PlayerPrefs.GetFloat("ZeroZonePlayer");
        zeroZoneRadiusG = PlayerPrefs.GetFloat("ZeroZoneGun");




        // Получить размеры экрана
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Получить размеры видимой области
        float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
        float visibleHeight = Camera.main.orthographicSize * 2.0f;
        visibleWidth /= 2;
        visibleHeight /= 2;

       
        //if (visibleWidth>11 && visibleWidth<12 && )
        

        GetComponent<CapsuleCollider2D>().enabled = false;

        PlayerR.transform.position = new Vector3(-visibleWidth - 1, transform.position.y, -3);

        health = FindObjectOfType<Health>();
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
    private void PlayDeathSound()
    {
        if (deathSound != null && gunshotAudioSource != null)
        {
            gunshotAudioSource.PlayOneShot(deathSound);
        }
    }
    private void FixedUpdate()
    {
        if (!startGo)
        {
            PlayerR.transform.position = new Vector3(transform.position.x + 0.07f, transform.position.y, transform.position.z);
            if (Timer < 0)
            {
                startGo = true;
                GetComponent<CapsuleCollider2D>().enabled = true;
                PlayerR.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        else
        {
            Vector2 joystickPlayerInput = new Vector2(JoistickPlayer.Horizontal, JoistickPlayer.Vertical);
            Vector2 joystickGunInput = new Vector2(JoistickGun.Horizontal, JoistickGun.Vertical);

            joystickPlayerInput = ApplyZeroZone(joystickPlayerInput, zeroZoneRadiusP);
            joystickGunInput = ApplyZeroZone(joystickGunInput, zeroZoneRadiusG);

            Rb.velocity = new Vector2(joystickPlayerInput.x * SpeedHorizontal, joystickPlayerInput.y * SpeedVertical);

            Vector2 lookDirection = new Vector2(joystickGunInput.x, joystickGunInput.y);
            if (lookDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (sniperSteel <= 0)
                    PlayerR.rotation = Quaternion.Slerp(PlayerR.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else if (PlayerPrefs.GetInt("SettingAutoRotate") == 0)
            {
                Quaternion idleRotation = Quaternion.AngleAxis(0f, Vector3.forward);
                if (sniperSteel <= 0)
                    PlayerR.rotation = Quaternion.Slerp(PlayerR.rotation, idleRotation, idleRotationSpeed * Time.deltaTime);
            }

            if (Timer <= 0)
            {
                GameObject bulletGun1 = Instantiate(Bullet, Gun1.transform.position, Gun1.rotation);
                bulletGun1.transform.position = new Vector3(Gun1.transform.position.x, Gun1.transform.position.y, 0);
                PlayGunshotSound();

                GameObject bulletGun2 = Instantiate(Bullet, Gun2.transform.position, Gun2.rotation);
                bulletGun2.transform.position = new Vector3(Gun2.transform.position.x, Gun2.transform.position.y, 0);

                if (powerSteel > 0)
                    timeShoot = 0.05f;
                else
                {
                    timeShoot = 0.3f;
                    PowerIs = false;
                }

                Timer = timeShoot;
            }
        }

        if (sniperSteel > 0)
        {
            // Знайдемо ближайшого противника
            Transform nearestEnemy = FindNearestEnemy();

            // Націлюємо дула гравця на знайденого противника, якщо такий є
            if (nearestEnemy != null)
            {
                Vector2 lookDirection = nearestEnemy.position - PlayerR.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                PlayerR.rotation = Quaternion.Slerp(PlayerR.rotation, targetRotation, rotationSpeed * 2 * Time.deltaTime);
            }
        }

        if (PlayerPrefs.GetInt("Difficult") == 0 && sniperSteel <= 0)
        {
            PlayerPrefs.SetInt("SettingAutoRotate", 1);
            // Знайдемо ближайшого противника
            Transform nearestEnemy = FindNearestEnemy();

            // Націлюємо дула гравця на знайденого противника, якщо такий є
            if (nearestEnemy != null)
            {
                Vector2 lookDirection = nearestEnemy.position - PlayerR.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                PlayerR.rotation = Quaternion.Slerp(PlayerR.rotation, targetRotation, rotationSpeed * 0.25f * Time.deltaTime);
            }
        }

        Timer -= Time.deltaTime;
        liveSteel -= Time.deltaTime;
        brushSteel -= Time.deltaTime;
        powerSteel -= Time.deltaTime;
        sniperSteel -= Time.deltaTime;
    }
    private void PlayGunshotSound()
    {
        if (gunshotSound != null && gunshotAudioSource != null)
        {
            gunshotAudioSource.PlayOneShot(gunshotSound);
        }
    }
    private Vector2 ApplyZeroZone(Vector2 input, float radius)
    {
        float magnitude = input.magnitude;

        if (magnitude < radius)
        {
            input = Vector2.zero;
        }
        else
        {
            float normalizedMagnitude = (magnitude - radius) / (1 - radius);
            input = input.normalized * normalizedMagnitude;
        }

        return input;
    }
    private void SpawnBullets()
    {

        int numberOfBullets = 120;
        float angleStep = 360f / numberOfBullets;
        liveSteel = 2;
        PlayDeathSound();
        for (int i = 0; i < numberOfBullets; i++)
        {
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angleStep * i);
            Instantiate(Bullet, PlayerR.position, bulletRotation);
        }

    }

    private void BuffBrusher()
    {

        if (brushSteel < 0.7f && BrushIs)
        {

            brushSteel = buffBrushTime;

            GameObject brushObject = Instantiate(Brush, PlayerR.position, Quaternion.identity);

            StartCoroutine(MakeBrushBlink(brushObject));
        }
        else
        {
            if (buffBrushTime > brushSteel)
                brushSteel = buffBrushTime;
        }
    }

    private IEnumerator MakeBrushBlink(GameObject brushObject)
    {

        SpriteRenderer brushRenderer = brushObject.GetComponent<SpriteRenderer>();
        Vector3 originalScale = brushObject.transform.localScale;

        while (brushSteel > 0)
        {

            if (brushObject == null)
            {
                // Зупинити виконання MakeBrushBlink() після знищення об'єкта

                yield break;
            }

            // Змінюємо прозорість від 0 до 255 протягом 1 секунди, якщо changeTransparency == true
            if (brushSteel < 2f)
            {
                float alpha = Mathf.PingPong(Time.time, 1f);
                byte alphaByte = (byte)(Mathf.Lerp(0f, 179f, alpha));
                Color newColor = brushRenderer.color;
                newColor.a = alphaByte / 179f;
                brushRenderer.color = newColor;
            }
            else
            {
                Color newColor = brushRenderer.color;
                newColor.a = 179;
                brushRenderer.color = newColor;
            }

            // Розміри brush завжди будуть 0.8f x 0.8f
            brushObject.transform.localScale = originalScale * 0.8f;

            // Слідуємо за гравцем
            brushObject.transform.position = PlayerR.position;

            if (brushSteel < 0.1f)
            {
                BrushIs = false;
                Destroy(brushObject);
            }

            yield return null;
        }
    }
    private void BuffPower()
    {
        if (powerSteel < 0.7f && PowerIs)
        {
            powerSteel = buffPowerTime;
        }
        else
        {
            if (buffPowerTime > powerSteel)
                powerSteel = buffPowerTime;
        }
    }
    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
    private void BuffSniper()
    {
        if (sniperSteel < 0.7f && SniperIs)
        {
            sniperSteel = buffSniperTime;

        }
        else
        {
            if (buffSniperTime > sniperSteel)
                sniperSteel = buffSniperTime;
        }
    }









    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PipePart") || collision.gameObject.CompareTag("Enemy"))
        {
            if (liveSteel <= 0 && brushSteel <= 0)
            {
                health.currentHealth--;
                health.UpdateLifeCounter();
                if (health.currentHealth <= 0)
                {
                    GameManager.instance.LoseSurvival();
                    return;
                }
                SpawnBullets();
            }
        }
    }
    public void AdLeave()
    {

        buffBrushTime = 5;
        buffPowerTime = 5;
        buffSniperTime = 5;
        BrushIs = true;
        PowerIs = true;
        SniperIs = true;
        BuffBrusher();
        BuffPower();
        BuffSniper();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PipePart"))
        {
            if (liveSteel <= 0 && brushSteel <= 0)
            {
                health.currentHealth--;
                health.UpdateLifeCounter();
                if (health.currentHealth <= 0)
                {
                    GameManager.instance.LoseSurvival();
                    return;
                }
                SpawnBullets();
            }
        }
        if (other.gameObject.CompareTag("BuffBrush"))
        {
            buffBrush = FindObjectOfType<BuffBrush>();
            buffBrushTime = buffBrush.BuffTimeValue; // Змініть назву змінної на BuffTimeValue
            BrushIs = true;
            Debug.Log("Brush: ");
            BuffBrusher();
        }
        if (other.gameObject.CompareTag("BuffPower"))
        {
            buffBrush = FindObjectOfType<BuffBrush>();
            buffPowerTime = buffBrush.BuffTimeValue; // Змініть назву змінної на BuffTimeValue
            PowerIs = true;
            Debug.Log("Power: ");
            BuffPower();
        }
        if (other.gameObject.CompareTag("BuffSniper"))
        {
            buffBrush = FindObjectOfType<BuffBrush>();
            buffSniperTime = buffBrush.BuffTimeValue;
            SniperIs = true;
            Debug.Log("Sniper: ");
            BuffSniper();
        }
        
    }


}

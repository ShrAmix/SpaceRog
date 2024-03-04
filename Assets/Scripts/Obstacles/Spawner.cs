using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using UnityEngine.EventSystems;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private float TimeToSpawn;
    [SerializeField] private int Rannd, Hard = 0;
    [SerializeField] private GameObject[] PipePrefabs = new GameObject[3];
    [SerializeField] private Transform WhereSpawn;
    [SerializeField] private bool superHard = false;
    [SerializeField] private float Timer = 4, HardTime;
    [SerializeField] private float WhyTimeMinus = 0;

    private float hardT, spawnT, TimerHard;

    private void Awake()
    {
        if (!superHard)
        {
            if (PlayerPrefs.GetInt("Difficult") == 0)
                WhyTimeMinus = 0.015f;
            else if (PlayerPrefs.GetInt("Difficult") == 2)
                WhyTimeMinus = 0.018f;
        }

        hardT = HardTime;
        spawnT = Timer;
        TimerHard = TimeToSpawn;
    }

    private void FixedUpdate()
    {
        if (IsHost || SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (hardT < 0 && Hard < PipePrefabs.Length && !superHard)
            {
                Hard = Hard + 3;
                if ((Hard / 3) + 1 > PlayerPrefs.GetInt("maxWave" + $"{PlayerPrefs.GetInt("Difficult")}"))
                {
                    PlayerPrefs.SetInt("maxWave" + $"{PlayerPrefs.GetInt("Difficult")}", (Hard / 3) + 1);
                    PlayerPrefs.SetInt("DayWave" + $"{PlayerPrefs.GetInt("Difficult")}", (Hard / 3) + 1);
                }
                hardT = HardTime;
                TimerHard = TimeToSpawn;

                string sms = "WAVE " + ((Hard / 3) + 1);

                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "’¬»Àﬂ " + ((Hard / 3) + 1);
                HintSystem.Instance.ShowHint(sms);
            }

            if (Hard == PipePrefabs.Length)
            {
                superHard = true;
                if (Hard / 3 > PlayerPrefs.GetInt("maxWave" + $"{PlayerPrefs.GetInt("Difficult")}"))
                {
                    PlayerPrefs.SetInt("maxWave" + $"{PlayerPrefs.GetInt("Difficult")}", 5);
                    PlayerPrefs.SetInt("DayWave" + $"{PlayerPrefs.GetInt("Difficult")}", 5);
                }
                string sms = "WAVE 5";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "’¬»Àﬂ 5";
                HintSystem.Instance.ShowHint(sms);
                Hard = 0;
                TimeToSpawn /= 2;
                TimerHard = TimeToSpawn;
                Rannd = PipePrefabs.Length;
            }

            if (spawnT <= 0)
            {
                spawnT = TimerHard;
                if (!superHard)
                {
                    TimerHard -= WhyTimeMinus;
                    Debug.Log(TimerHard);
                }

                if (SceneManager.GetActiveScene().buildIndex == 8)
                {
                    // Spawn on the server and synchronize to all clients
                   Gun();
                }
                else
                {
                    var prefabToInstantiate = PipePrefabs[Random.Range(Hard, Rannd + Hard)];
                    var instance = Instantiate(prefabToInstantiate, WhereSpawn);
                   
                }
            }
            else
            {
                spawnT -= Time.deltaTime;
            }
            hardT -= Time.deltaTime;
        }
        
    }
    [ServerRpc]
    private void RequestFireServerRpc(Vector3 dir, int who, float rb, float rot, float moveDirection)
    {
        FireClientRpc(dir, who, rb, rot, moveDirection);
    }

    [ClientRpc]
    private void FireClientRpc(Vector3 dir, int who, float rb, float rot, float moveDirection)
    {
        if (!IsOwner) ExecuteShoot(dir, who, rb, rot, moveDirection);
    }

    private void ExecuteShoot(Vector3 dir, int who, float rb, float rot, float moveDirection)
    {
        var prefabToInstantiate = PipePrefabs[who];

        var instance = Instantiate(prefabToInstantiate, WhereSpawn);
        BuffBrush b = instance.GetComponent<BuffBrush>();
        b.MoveClient = howBool;
        b.mover = rb;
        b.moveDirection = moveDirection;
        b.angle = rot;
    }

    private bool howBool = false;
    private float rb, rot, moveDirection;

    public void Gun()
    {
        if (!IsOwner) return;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
        visibleWidth /= 2;
        rb = Random.Range(-visibleWidth + 1, visibleWidth - 1);
        moveDirection = transform.position.x < 0f ? 1f : -1f;
        rot = transform.position.x < 0f ? Random.Range(-90f, -90f + 30) : Random.Range(-30 + 90f, 90f);

        int sp = Random.Range(Hard, Rannd + Hard);
        var dir = transform.forward;

        // Send off the request to be executed on all clients
        RequestFireServerRpc(dir, sp, rb, rot, moveDirection);

        // Fire locally immediately
        ExecuteShoot(dir, sp, rb, rot, moveDirection);
    }


}

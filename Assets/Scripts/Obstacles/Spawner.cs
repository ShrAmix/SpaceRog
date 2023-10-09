using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class Spawner : MonoBehaviour
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
        hardT = HardTime;
        spawnT = Timer;
        TimerHard = TimeToSpawn;

        // backgroundData = FindObjectOfType<BackgroundData>().transform;

    }


    private void FixedUpdate()
    {
        if (hardT < 0 && Hard < PipePrefabs.Length && !superHard)
        {
            Hard = Hard + 3;
            if ((Hard / 3) + 1 > PlayerPrefs.GetInt("maxWave"))
                PlayerPrefs.SetInt("maxWave", (Hard / 3) + 1);
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
            if (Hard / 3 > PlayerPrefs.GetInt("maxWave"))
                PlayerPrefs.SetInt("maxWave", 5);
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

            Instantiate(PipePrefabs[Random.Range(Hard, Rannd + Hard)], WhereSpawn);
        }
        else
        {
            spawnT -= Time.deltaTime;
        }
        hardT -= Time.deltaTime;
    }
}

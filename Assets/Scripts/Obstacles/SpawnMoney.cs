using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnMoney : MonoBehaviour
{
    public GameObject[] MoneyPrefabs = new GameObject[3];
    public Transform Spawnn;
    public int Rannd = 4;
    private float Timer=1, HardTime;
    private void FixedUpdate()
    {
        if(Timer==1)
        {
            GameObject Money = Instantiate(MoneyPrefabs[Random.Range(0, Rannd)], transform.position, Quaternion.identity);
            Money.transform.position = new Vector3(Money.transform.position.x, Money.transform.position.y, 0);
            Timer++;
        }
            
    }
}

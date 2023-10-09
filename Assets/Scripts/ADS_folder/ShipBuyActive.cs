using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuyActive : MonoBehaviour
{
    [SerializeField] GameObject[] shipBuyActivePrefab;
    public static ShipBuyActive shipBuyActive;

    private int index=0;

    private void Start()
    {
        shipBuyActive = this;
        ShipWhy();


    }
    public void ShipWhy()
    {
        for(int i=0;i<shipBuyActivePrefab.Length;i++)
        {
            if (PlayerPrefs.GetInt("ship" + $"{i + 1}" + "Access") == 0)
            {
                index = i + 1;
                break;
            }
        }
        if(index==0)
        {
            for (int i = 0; i < shipBuyActivePrefab.Length; i++)
            {
                shipBuyActivePrefab[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < shipBuyActivePrefab.Length; i++)
                if (index-1!=i)
                    shipBuyActivePrefab[i].SetActive(false);
        }
    }
}

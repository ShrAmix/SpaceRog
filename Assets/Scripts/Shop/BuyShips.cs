using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyShips : MonoBehaviour
{
    public string objectName;
    public int price, access;
    public GameObject block;
    public TextMeshProUGUI objectPrice;
    private void Awake()
    {
        access = PlayerPrefs.GetInt(objectName + "Access");
        objectPrice.text = price.ToString();
        if (access == 1)
        {
            block.SetActive(false);
            objectPrice.gameObject.SetActive(false);
        }


    }
    public void OnBuyObject()
    {
        int coins = PlayerPrefs.GetInt("Money");
        access = PlayerPrefs.GetInt(objectName + "Access");
        if (access == 0)
        {
            if (coins >= price)
            {
                PlayerPrefs.SetInt(objectName + "Access", 1);
                PlayerPrefs.SetInt("Money", coins - price);
                block.SetActive(false);
                objectPrice.gameObject.SetActive(false);
                MenuScore.Instance.SetScore(0, -price);
            }
            else
            {
                int coinDel = price - coins;
                string sms = "YOU ARE MISSING " + coinDel + " COINS";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                {

                    sms = "ÂÀÌ ÍÅ ÂÈÑÒÀ×Àª " + coinDel + " ÌÎÍÅÒ";

                }
                HintSystem.Instance.ShowHint(sms);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BuyBuff : MonoBehaviour
{
    public string objectName;
    public int[] price;
    public int access;

    public TextMeshProUGUI objectPrice;
    public Image buttonImage; // Reference to the Image component

    [SerializeField] private Sprite[] imageBuff;
    [SerializeField] private GameObject buttonBuff;




    private void Awake()
    {
        access = PlayerPrefs.GetInt(objectName + "BuffBuy");
        objectPrice.text = access == 3 ? "MAX" : price[access].ToString();

        // Make sure to assign the "buttonImage" variable in the Unity editor
        buttonImage.sprite = imageBuff[access];
    }

    public void OnBuyObject()
    {
        int coins = PlayerPrefs.GetInt("Money");

        if (access != 3)
        {
            if (coins >= price[access] && PlayerPrefs.GetInt("ship" + $"{access + 1}Access") == 1)
            {
                access++;
                PlayerPrefs.SetInt(objectName + "BuffBuy", access);//sniperBuffBuy brushBuffBuy powerBuffBuy
                PlayerPrefs.SetInt("Money", coins - price[access]);

                MenuScore.Instance.SetScore(0, -price[access]);
                objectPrice.text = access == 3 ? "MAX" : price[access].ToString();

                buttonImage.sprite = imageBuff[access];
            }
            else if (coins < price[access] && PlayerPrefs.GetInt("ship" + $"{access + 1}Access") == 1)
            {
                int requiredShipLevel = price[access] - coins;
                string sms;
                if (PlayerPrefs.GetString("SelectedLocale") == "en")
                    sms = "YOU LACK " + requiredShipLevel + " COINS";
                else
                    sms = "ÂÀÌ ÍÅ ÂÈÑÒÀ×Àª " + requiredShipLevel + " ÌÎÍÅÒ";
                HintSystem.Instance.ShowHint(sms);
            }
            else
            {
                int requiredShipLevel = access + 2;
                string sms;
                if (PlayerPrefs.GetString("SelectedLocale") == "en")
                    sms = "TO PUMP THE BUFF, BUY A LEVEL " + requiredShipLevel + " SHIP";
                else
                    sms = "ÙÎÁ ÏÐÎÊÀ×ÀÒÈ ÁÀÔÔ, ÏÐÈÄÁÀÉÒÅ ÊÎÐÀÁÅËÜ " + requiredShipLevel + "-ÃÎ Ð²ÂÍß";
                HintSystem.Instance.ShowHint(sms);
            }
        }
        else
        {
            objectPrice.text = "MAX";
        }
    }
}

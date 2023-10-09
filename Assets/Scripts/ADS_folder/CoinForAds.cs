using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class CoinForAds : MonoBehaviour
{
    public TextMeshProUGUI adsButton;
    private int coinsForAds;
    public static CoinForAds instance;
    void Start()
    {
        instance = this;
        System.DateTime currentTime = System.DateTime.UtcNow;
        if (currentTime.Day != PlayerPrefs.GetInt("DayNow"))
        {
            PlayerPrefs.SetInt("CoinAds",50);
        }
        adsButton.text = ""+PlayerPrefs.GetInt("CoinAds");
    }

}

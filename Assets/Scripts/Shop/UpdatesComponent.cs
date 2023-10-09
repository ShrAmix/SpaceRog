using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UpdatesComponent : MonoBehaviour
{
    public static UpdatesComponent instance;
    [SerializeField] private string proburt;
    [SerializeField] private float floatPr;


    private float UpdateNow;

    public float MaxLimite;
    public float MinLimite;

    public Image[] emptyIcon;
    public Sprite fillIcon;
    public Sprite delIcon;

    private int upd;

    [SerializeField] private PlayerSelecter playerSelecter;
    [SerializeField] private int WhyShip;


    public TextMeshProUGUI whyMoney;

    [SerializeField] private int[] Prices;

    private void Start()
    {

        UpdateNow = PlayerPrefs.GetFloat(proburt + "Update" + WhyShip);
        //Debug.Log("Ship"+WhyShip+" "+proburt+" "+PlayerPrefs.GetFloat(proburt + "Update" + WhyShip));
        TextManag();
        float currentValue = PlayerPrefs.GetFloat(proburt);
        if (PlayerPrefs.GetFloat(proburt) < MinLimite || PlayerPrefs.GetFloat(proburt) > MaxLimite)
        {
            PlayerPrefs.SetFloat(proburt, MinLimite);
        }
        upd = Mathf.FloorToInt((PlayerPrefs.GetFloat(proburt) - MinLimite) / floatPr) + 1;
        if (upd < 1)
            upd = 1;
        iconsUpdate();

        //ResetAll();
    }

    public void productUpdateFloat()
    {

        if (upd == UpdateNow && emptyIcon.Length != UpdateNow)
        {
            int coins = PlayerPrefs.GetInt("Money");
            if (coins >= Prices[(int)UpdateNow])
            {
                PlayerPrefs.SetInt("Money", coins - Prices[(int)UpdateNow]);
                MenuScore.Instance.SetScore(0, -Prices[(int)UpdateNow]);
                UpdateNow++;
                PlayerPrefs.SetFloat(proburt + "Update" + WhyShip, UpdateNow);

                TextManag();
            }
            else
            {
                int coinDel = Prices[(int)UpdateNow] - coins;
                string sms = "YOU ARE MISSING " + coinDel + " COINS";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                {

                    sms = "ÂÀÌ ÍÅ ÂÈÑÒÀ×Àª " + coinDel + " ÌÎÍÅÒ";

                }
                HintSystem.Instance.ShowHint(sms);
            }
        }
        checkBord();
        if (upd != emptyIcon.Length && upd != UpdateNow)
        {
            float count = PlayerPrefs.GetFloat(proburt);
            PlayerPrefs.SetFloat(proburt, count + floatPr);
            emptyIcon[upd].overrideSprite = fillIcon;
            upd++;
        }


        /*Debug.Log("");
       Debug.Log(upd);
       Debug.Log(PlayerPrefs.GetFloat(proburt));*/
    }

    public void productDowngrateFloat()
    {
        checkBord();
        if (upd != 1)
        {
            float count = PlayerPrefs.GetFloat(proburt);
            PlayerPrefs.SetFloat(proburt, count - floatPr);
            int u = upd - 1;
            emptyIcon[u].overrideSprite = delIcon;
            upd--;
        }
        /*Debug.Log("");
        Debug.Log(upd);
        Debug.Log(PlayerPrefs.GetFloat(proburt));*/
    }

    void iconsUpdate()
    {
        for (int i = 0; i < emptyIcon.Length; i++)
        {
            emptyIcon[i].overrideSprite = delIcon;
        }
        if (upd != 0)
        {

            for (int i = 0; i < upd; i++)
            {
                emptyIcon[i].overrideSprite = fillIcon;
            }
        }

    }
    private void TextManag()
    {
        if ((int)UpdateNow == emptyIcon.Length)
            whyMoney.text = "MAX";
        else
            whyMoney.text = "" + Prices[(int)UpdateNow];

    }
    void checkBord()
    {
        if (PlayerPrefs.GetFloat(proburt) < MinLimite || PlayerPrefs.GetFloat(proburt) > MaxLimite)
        {
            PlayerPrefs.SetFloat(proburt, MinLimite);
            upd = Mathf.FloorToInt((PlayerPrefs.GetFloat(proburt) - MinLimite) / floatPr) + 1;
            if (upd < 1)
                upd = 1;
            iconsUpdate();
        }
    }
    public void ResetAll()
    {
        // It's better to use arrays or dictionaries to store the values for different indexes.
        // Here's an example using arrays:
        float[] defaultSpeedHorizontal = { 8f, 10f, 12f, 14f };
        float[] defaultSpeedVertical = { 6f, 8f, 9f, 11f };
        float[] defaultRotationSpeed = { 5f, 6f, 7f, 9f };
        float[] defaultMaxHealth = { 1f, 2f, 3f, 4f };

        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetFloat($"{i}SpeedHorizontal", defaultSpeedHorizontal[i]);
            PlayerPrefs.SetFloat($"{i}SpeedVertical", defaultSpeedVertical[i]);
            PlayerPrefs.SetFloat($"{i}rotationSpeed", defaultRotationSpeed[i]);
            PlayerPrefs.SetFloat($"{i}maxHealth", defaultMaxHealth[i]);
        }

        upd = 1;

        iconsUpdate();
    }

    public void LoadUpgrate(int index)
    {
        if (index >= 0 && index < 4)
        {
            PlayerPrefs.SetFloat("SpeedHorizontal", PlayerPrefs.GetFloat($"{index}SpeedHorizontal"));
            PlayerPrefs.SetFloat("SpeedVertical", PlayerPrefs.GetFloat($"{index}SpeedVertical"));
            PlayerPrefs.SetFloat("rotationSpeed", PlayerPrefs.GetFloat($"{index}rotationSpeed"));
            PlayerPrefs.SetFloat("maxHealth", PlayerPrefs.GetFloat($"{index}maxHealth"));
        }

        upd = Mathf.FloorToInt((PlayerPrefs.GetFloat(proburt) - MinLimite) / floatPr) + 1;
        if (upd < 1)
            upd = 1;
        TextManag();
        iconsUpdate();
    }
}

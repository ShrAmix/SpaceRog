using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelecter : MonoBehaviour
{
    public static PlayerSelecter Instance;
    [SerializeField] private UpdatesComponent[] updatesComponents;


    private GameObject[] characters;
    public int index = 1;
    private Button[] button;

    [SerializeField] private Button[] takeButton;
    [SerializeField] private Color selectedColor;
    private Color originalButtonColor;

    [SerializeField] private bool isGame = false;

    private void Start()
    {
        button = new Button[takeButton.Length];
        index = PlayerPrefs.GetInt("SelectPlayer");

        characters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
        if (characters[index])
        {
            characters[index].SetActive(true);
        }
        if (!isGame)
        {
            for (int i = 0; i < takeButton.Length; i++)
            {
                button[i] = takeButton[i].GetComponent<Button>();
                originalButtonColor = button[i].colors.normalColor;
            }


            PakUpdate();
        }
    }

    public void SelectLeft()
    {
        int ind = index - 1;
        if (ind < 0)
            ind = characters.Length - 1;
        if (PlayerPrefs.GetInt($"ship{ind}Access") == 1)
        {
            characters[index].SetActive(false);
            SaveUpdate();
            index--;

            if (index < 0)
            {

                index = characters.Length - 1;
            }
            characters[index].SetActive(true);


            PakUpdate();
        }
        else
        {

            string sms = "BUY THE LAST SHIP";
            if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                sms = "ÏÐÈÄÁÀÉ ÎÑÒÀÍÍ²É ÊÎÐÀÁÅËÜ";
            HintSystem.Instance.ShowHint(sms);
        }
    }

    public void SelectRight()
    {
        int ind = index + 1;
        if (PlayerPrefs.GetInt($"ship{ind}Access") == 1 || PlayerPrefs.GetInt($"ship{ind}Access") == 0 && PlayerPrefs.GetInt($"ship{ind - 1}Access") == 1)
        {
            characters[index].SetActive(false);
            SaveUpdate();
            index++;

            if (index == characters.Length)
            {
                index = 0;
            }
            characters[index].SetActive(true);


            PakUpdate();
        }
        else
        {
            if (ind != 4)
            {
                string sms = "BUY A LEVEL " + ind + " SHIP";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "ÏÐÈÄÁÀÉ ÊÎÐÀÁÅËÜ " + ind + "-ÃÎ Ð²ÂÅÍß";
                HintSystem.Instance.ShowHint(sms);
            }
            else
            {
                string sms = "BUY THE LAST SHIP";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "ÏÐÈÄÁÀÉ ÎÑÒÀÍÍ²É ÊÎÐÀÁÅËÜ";
                HintSystem.Instance.ShowHint(sms);
            }
        }
    }
    private void SaveUpdate()
    {
        PlayerPrefs.SetFloat($"{index}SpeedHorizontal", PlayerPrefs.GetFloat("SpeedHorizontal"));
        PlayerPrefs.SetFloat($"{index}SpeedVertical", PlayerPrefs.GetFloat("SpeedVertical"));
        PlayerPrefs.SetFloat($"{index}rotationSpeed", PlayerPrefs.GetFloat("rotationSpeed"));
        PlayerPrefs.SetFloat($"{index}maxHealth", PlayerPrefs.GetFloat("maxHealth"));
    }
    private void PakUpdate()
    {
        for (int i = index * 4; i < index * 4 + 4; i++)
        {
            updatesComponents[i].LoadUpgrate(index);
        }
    }
    public void StartPref()
    {
        if (PlayerPrefs.GetInt($"ship{index}Access") == 1)
        {
            PlayerPrefs.SetInt("SelectPlayer", index);

           // Debug.Log("Ìè çáåðåãëè " + index + "Êîðàáåëü");
        }
        else
        {
            index = PlayerPrefs.GetInt("SelectPlayer");
            PakUpdate();
        }
        SaveUpdate();
    }


}
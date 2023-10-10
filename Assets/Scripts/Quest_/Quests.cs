using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;


[System.Serializable]
public class Quests : MonoBehaviour
{
    public string[] questTexts; // Масив стрінгів для квестів
    public string[] questTextsUK; // Масив стрінгів для квестів
    public int Award;
    public TextMeshProUGUI textMeshPro; // Посилання на компонент TextMeshPro
    public TextMeshProUGUI textAward; // Посилання на компонент TextMeshPro
    [SerializeField] private TextMeshProUGUI textHowMoney;
    public GameObject button;
    public int Hard, hardd;

    private int questWhy;

    private int whatDo;


    private void Start()
    {
        int hardd = PlayerPrefs.GetInt("Difficult");
        if(hardd == 0)
        {
            Award /= 2;
        }
        else if(hardd == 2) 
        {
            float u = Award * 1.5f;
            Award = (int)u;
        }
        System.DateTime currentTime = System.DateTime.UtcNow;
        questWhy = PlayerPrefs.GetInt("DayQuest" + $"{Hard}");
        textHowMoney.text = "+" + Award;
        //Debug.Log(PlayerPrefs.GetInt("DayNow" + $"{Hard}") + "  " + currentTime.Day);


        if (currentTime.Day != PlayerPrefs.GetInt("DayNow" + $"{Hard}"))
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt("EnemysDay" + $"{i}", 0);
                PlayerPrefs.SetInt("MoneyDay" + $"{i}", 0);
                PlayerPrefs.SetInt("ScoreDay" + $"{i}", 0);
                PlayerPrefs.SetInt("DayWave" + $"{i}", 0);
            }

            PlayerPrefs.SetInt("DayNow" + $"{Hard}", currentTime.Day);
            PlayerPrefs.SetInt("DayQuest" + $"{Hard}", Random.Range(1, questTexts.Length));
            questWhy = PlayerPrefs.GetInt("DayQuest" + $"{Hard}");
        }

        textMeshPro.text = questTexts[questWhy];

        if (PlayerPrefs.GetString("SelectedLocale") == "uk")
            textMeshPro.text = questTextsUK[questWhy];

        if (PlayerPrefs.GetInt("DayQuest" + $"{Hard}") == 0)
            gameObject.SetActive(false);
        button.SetActive(false);
        QuestsSettings();


        //Debug.Log(PlayerPrefs.GetInt("DayNow" + $"{Hard}") + "  " + currentTime.Day);
        //Debug.Log("");
    }
    public void OnButtonQuestComplete()
    {
        int mon = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", mon + Award);
        button.SetActive(false);
        gameObject.SetActive(false);
        MenuScore.Instance.ScoreTXT(PlayerPrefs.GetInt("BestScore"), PlayerPrefs.GetInt("Money"));


        PlayerPrefs.SetInt("DayQuest" + $"{Hard}", 0);
        questWhy = 0;
    }

    private void QuestsSettings()
    {
        switch (questWhy)
        {
            case 0:
                button.SetActive(false);
                break;
            case 1://Destroy X enemies
                {
                    whatDo = PlayerPrefs.GetInt("EnemysDay"  + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    Debug.Log(whatDo+ "          Destroy X enemies");
                    int number = ExtractNumberFromTask(questTexts[questWhy]);

                    LocalText(whatDo, number);
                    if (whatDo >= number)
                    {
                        button.SetActive(true);
                    }
                    break;
                }

            case 2://Score 100 points in survival
                {
                    whatDo = PlayerPrefs.GetInt("ScoreDay" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    int number = ExtractNumberFromTask(questTexts[questWhy]);
                    LocalText(whatDo, number);
                    if (whatDo >= number)
                    {
                        button.SetActive(true);
                    }
                    break;
                }
            case 3://Collect X coins
                {

                    whatDo = PlayerPrefs.GetInt("MoneyDay" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    int number = ExtractNumberFromTask(questTexts[questWhy]);
                    LocalText(whatDo, number);
                    if (whatDo >= number)
                    {
                        button.SetActive(true);
                    }
                    break;
                }
            case 4://Get to the Xth wave.
                {
                    whatDo = PlayerPrefs.GetInt("DayWave" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    int number = ExtractNumberFromTask(questTexts[questWhy]);
                    LocalText(whatDo, number);
                    if (whatDo >= number)
                    {
                        button.SetActive(true);
                    }
                    break;

                }
        }
    }
    private void LocalText(int x, int y)
    {
        textAward.text = x + " of " + y;
        if (PlayerPrefs.GetString("SelectedLocale") == "uk")
            textAward.text = x + " з " + y;
    }


    private int ExtractNumberFromTask(string taskString)
    {
        // Використовуємо регулярний вираз для пошуку числа в стрічці
        // \d+ означає "одна або більше цифра"
        Match match = Regex.Match(taskString, @"\d+");

        // Перевіряємо, чи було знайдено число
        if (match.Success)
        {
            // Якщо число знайдено, витягаємо його з групи та перетворюємо у ціле число
            int number = int.Parse(match.Groups[0].Value);
            return number;
        }

        // Якщо число не знайдено, повертаємо 0 або інше значення за замовчуванням
        return 0;
    }
}

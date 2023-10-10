using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class QuestALL : MonoBehaviour
{
    [SerializeField] private string[] questTexts; // Масив стрінгів для квестів
    [SerializeField] private string[] quesTextsUk;
    [SerializeField] private int[] Award;

    [SerializeField] private TextMeshProUGUI textMeshPro; // Посилання на компонент TextMeshPro
    [SerializeField] private TextMeshProUGUI textAward; // Посилання на компонент TextMeshPro
    [SerializeField] private TextMeshProUGUI textHowMoney; // Посилання на компонент TextMeshPro
    [SerializeField] private GameObject button;

    [SerializeField] private int questWhy;

    private int whatDo;
    private int hardd;
    private void Start()
    {
        int hardd = PlayerPrefs.GetInt("Difficult");
        if (hardd == 0)
        {
            for(int i=0;i<Award.Length;i++)
            Award[i] /= 2;
        }
        else if (hardd == 2)
        {
            for (int i = 0; i < Award.Length; i++)
            {
                float u = Award[i] * 1.5f;
                Award[i] = (int)u;
            }
        }


        hardd = PlayerPrefs.GetInt("Difficult", 1);
        if (PlayerPrefs.GetInt("QuestAll" + $"{questWhy}") >= questTexts.Length - 1)
            gameObject.SetActive(false);
        else
        {
            if (PlayerPrefs.GetString("SelectedLocale") == "uk")
                questTexts = quesTextsUk;
            System.DateTime currentTime = System.DateTime.UtcNow;
            textMeshPro.text = questTexts[PlayerPrefs.GetInt("QuestAll" + $"{questWhy}")];
            textHowMoney.text = "+" + Award[PlayerPrefs.GetInt("QuestAll" + $"{questWhy}")];

            button.SetActive(false);
            QuestsSettings();
            //Debug.Log(PlayerPrefs.GetInt("QuestAll" + $"{questWhy}"));
        }

    }
    public void OnButtonQuestComplete()
    {
        int mon = PlayerPrefs.GetInt("Money");
        mon += Award[PlayerPrefs.GetInt("QuestAll" + $"{questWhy}")];
        PlayerPrefs.SetInt("Money", mon  );
        button.SetActive(false);
        PlayerPrefs.SetInt("QuestAll" + $"{questWhy}", PlayerPrefs.GetInt("QuestAll" + $"{questWhy}") + 1);
        if (PlayerPrefs.GetInt("QuestAll" + $"{questWhy}") == questTexts.Length)
            gameObject.SetActive(false);
        else
        {
            
            if (PlayerPrefs.GetString("SelectedLocale") == "uk")
                questTexts = quesTextsUk;
            textMeshPro.text = questTexts[PlayerPrefs.GetInt("QuestAll" + $"{questWhy}")];
            textHowMoney.text = "+" + Award[PlayerPrefs.GetInt("QuestAll" + $"{questWhy}")];
            QuestsSettings();
        }

        MenuScore.Instance.ScoreTXT(PlayerPrefs.GetInt("BestScore" + $"{PlayerPrefs.GetInt("Difficult")}"), PlayerPrefs.GetInt("Money"));


        PlayerPrefs.SetInt("AllQuest" + $"{questWhy}", 1);
        //questWhy = 1;
    }

    private void QuestsSettings()
    {
        switch (questWhy)
        {
            case 0:
                button.SetActive(false);
                break;
            case 1://DESTROY 50 ENEMIES
                {
                    
                    whatDo = PlayerPrefs.GetInt("Enemys" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    QuestComplite();
                    break;
                }

            case 2://SCORE 100 POINTS IN SURVIVAL
                {
                    
                    whatDo = PlayerPrefs.GetInt("BestScore" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    QuestComplite();
                    break;
                }
            case 3://COLLECT 50 COINS
                {
                    whatDo = PlayerPrefs.GetInt("MaxMoney" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    QuestComplite();
                    break;
                }
            case 4://GET TO THE 5TH WAVE
                {
                    whatDo = PlayerPrefs.GetInt("maxWave" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
                    QuestComplite();
                    break;

                }
            case 5: //BUY THE 4TH SHIP.
                {
                    whatDo = 1;
                    for(int i = 0; i < 4; i++)
                    {
                        if (PlayerPrefs.GetInt("ship" + $"{i}" + "Access") == 0) break;
                        else whatDo = i + 1;
                    }
                    QuestComplite();
                    break;
                }
            case 6: //UPGRADE THE 1ST SPACESHIP TO THE MAXIMUM
                {
                    whatDo = 0; 
                    for (int i = 0; i < 4; i++)
                    {
                        if (PlayerPrefs.GetInt("ship" + $"{i}" + "Access") == 1)
                        {
                            if (
                            PlayerPrefs.GetFloat("SpeedHorizontal" + "Update" + $"{i}") != 8 ||
                            PlayerPrefs.GetFloat("SpeedVertical" + "Update" + $"{i}") != 8 ||
                            PlayerPrefs.GetFloat("rotationSpeed" + "Update" + $"{i}") != 8 ||
                            PlayerPrefs.GetFloat("maxHealth" + "Update" + $"{i}") != 3
                            )
                            {
                                i = 4;
                                break;
                            }
                            else whatDo = i + 1;
                        }
                        else
                            break;
                    }

                    QuestComplite();
                    break;
                }
        }
    }
    private void QuestComplite()
    {
        int number = ExtractNumberFromTask(questTexts[PlayerPrefs.GetInt("QuestAll" + $"{questWhy}")]);
        LocalText(whatDo, number);
        if (whatDo >= number)
        {
            button.SetActive(true);
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

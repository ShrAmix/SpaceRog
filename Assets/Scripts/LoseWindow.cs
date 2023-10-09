using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseWindow : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI PlusMoneyText;
    void Start()
    {
        PlaeyrLose();
        MoneyGo();
    }
    public void PlaeyrLose()
    {
        int Score = ScoreManager.Instance.Score;
        ScoreText.text = Score.ToString();
        int hard = PlayerPrefs.GetInt("Difficult", 1);
        int BestScore = PlayerPrefs.GetInt("BestScore" + $"{hard}");
        if (Score > BestScore)
        {
            BestScore = Score;
        }
        BestScoreText.text = BestScore.ToString();
        PlayerPrefs.SetInt("BestScore" + $"{hard}", BestScore);
        PlayerPrefs.SetInt("ScoreDay", BestScore);
    }
    public void MoneyGo()
    {
        int Money = ScoreManager.Instance.Money;
        PlusMoneyText.text = "+"+Money.ToString();
        int AllMoney = PlayerPrefs.GetInt("Money");
        AllMoney += Money;
        
        MoneyText.text = AllMoney.ToString();
        PlayerPrefs.SetInt("Money", AllMoney);
        
    }
}

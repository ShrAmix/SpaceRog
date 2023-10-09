using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; }
    public int Score { get; set; }
    public int Money { get; set; }
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI MoneyText;

    private void Start()
    {
        Instance=this;
        SetScore(0);
        SetMoney(0);
    }
    public void SetScore(int Score)
    {
        this.Score+=Score;
        ScoreText.text = this.Score.ToString();
    }
    public void SetMoney(int Money)
    {
        this.Money += Money;
 
        MoneyText.text = this.Money.ToString();
    }
}

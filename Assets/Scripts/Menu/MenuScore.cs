using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class MenuScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreTextS;
    
    [SerializeField] private TextMeshProUGUI MoneyText;
    public static MenuScore Instance { get; set; }
    public int ScoreS { get; set; }
    
    public int Money { get; set; }
    private void Start()
    {
        Instance = this;
        int hard = PlayerPrefs.GetInt("Difficult", 1);
        SetScore(PlayerPrefs.GetInt("BestScore" + $"{hard}"), PlayerPrefs.GetInt("Money"));
    }
 
    public void SetScore(int ScoreS,int Money)
    {
        this.ScoreS += ScoreS;
       
        this.Money += Money;
        ScoreTextS.text = this.ScoreS.ToString();
        
        MoneyText.text=this.Money.ToString();
    }
    public void ScoreTXT(int ScoreS, int Money)
    {
        this.ScoreS = ScoreS;

        this.Money = Money;
        ScoreTextS.text = this.ScoreS.ToString();

        MoneyText.text = this.Money.ToString();
    }
}

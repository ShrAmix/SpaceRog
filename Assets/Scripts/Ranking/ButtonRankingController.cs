using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRankingController : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI email;
    public TextMeshProUGUI password;
    private DataBase db;
    void Start()
    {
        db = GetComponent<DataBase>();
    }

    public void ButtonClick()
    {
        db.SaveData(name.text, password.text, email.text);
    }
    public void ButtonLoadClick()
    {
        StartCoroutine(db.LoadData(name.text));
    }
}

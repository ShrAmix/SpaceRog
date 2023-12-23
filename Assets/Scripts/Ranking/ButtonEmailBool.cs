using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ButtonEmailBool : MonoBehaviour
{
    public TextMeshProUGUI email;
    private string ml;
    private Button buttonComponent;
    private void Start()
    {
        buttonComponent = GetComponent<Button>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ml=email.text;

        string lastTwoCharacters="";
        if(ml.Length>11)
        for (int i = ml.Length - 4; i < ml.Length; i++)
            lastTwoCharacters += ml[i];

        string boo = "com";
        //Debug.Log("||||||" + lastTwoCharacters + "||||||" +boo);
        if (lastTwoCharacters ==boo)
        {
            buttonComponent.interactable = true;
        }
        else
        {
            buttonComponent.interactable = false;
        }
    }
}

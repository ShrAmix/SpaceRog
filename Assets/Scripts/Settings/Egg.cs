using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Egg : MonoBehaviour
{
    

    private float audioValue = -1; // �������� � m_Audio (�� ������������ -1 ��� ������� �������)
    private float soundValue = -1; // �������� � m_Sound (�� ������������ -1 ��� ������� �������)

    private float lastUpdateTime = 0.0f;
    [SerializeField] private Slider m_Audio;
    [SerializeField] private Slider m_Sound;
    [SerializeField] private float updateInterval = 10.0f; // �������� ��� ��������, �� ������� ��������
    private string sms = "";
    private int egg1, egg2;

    private void FixedUpdate()
    {

        // ��������� �������� ������� � ������ m_Audio �� m_Sound
        float audioValue = PlayerPrefs.GetFloat("ZeroZonePlayer") * 100;
        float soundValue = PlayerPrefs.GetFloat("ZeroZoneGun") * 100;
        if (Convert.ToInt32(audioValue) != egg1 || Convert.ToInt32(soundValue) != egg2)
        {
            egg1 = Convert.ToInt32(audioValue); // ��� int egg1 = (int)audioValue;
            egg2 = Convert.ToInt32(soundValue); // ��� int egg2 = (int)soundValue;
            lastUpdateTime = 0;
        }
        //Debug.Log(lastUpdateTime +"  ^  "+ updateInterval);

        if (egg1 == 20 && egg2 == 04 && lastUpdateTime>= updateInterval) 
        {
            sms = "������� Ĳ��";
            HintSystem.Instance.ShowHint(sms);
             lastUpdateTime = -100000;
        }
        if (egg1 == 20 && egg2 == 11 && lastUpdateTime >= updateInterval)
        {
            SceneManager.LoadScene(8);
        }



        lastUpdateTime += Time.deltaTime;


    }

}

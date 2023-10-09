using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderInSettings : MonoBehaviour
{
    [SerializeField] private string namePref;
    [SerializeField] private string txtTMP;
    [SerializeField] private string txtTMPuk;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI TMP;

    private void Start()
    {
        if (PlayerPrefs.GetString("SelectedLocale") == "en")
            TMP.text = txtTMP + PlayerPrefs.GetFloat(namePref).ToString("F2");
        else
            TMP.text = txtTMPuk + PlayerPrefs.GetFloat(namePref).ToString("F2");
        slider.value = PlayerPrefs.GetFloat(namePref);
        
    }

    void FixedUpdate()
    {
        if (PlayerPrefs.GetString("SelectedLocale") == "en")
            TMP.text = txtTMP + PlayerPrefs.GetFloat(namePref).ToString("F2");
        else
            TMP.text = txtTMPuk + PlayerPrefs.GetFloat(namePref).ToString("F2");
        PlayerPrefs.SetFloat(namePref, slider.value);
    }
}

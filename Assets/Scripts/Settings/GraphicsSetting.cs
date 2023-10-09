using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GraphicsSetting : MonoBehaviour
{
    public static GraphicsSetting Instance { get; private set; }
    public TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        // Отримати збережене значення з PlayerPrefs
        LanguagrSetting();
    }
    public void LanguagrSetting()
    {
        int graphicsValue = PlayerPrefs.GetInt("Graphics", 0); // За замовчуванням 0
        if (PlayerPrefs.GetString("SelectedLocale") == "uk")
        {
            List<TMP_Dropdown.OptionData> options = graphicsDropdown.options;
            options[0].text = "НИЗЬКА";
            options[1].text = "СЕРЕДНЯ";
            options[2].text = "ВИСОКА";
            graphicsDropdown.options = options;
        }
        // Встановити значення в Dropdown
        graphicsDropdown.value = graphicsValue;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Graphics", qualityIndex);
        Debug.Log(qualityIndex);
    }
}

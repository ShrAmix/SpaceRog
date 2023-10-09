using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;

public class TakeDifficult : MonoBehaviour
{
    public GameObject menuScore;
    public static GraphicsSetting Instance { get; private set; }
    public TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        // Отримати збережене значення з PlayerPrefs
        LanguagrSetting();
    }
    public void LanguagrSetting()
    {
        int graphicsValue = PlayerPrefs.GetInt("Difficult", 1); // За замовчуванням 0
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
        PlayerPrefs.SetInt("Difficult", qualityIndex);
        menuScore.GetComponent<TextMeshProUGUI>().text= ""+ PlayerPrefs.GetInt("BestScore" + $"{PlayerPrefs.GetInt("Difficult", 1)}");
    }
}

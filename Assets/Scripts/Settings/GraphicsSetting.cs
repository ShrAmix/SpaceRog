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
        // �������� ��������� �������� � PlayerPrefs
        LanguagrSetting();
    }
    public void LanguagrSetting()
    {
        int graphicsValue = PlayerPrefs.GetInt("Graphics", 0); // �� ������������� 0
        if (PlayerPrefs.GetString("SelectedLocale") == "uk")
        {
            List<TMP_Dropdown.OptionData> options = graphicsDropdown.options;
            options[0].text = "������";
            options[1].text = "�������";
            options[2].text = "������";
            graphicsDropdown.options = options;
        }
        // ���������� �������� � Dropdown
        graphicsDropdown.value = graphicsValue;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Graphics", qualityIndex);
        Debug.Log(qualityIndex);
    }
}

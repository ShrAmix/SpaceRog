using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Weight : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        // �������� �������� ������� � PlayerPrefs (���� ���� ��������, ������������ 0 �� �������������)
        int graphicsValue = PlayerPrefs.GetInt("SelectedDefender", 0);

        // ����������, �� ������� ���� "���������"
        if (PlayerPrefs.GetString("SelectedLocale") == "uk")
        {
            // ������� ������ ��� ����������� ������ �� ��������
            List<TMP_Dropdown.OptionData> options = dropdown.options;
            options[0].text = "������";
            options[1].text = "�������";
            options[2].text = "������";
            dropdown.options = options;
        }
        else // ���� ���� ���� (������� "���������")
        {
            // ������� ������ ��� ����������� ������ �� ��������
            List<TMP_Dropdown.OptionData> options = dropdown.options;
            options[0].text = "LOW";
            options[1].text = "MEDIUM";
            options[2].text = "HIGH";
            dropdown.options = options;
        }

        // ������������ �������� ����������� ������ �� ������ ����������� �������� �������
        dropdown.value = graphicsValue;
    }

    // �������, ��� ����������� ��� ��� ����������� ������
    public void LocaleSelected(int index)
    {
        // �������� ������ �������� ������� � PlayerPrefs
        PlayerPrefs.SetInt("SelectedDefender", index);

        // �������� �������� 䳿 � ��������� �� �������� �������� �������
        switch (index)
        {
            case 0: // ������� "������"
                PlayerPrefs.SetFloat("DeafH", -1.7f);
                PlayerPrefs.SetFloat("DeafV", -1.2f);
                PlayerPrefs.SetFloat("DeafG", -0.5f);
                break;
            case 1: // ������� "�������"
                PlayerPrefs.SetFloat("DeafH", 0f);
                PlayerPrefs.SetFloat("DeafV", 0f);
                PlayerPrefs.SetFloat("DeafG", 0f);
                break;
            case 2: // ������� "������"
                PlayerPrefs.SetFloat("DeafH", 1.8f);
                PlayerPrefs.SetFloat("DeafV", 1.3f);
                PlayerPrefs.SetFloat("DeafG", 0.4f);
                break;
        }
    }
}

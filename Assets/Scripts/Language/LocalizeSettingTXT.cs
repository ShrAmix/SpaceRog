using UnityEngine;
using TMPro;

public class LocalizeSettingTXT : MonoBehaviour
{
    [SerializeField] private string englishLocal;
    [SerializeField] private string ukraineLocal;

    private void Start()
    {
        // �������� ��������� �������� ����
        string selectedLocale = PlayerPrefs.GetString("SelectedLocale","en");

        // �������� ��������� ����� � ��������� �� ������ ����
        string selectedText = (selectedLocale == "en") ? englishLocal : ukraineLocal;

        // �������� ��������� TextMeshProUGUI (��� TMP_Text) �� ����� � ��'���
        TextMeshProUGUI textField = GetComponent<TextMeshProUGUI>();

        // ����������, �� �� ������� ���������, � ������������ �����
        if (textField != null)
        {
            textField.text = selectedText;
        }
        else
        {
            Debug.LogWarning("LocalizedTextManual script is attached to an object without TextMeshProUGUI component.");
        }
    }
}

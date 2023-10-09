using UnityEngine;
using TMPro;

public class LocalizeSettingTXT : MonoBehaviour
{
    [SerializeField] private string englishLocal;
    [SerializeField] private string ukraineLocal;

    private void Start()
    {
        // Отримуємо збережене значення мови
        string selectedLocale = PlayerPrefs.GetString("SelectedLocale","en");

        // Вибираємо відповідний текст в залежності від обраної мови
        string selectedText = (selectedLocale == "en") ? englishLocal : ukraineLocal;

        // Отримуємо компонент TextMeshProUGUI (або TMP_Text) на цьому ж об'єкті
        TextMeshProUGUI textField = GetComponent<TextMeshProUGUI>();

        // Перевіряємо, чи ми знайшли компонент, і встановлюємо текст
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

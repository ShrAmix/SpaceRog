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
        // Отримуємо значення графіки з PlayerPrefs (якщо немає значення, встановлюємо 0 за замовчуванням)
        int graphicsValue = PlayerPrefs.GetInt("SelectedDefender", 0);

        // Перевіряємо, чи вибрана мова "українська"
        if (PlayerPrefs.GetString("SelectedLocale") == "uk")
        {
            // Змінюємо тексти для випадаючого списку на українські
            List<TMP_Dropdown.OptionData> options = dropdown.options;
            options[0].text = "НИЗЬКА";
            options[1].text = "СЕРЕДНЯ";
            options[2].text = "ВИСОКА";
            dropdown.options = options;
        }
        else // Якщо інша мова (зокрема "англійська")
        {
            // Змінюємо тексти для випадаючого списку на англійські
            List<TMP_Dropdown.OptionData> options = dropdown.options;
            options[0].text = "LOW";
            options[1].text = "MEDIUM";
            options[2].text = "HIGH";
            dropdown.options = options;
        }

        // Встановлюємо значення випадаючого списку на підставі збереженого значення графіки
        dropdown.value = graphicsValue;
    }

    // Функція, яка викликається при зміні випадаючого списку
    public void LocaleSelected(int index)
    {
        // Зберігаємо обране значення графіки в PlayerPrefs
        PlayerPrefs.SetInt("SelectedDefender", index);

        // Виконуємо додаткові дії в залежності від обраного значення графіки
        switch (index)
        {
            case 0: // Вибрано "НИЗЬКА"
                PlayerPrefs.SetFloat("DeafH", -1.7f);
                PlayerPrefs.SetFloat("DeafV", -1.2f);
                PlayerPrefs.SetFloat("DeafG", -0.5f);
                break;
            case 1: // Вибрано "СЕРЕДНЯ"
                PlayerPrefs.SetFloat("DeafH", 0f);
                PlayerPrefs.SetFloat("DeafV", 0f);
                PlayerPrefs.SetFloat("DeafG", 0f);
                break;
            case 2: // Вибрано "ВИСОКА"
                PlayerPrefs.SetFloat("DeafH", 1.8f);
                PlayerPrefs.SetFloat("DeafV", 1.3f);
                PlayerPrefs.SetFloat("DeafG", 0.4f);
                break;
        }
    }
}

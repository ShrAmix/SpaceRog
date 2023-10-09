using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class SettingLanguage : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Generate list of available Locales
        List<TMP_Dropdown.OptionData> options = dropdown.options;
        int selected = 0;

        // Load saved language selection from PlayerPrefs
        string savedLocaleCode = PlayerPrefs.GetString("SelectedLocale");
        Locale savedLocale = null;

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;

            //string displayName = locale.Identifier.CultureInfo.DisplayName; // Отримати назву локалізації без (en) або (uk)
            

            if (locale.Identifier.Code == savedLocaleCode)
            {
                savedLocale = locale;
            }
        }

        dropdown.options = options; // Встановлюємо опції для випадаючого списку

        if (savedLocale != null)
        {
            // If a saved locale was found, select it
            LocalizationSettings.SelectedLocale = savedLocale;
            dropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(savedLocale); // Встановлюємо індекс випадаючого списку
        }
        else
        {
            // If no saved locale was found, select the default locale
            dropdown.value = selected;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[selected];
        }

        dropdown.onValueChanged.AddListener(LocaleSelected);
    }

    void LocaleSelected(int index)
    {
        //Debug.Log("LocaleSelected method called.");
        if (LocalizationSettings.SelectedLocale != LocalizationSettings.AvailableLocales.Locales[index])
        {
            // Save the selected locale code to PlayerPrefs
            string localeCode = LocalizationSettings.AvailableLocales.Locales[index].Identifier.Code;
            PlayerPrefs.SetString("SelectedLocale", localeCode);
            Debug.Log(PlayerPrefs.GetString("SelectedLocale"));

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
            GameManager.instance.LoadScene(6);
        }
        else
        {
            // If the selected locale is the same, just reload the scene
            GameManager.instance.LoadScene(6);
           // Debug.Log("resetScene");
        }
    }
    public void ToggleOn()
    {
        GameManager.instance.LoadScene(6);
    }
}

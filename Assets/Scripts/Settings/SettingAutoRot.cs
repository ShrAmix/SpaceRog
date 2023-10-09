using UnityEngine;
using UnityEngine.UI;

public class SettingAutoRot : MonoBehaviour
{
    public Button targetButton; // Посилання на кнопку, колір якої ми будемо змінювати
    public Color defaultColor; // Дефолтний колір кнопки
    public Color changedColor; // Колір, коли SettingAutoRotate == 0

    private void Start()
    {
        // Отримати значення SettingAutoRotate з PlayerPrefs
        int settingAutoRotate = PlayerPrefs.GetInt("SettingAutoRotate", 1);

        // Встановити колір кнопки залежно від значення SettingAutoRotate
        if (settingAutoRotate == 0)
        {
            targetButton.image.color = changedColor;
        }
        else
        {
            targetButton.image.color = defaultColor;
        }
    }

    public void ToggleButtonColor()
    {
        // Отримати поточне значення SettingAutoRotate з PlayerPrefs
        int settingAutoRotate = PlayerPrefs.GetInt("SettingAutoRotate", 1);

        // Переключити значення SettingAutoRotate
        settingAutoRotate = (settingAutoRotate == 0) ? 1 : 0;

        // Зберегти оновлене значення SettingAutoRotate у PlayerPrefs
        PlayerPrefs.SetInt("SettingAutoRotate", settingAutoRotate);

        // Встановити колір кнопки залежно від нового значення SettingAutoRotate
        if (settingAutoRotate == 0)
        {
            targetButton.image.color = changedColor;
        }
        else
        {
            targetButton.image.color = defaultColor;
        }
    }
}

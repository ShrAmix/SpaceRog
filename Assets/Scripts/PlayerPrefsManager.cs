using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string PrefsInitializedKey = "PrefsInitialized";

    // Функция для установки стандартных значений PlayerPrefs
    public static void SetDefaultPlayerPrefs()
    {
        // Проверяем, были ли уже установлены стандартные значения
        if (!PlayerPrefs.HasKey(PrefsInitializedKey))
        {
            // Устанавливаем стандартные значения


            PlayerPrefs.SetInt("BestScore", 0);
            PlayerPrefs.SetInt("Money", 0);

            PlayerPrefs.SetFloat("0SpeedHorizontal", 8f);
            PlayerPrefs.SetFloat("0SpeedVertical", 6f);
            PlayerPrefs.SetFloat("0rotationSpeed", 5f);
            PlayerPrefs.SetFloat("0maxHealth", 1f);

            PlayerPrefs.SetFloat("1SpeedHorizontal", 10f);
            PlayerPrefs.SetFloat("1SpeedVertical", 8f);
            PlayerPrefs.SetFloat("1rotationSpeed", 6f);
            PlayerPrefs.SetFloat("1maxHealth", 2f);

            PlayerPrefs.SetFloat("2SpeedHorizontal", 12f);
            PlayerPrefs.SetFloat("2SpeedVertical", 9f);
            PlayerPrefs.SetFloat("2rotationSpeed", 7f);
            PlayerPrefs.SetFloat("2maxHealth", 2f);

            PlayerPrefs.SetFloat("3SpeedHorizontal", 14f);
            PlayerPrefs.SetFloat("3SpeedVertical", 11f);
            PlayerPrefs.SetFloat("3rotationSpeed", 9f);
            PlayerPrefs.SetFloat("3maxHealth", 2f);

            PlayerPrefs.SetFloat("SpeedHorizontal", 8f);
            PlayerPrefs.SetFloat("SpeedVertical", 6f);
            PlayerPrefs.SetFloat("rotationSpeed", 5f);
            PlayerPrefs.SetFloat("maxHealth", 1f);

            // Помечаем, что стандартные значения уже были установлены
            PlayerPrefs.SetInt(PrefsInitializedKey, 1);

            // Сохраняем изменения
            PlayerPrefs.Save();
        }
    }
}

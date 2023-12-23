using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sorting : MonoBehaviour
{
    public GameObject[] playerInfo;

    private void Start()
    {
        //SortPlayerInfo();
    }

    public void OnSortButtonClick(string what)
    {
        SortPlayerInfo(what);
    }

    private void SortPlayerInfo(string name)
    {
        // Створюємо масив Tuple, де кожен елемент - пара playerInfo і TextMeshProUGUI
        List<Tuple<GameObject, TextMeshProUGUI>> playerTextPairs = new List<Tuple<GameObject, TextMeshProUGUI>>();

        // Заповнюємо масив Tuple
        foreach (GameObject playerObject in playerInfo)
        {
            // Отримуємо TextMeshProUGUI з дочірнього об'єкта Score0
            TextMeshProUGUI textMeshPro = playerObject.transform.Find(name)?.GetComponent<TextMeshProUGUI>();

            // Перевіряємо, чи textMeshPro існує
            if (textMeshPro != null)
            {
                // Додаємо пару playerInfo і TextMeshProUGUI до масиву
                playerTextPairs.Add(new Tuple<GameObject, TextMeshProUGUI>(playerObject, textMeshPro));
            }
            else
            {
                Debug.LogError("TextMeshProUGUI не знайдено в Score0 для playerObject");
            }
        }

        // Сортуємо Tuple за рахунком в TextMeshPro у порядку спадання
        playerTextPairs.Sort((a, b) => GetScore(b.Item2).CompareTo(GetScore(a.Item2)));

        // Оновлюємо порядок об'єктів в VerticalLayoutGroup
        VerticalLayoutGroup verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        for (int i = 0; i < playerTextPairs.Count; i++)
        {
            // Отримуємо Number з дочірнього об'єкта Score0
            TextMeshProUGUI numberText = playerTextPairs[i].Item1.transform.Find("Number")?.GetComponent<TextMeshProUGUI>();

            // Перевіряємо, чи numberText існує
            if (numberText != null)
            {
                // Змінюємо порядок об'єктів у батьківському Transform
                playerTextPairs[i].Item1.transform.SetSiblingIndex(i);

                // Оновлюємо значення Number
                numberText.text = (i + 1).ToString(); // Починаємо нумерацію з 1
            }
            else
            {
                Debug.LogError("TextMeshProUGUI не знайдено в Number для playerObject");
            }
        }
    }



    // Метод для отримання рахунку з об'єкта за допомогою компоненту TextMeshPro
    private int GetScore(TextMeshProUGUI textMeshPro)
    {
        if (textMeshPro != null)
        {
            // Парсимо текст в ціле число
            if (int.TryParse(textMeshPro.text, out int score))
            {
                return score;
            }
        }

        // Якщо не вдалося отримати рахунок, повертаємо 0 або інше значення за замовчуванням
        return 0;
    }
}

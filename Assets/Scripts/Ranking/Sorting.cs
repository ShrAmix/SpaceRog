using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sorting : MonoBehaviour
{
    public GameObject[] playerInfo;
    public TMP_InputField searchInput;
    private void Start()
    {
        //SortPlayerInfo();
    }

    public void OnSortButtonClick(string what)
    {
        SortPlayerInfo(what);
    }
    public void OnSearchInputEndEdit()
    {
        string searchQuery=searchInput.text;
        SearchAndDisplayResults(searchQuery);
    }

    private void SearchAndDisplayResults(string searchQuery)
    {
        // Якщо введено пустий рядок, вмикаємо всі об'єкти і виходимо
        if (string.IsNullOrEmpty(searchQuery))
        {
            EnableAllObjects();
            return;
        }

        foreach (GameObject playerObject in playerInfo)
        {
            // Отримуємо компоненти TextMeshProUGUI з дочірніх об'єктів Score0, Score1, Score, Name, Money
            List<TextMeshProUGUI> textComponents = new List<TextMeshProUGUI>();
            textComponents.Add(playerObject.transform.Find("Score0")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Score1")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Score")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Name")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Money")?.GetComponent<TextMeshProUGUI>());

            // Перевіряємо, чи в playerObject є хоча б один TextMeshProUGUI, який містить searchQuery
            bool containsSearchQuery = textComponents.Exists(component => component != null && component.text.Contains(searchQuery));

            // Вмикаємо або вимикаємо об'єкт відповідно до результату пошуку
            playerObject.SetActive(containsSearchQuery);
        }
    }

    // Метод для вмикання всіх об'єктів
    private void EnableAllObjects()
    {
        foreach (GameObject playerObject in playerInfo)
        {
            playerObject.SetActive(true);
        }
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

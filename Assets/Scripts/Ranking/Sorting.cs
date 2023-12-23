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
        // ���� ������� ������ �����, ������� �� ��'���� � ��������
        if (string.IsNullOrEmpty(searchQuery))
        {
            EnableAllObjects();
            return;
        }

        foreach (GameObject playerObject in playerInfo)
        {
            // �������� ���������� TextMeshProUGUI � ������� ��'���� Score0, Score1, Score, Name, Money
            List<TextMeshProUGUI> textComponents = new List<TextMeshProUGUI>();
            textComponents.Add(playerObject.transform.Find("Score0")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Score1")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Score")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Name")?.GetComponent<TextMeshProUGUI>());
            textComponents.Add(playerObject.transform.Find("Money")?.GetComponent<TextMeshProUGUI>());

            // ����������, �� � playerObject � ���� � ���� TextMeshProUGUI, ���� ������ searchQuery
            bool containsSearchQuery = textComponents.Exists(component => component != null && component.text.Contains(searchQuery));

            // ������� ��� �������� ��'��� �������� �� ���������� ������
            playerObject.SetActive(containsSearchQuery);
        }
    }

    // ����� ��� �������� ��� ��'����
    private void EnableAllObjects()
    {
        foreach (GameObject playerObject in playerInfo)
        {
            playerObject.SetActive(true);
        }
    }
    private void SortPlayerInfo(string name)
    {
        // ��������� ����� Tuple, �� ����� ������� - ���� playerInfo � TextMeshProUGUI
        List<Tuple<GameObject, TextMeshProUGUI>> playerTextPairs = new List<Tuple<GameObject, TextMeshProUGUI>>();

        // ���������� ����� Tuple
        foreach (GameObject playerObject in playerInfo)
        {
            // �������� TextMeshProUGUI � ���������� ��'���� Score0
            TextMeshProUGUI textMeshPro = playerObject.transform.Find(name)?.GetComponent<TextMeshProUGUI>();

            // ����������, �� textMeshPro ����
            if (textMeshPro != null)
            {
                // ������ ���� playerInfo � TextMeshProUGUI �� ������
                playerTextPairs.Add(new Tuple<GameObject, TextMeshProUGUI>(playerObject, textMeshPro));
            }
            else
            {
                Debug.LogError("TextMeshProUGUI �� �������� � Score0 ��� playerObject");
            }
        }

        // ������� Tuple �� �������� � TextMeshPro � ������� ��������
        playerTextPairs.Sort((a, b) => GetScore(b.Item2).CompareTo(GetScore(a.Item2)));

        // ��������� ������� ��'���� � VerticalLayoutGroup
        VerticalLayoutGroup verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        for (int i = 0; i < playerTextPairs.Count; i++)
        {
            // �������� Number � ���������� ��'���� Score0
            TextMeshProUGUI numberText = playerTextPairs[i].Item1.transform.Find("Number")?.GetComponent<TextMeshProUGUI>();

            // ����������, �� numberText ����
            if (numberText != null)
            {
                // ������� ������� ��'���� � ������������ Transform
                playerTextPairs[i].Item1.transform.SetSiblingIndex(i);

                // ��������� �������� Number
                numberText.text = (i + 1).ToString(); // �������� ��������� � 1
            }
            else
            {
                Debug.LogError("TextMeshProUGUI �� �������� � Number ��� playerObject");
            }
        }
    }



    // ����� ��� ��������� ������� � ��'���� �� ��������� ���������� TextMeshPro
    private int GetScore(TextMeshProUGUI textMeshPro)
    {
        if (textMeshPro != null)
        {
            // ������� ����� � ���� �����
            if (int.TryParse(textMeshPro.text, out int score))
            {
                return score;
            }
        }

        // ���� �� ������� �������� �������, ��������� 0 ��� ���� �������� �� �������������
        return 0;
    }
}

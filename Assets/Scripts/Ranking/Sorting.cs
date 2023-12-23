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

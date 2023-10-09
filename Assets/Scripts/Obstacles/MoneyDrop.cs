using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // ������ �������

    private void Start()
    {
        SpawnCoin(); // �������� ������� ��� ����� �������
    }

    private void SpawnCoin()
    {
        // �������� �������� ��������� ��� ��������� �������
        float randomOffsetX = Random.Range(-0.2f, 0.2f);
        float randomOffsetY = Random.Range(-0.2f, 0.2f);

        // �������� ������� � ��������� �� ����������� enemy
        Vector3 spawnPosition = new Vector3(transform.position.x + randomOffsetX, transform.position.y + randomOffsetY, transform.position.z);
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        // �������� ��������� Animator � �������
        Animator coinAnimator = coin.GetComponent<Animator>();
        if (coinAnimator != null)
        {ScoreManager.Instance.SetMoney(1);
            coinAnimator.Play("CoinAnimation", 0, 0); // ³��������� ������� ������� � �������
            coinAnimator.speed = 1f; // ������������ �������� ���������� ������� 1, ��� ���� ����������� ���� ���
            
            Destroy(coin, coinAnimator.GetCurrentAnimatorStateInfo(0).length); // ��������� ������� ���� ��������� �������
        }
    }
}

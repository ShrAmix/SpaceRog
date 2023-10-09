using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Префаб монетки

    private void Start()
    {
        SpawnCoin(); // Спавнимо монетку при старті скрипту
    }

    private void SpawnCoin()
    {
        // Генеруємо випадкові погрішності для координат монетки
        float randomOffsetX = Random.Range(-0.2f, 0.2f);
        float randomOffsetY = Random.Range(-0.2f, 0.2f);

        // Спавнимо монетку з погрішністю на координатах enemy
        Vector3 spawnPosition = new Vector3(transform.position.x + randomOffsetX, transform.position.y + randomOffsetY, transform.position.z);
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        // Отримуємо компонент Animator з монетки
        Animator coinAnimator = coin.GetComponent<Animator>();
        if (coinAnimator != null)
        {ScoreManager.Instance.SetMoney(1);
            coinAnimator.Play("CoinAnimation", 0, 0); // Відтворюємо анімацію монетки з початку
            coinAnimator.speed = 1f; // Встановлюємо швидкість відтворення анімації 1, щоб вона відтворилася один раз
            
            Destroy(coin, coinAnimator.GetCurrentAnimatorStateInfo(0).length); // Видаляємо монетку після закінчення анімації
        }
    }
}

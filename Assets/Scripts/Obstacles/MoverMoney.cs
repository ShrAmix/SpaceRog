using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMoney : MonoBehaviour
{
    public float Speed = 3;
    public GameObject Collider;
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
        if (Collider.transform.position.x < -20)
        {
            Destroy(Collider.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerFly>(out _))
        {
            ScoreManager.Instance.SetMoney(1);
            Destroy(Collider.gameObject);
        }
    }
}

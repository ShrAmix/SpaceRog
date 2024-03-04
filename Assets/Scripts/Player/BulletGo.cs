using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletGo : NetworkBehaviour
{
    [SerializeField] private float Speed = 30;
    [SerializeField] private GameObject ColliderB;
    [SerializeField] private bool boolPlayer = false;
   
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Speed* Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ƒл€ гравц€
        if (boolPlayer) 
        { 
            if (other.gameObject.CompareTag("PipePart") || other.gameObject.CompareTag("Enemy"))
            {
               Destroy(ColliderB.gameObject);
            }
        }

        //ƒл€ противник≥в
        if (!boolPlayer)
        {
            if (other.gameObject.CompareTag("Bullet")  || other.gameObject.CompareTag("PipePart") || other.gameObject.CompareTag("Player"))
            {
                Destroy(ColliderB.gameObject);
            }
        }

        //ƒл€ вс≥х
        if(other.gameObject.CompareTag("Polia"))
        {
            Destroy(ColliderB.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //ƒл€ гравц€
        if (boolPlayer)
        {
            if (other.gameObject.CompareTag("PipePart") || other.gameObject.CompareTag("Enemy"))
            {
                Destroy(ColliderB.gameObject);
            }
        }

        //ƒл€ противник≥в
        if (!boolPlayer)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(ColliderB.gameObject);
            }
        }
        //ƒл€ вс≥х
        if (other.gameObject.CompareTag("Polia"))
        {
            Destroy(ColliderB.gameObject);
        }
    }

}

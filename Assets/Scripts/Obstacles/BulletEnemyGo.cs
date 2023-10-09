using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyGo : MonoBehaviour
{
    public float Speed = 30;
    public GameObject ColliderB;
    void FixedUpdate()
    {
       transform.Translate(Vector2.right * Speed* Time.deltaTime);
        double X = ColliderB.transform.position.x, Y= ColliderB.transform.position.y;
        if ( X> 20 || X < -20 || Y > 20 || Y < -20)
        {
            Destroy(ColliderB.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("PipePart"))
        {
            Destroy(ColliderB.gameObject);
        }
    }
}

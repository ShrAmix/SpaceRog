using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGo : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndGame()
    {
        Vector3 newPosition = new Vector3(-200f, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }

   
}

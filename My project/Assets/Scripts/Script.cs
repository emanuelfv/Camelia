using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public float speed;
    public Vector3 initialPosition, finalPosition;
    
    void Update()
    {
        if (transform.position.x != finalPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
        }
    }
}

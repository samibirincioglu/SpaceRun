using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float turnSpeed = 90f;

    private void Update()
    {
           transform.Rotate(transform.position, turnSpeed);
    }
}

  

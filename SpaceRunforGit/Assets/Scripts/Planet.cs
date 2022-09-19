using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float turnSpeed = 15f;

    private void Start()
    {
        Invoke("Destroy", 38);
    }

    void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }


     void Destroy()
    {
        Destroy(gameObject);
    }
}

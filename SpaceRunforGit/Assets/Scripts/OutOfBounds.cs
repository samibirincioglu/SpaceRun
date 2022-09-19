using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public PlayerMove playermove;

    private void OnTriggerExit(Collider other)
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playermove = g.GetComponent<PlayerMove>();
        playermove.isDead = true;

    }
}

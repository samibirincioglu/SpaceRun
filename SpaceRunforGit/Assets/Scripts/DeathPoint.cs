using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoint : MonoBehaviour
{
    public PlayerMove playermove;
    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playermove = g.GetComponent<PlayerMove>();

    }
    private void OnTriggerEnter(Collider collider)
    {
        //karakter oyun alan�n�n d���na ��kt� m�?
        Debug.Log("Triggered");
            playermove.isDead = true;
          
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleWall : MonoBehaviour
{
    public PlayerMove playermove;
    public bool isRight;
    void Start()
    {
        //Playermove scriptindeki deðiþkenlerin direkt kullanýmýna ihtiyacým olduðundan
        //bu þekilde bi referans oluþturuyorum
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playermove = g.GetComponent<PlayerMove>();
    }
    public void FindPlayer()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playermove = g.GetComponent<PlayerMove>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        FindPlayer();
            //isRight bu fonksiyonda kullandýðým 
            //onRight playermovedaki 
            if (other.gameObject.tag == "Player")
            {
                if (playermove.onRight)
                {
                    isRight = true;
                  
                }
                else if (!playermove.onRight)
                {
                    isRight = false;
                }
            }
    }

    private void OnTriggerExit(Collider other)
    {
           // duvar ve karakter kesiþimden çýktýðýnda karakter ayný yöne gidiyorsa ölsün
           if (other.gameObject.tag == "Player")
            {
                if (playermove.onRight != isRight)
                {
                    return;
                }
                else if (playermove.onRight == isRight)
                {
                    playermove.isDead = true;
                }

            }
    }
}

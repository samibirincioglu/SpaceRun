using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleWall : MonoBehaviour
{
    public PlayerMove playermove;
    public bool isRight;
    void Start()
    {
        //Playermove scriptindeki de�i�kenlerin direkt kullan�m�na ihtiyac�m oldu�undan
        //bu �ekilde bi referans olu�turuyorum
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
            //isRight bu fonksiyonda kulland���m 
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
           // duvar ve karakter kesi�imden ��kt���nda karakter ayn� y�ne gidiyorsa �ls�n
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

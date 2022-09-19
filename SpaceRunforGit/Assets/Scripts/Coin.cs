using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool didCollide=false;
    public float turnSpeed = 90f;
    private void OnTriggerEnter(Collider other)
    {
        if (didCollide)
            return;
        //collide player mi? playersa sil
        if (other.gameObject.tag == "Player")
        {
            didCollide = true;
            //money ekle
            GameManager.inst.IncrementMoney();
            //coini sil 
            Destroy(gameObject);
        }
        else 
        { 
            return;
        }
    }

    //coinin olduðu yerde dönmesi
    void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime,0 );
    }

}

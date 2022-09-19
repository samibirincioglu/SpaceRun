using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnımatıonController : MonoBehaviour
{
    public PlayerMove player;
    private Animator animator;
    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g.GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameStarted)
            animator.SetTrigger("started");

        if (animator != null)
        {

            if (player.isCollideWall == true)
            {
                animator.SetBool("isCollideWall", true);

                if (player.onRight)
                {
                    animator.SetBool("isRight", true);
                }
                else if (!player.onRight)
                {
                    animator.SetBool("isRight", false);
                }
            }
            else
            {
                animator.SetBool("isCollideWall", false);
            }
        }
    }
}

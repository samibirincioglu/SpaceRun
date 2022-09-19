using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCamera : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void GameStarted()
    {
        animator.SetTrigger("Gamestarted");
    }
}
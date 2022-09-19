using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    GameObject player;
    public GameObject startMenu;
    public bool gameStarted;
    [SerializeField] GameObject Camera;

    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g;
        startMenu.SetActive(true);
        gameStarted = false;
        player.GetComponent<Score>().enabled = false;
        player.GetComponent<PlayerMove>().enabled = false;
            
    }
    public void FindPlayer()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g;
    }

    public void StartGame()
    {
        Invoke("StartPlayerMove", 1.2f);
    }
    public void StartPlayerMove()
    {
        Camera.GetComponent<CameraControl>().enabled = true; 
        //start
        gameStarted = true;

        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<Score>().enabled = true;
    }


}

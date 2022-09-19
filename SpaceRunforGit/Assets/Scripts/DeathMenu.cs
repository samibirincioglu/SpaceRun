using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    Score score;
    public DeathMenu deathMenu;
    public GameObject inGameObjects;
    GameObject player;
    public int highScore;
    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        score = g.GetComponent<Score>();
        player = g;
        //ekraný temizle karakter hareketini durdur
        inGameObjects.SetActive(false);
        player.GetComponent<PlayerMove>().enabled = false;

        //score scriptinden referans al

        if (score.deathScore > highScore)
        {
            highScore = score.deathScore;
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{

    public float score = 0;
    public Text highScoreText;
    public Text scoreText;
    public Text DeathScoreText;
    public int deathScore;
    private float difficulty = 1;
    private float maxDifficulty = 25;
    private float nextLevelScore = 10;
    private float speedFactor = 1;
    public int highScore;

    private void Start()
    {
        //kaydedilmis verilerden high score i cikar ve ata
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        else if(!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
    }

    void Update()
    {
        //karakter olduyse olum skorunu kaydet ve scoru durdur
        if (gameObject.GetComponent<PlayerMove>().isDead == true)
        {
            deathScore = ((int)score);

            //skoru high score  kontrolu icin HighScore a gonder
            HighScore(deathScore);

            //skoru ekrana yazdir
            DeathScoreText.text = ((int)score).ToString();
            return;
        }

        if (score >= nextLevelScore)
            LevelUp();
        //skoru saniyede 1 arttýr 
        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    //zorluk
    void LevelUp()
    {
        

        //15 seviyeli zorluk
        if (difficulty == maxDifficulty)
            return;

        difficulty = difficulty + 1;

        // bir dahaki zorluk için gerekli skoru 2 ile çarp (10-20-40-80-160)
        if (nextLevelScore > 100)
        {
            nextLevelScore *= 1.5f;
        }
        else
        {
             nextLevelScore *= 2f;
        }
        //speedFactor floati ile plavermovadaki setspeed fonksiyonuna git
        GetComponent<PlayerMove>().SetSpeed(speedFactor);
        Debug.Log("zorluk: " + difficulty);
    }

    void HighScore(int score)
    {
 
        //gelen scoreu high scorela kiyasla ve daha yuksekse highscore olarak tanimla
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
        }
        else
        {
            //degilse ekrana bir onceki high'i yazdir.
            highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
        }
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    int money;
    int playerTotalMoney;
    //d��ar�dan ula�mak i�in statik bir referans olu�tur
    public static GameManager inst;
    Score score;
    GameObject[] emptyArray;
    
    private void Awake()
    {
 

        //script �al��t���nda statik s�n�f� kendisine ata
        if (inst == null)
        {
            inst = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(this);
        }
        else
        {
             Destroy(gameObject);
        }
    }
    

    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        score = g.GetComponent<Score>();

       // playerTotalMoney = 0;
        PlayerPrefs.SetInt("money", playerTotalMoney);
        playerTotalMoney = PlayerPrefs.GetInt("money");
    }
    public void DestroyGroundTiles()
    {
        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("GroundTile");
        Debug.Log(groundTiles.Length);
        for (int i = 0; i < groundTiles.Length; i++)
        {
            Destroy(groundTiles[i]);    
        }

        groundTiles = emptyArray;
    }
    public void playerOnDead()
    {
        playerTotalMoney += money;
        PlayerPrefs.SetInt("money", playerTotalMoney);
        playerTotalMoney = PlayerPrefs.GetInt("money");
    }

    //para artt�rma fonksiyonu
    public void IncrementMoney()
    {
        money++;
        score.score++; 
    }

    public void GetScoreScript()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        score = g.GetComponent<Score>();    
    }

    public bool IsMoneyEnough(int amount)
    {
        return (playerTotalMoney >= amount);   
    }

    public void SuccesfullBuy(int amount)
    {
        playerTotalMoney -= amount;
        PlayerPrefs.SetInt("money", playerTotalMoney);

    }

    public void EquipItem()
    {

    }

}

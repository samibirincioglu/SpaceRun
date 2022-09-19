using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketMenu : MonoBehaviour
{
    public Text moneyText;

    public void whenMenuActive()
    {
        moneyText.text = PlayerPrefs.GetInt("money").ToString();

    }
  
}

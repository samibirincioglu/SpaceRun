using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    [SerializeField] Transform playerHolder;


    private void Start()
    {

        int itemIndex = PlayerPrefs.GetInt("CItemSelected");

        playerHolder.GetChild(itemIndex).gameObject.SetActive(true);
    }
}

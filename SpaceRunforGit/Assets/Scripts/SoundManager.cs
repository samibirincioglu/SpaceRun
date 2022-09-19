using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false ;

    void Start()
    {
        // HasKey komutu playerprefs de muted diye bir anahtar varsa true döndürür
        if (!PlayerPrefs.HasKey("muted"))
        {
         
            //muted i false yap
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }

        SoundIcon();
        AudioListener.pause = muted;
    }
     public void OnButtonPress()
    {
        if(muted == false)
        {
            muted = true; 
            AudioListener.pause = true;

        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        SoundIcon();
        Save();
    }
    public void SoundIcon()
    {
        if(muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }
    
    private void Save()
    {
        //playerprefse kaydediyorum
        //muted true ise 1 false ise 0
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    private void Load()
    {
        //playerprefsten aliyorum
        //eger 'muted' 1 ise muted = true degilse false
        muted = PlayerPrefs.GetInt("muted") == 1 ;
    }

}

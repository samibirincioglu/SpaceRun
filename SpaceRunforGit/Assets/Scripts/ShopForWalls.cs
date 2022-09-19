using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopForWalls : MonoBehaviour
{
    public MarketMenu marketMenu;
    [System.Serializable]
    class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
        public Sprite EquippedImage;
    }

    [SerializeField] List<ShopItem> ShopItemsList;

    [SerializeField] GameObject GPGSManager;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] GameObject groundSpawner;
    Button buyButton;
    Button selectButton;
    private void Awake()
    {
        //OYUNCU ÝLERLEMESÝNÝ SIFIRLAMA

        /* int lengthh = ShopItemsList.Count;
          for (int i = 0; i < lengthh; i++)
          {
               PlayerPrefs.DeleteKey("WItemIndexIsBought_" + i);
          }
          PlayerPrefs.DeleteKey("WItemSelected");
          PlayerPrefs.DeleteKey("WnewSelectedItemIndex");
          PlayerPrefs.DeleteKey("WpreviousSelectedItemIndex");
        */


        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        int length = ShopItemsList.Count;
        for (int i = 1; i < length; i++)
        {
            //Satýn alýnmýþ veya alýnmamýþ shop itemlarýný oluþturma
            if (PlayerPrefs.HasKey("WItemIndexIsBought_" + i) && PlayerPrefs.GetInt("WItemIndexIsBought_" + i) == 1)
            {

                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
                g.transform.GetChild(1).gameObject.SetActive(false);
                g.transform.GetChild(3).gameObject.SetActive(false);

                buyButton = g.transform.GetChild(2).GetComponent<Button>();
                buyButton.transform.GetChild(0).GetComponent<Text>().text = "BOUGHT";
                buyButton.interactable = false;

                SelectButtonActivate(i);
                selectButton = g.transform.GetChild(4).GetComponent<Button>();
                selectButton.AddEventListener(i, OnSelectItemButtonClick);
            }
            else
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
                g.transform.GetChild(1).gameObject.SetActive(true);
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
                g.transform.GetChild(3).gameObject.SetActive(false);

                buyButton = g.transform.GetChild(2).GetComponent<Button>();
                buyButton.transform.GetChild(0).GetComponent<Text>().text = "BUY";
                buyButton.interactable = true;
                buyButton.AddEventListener(i, OnShopItemButtonClicked);

                selectButton = g.transform.GetChild(4).GetComponent<Button>();
                selectButton.AddEventListener(i, OnSelectItemButtonClick);
                selectButton.interactable = false;
                SelectButtonOff(i);
            }
        }
    }

    void Start()
    {
        Load();

    }
    private void Load()
    {

        if (!PlayerPrefs.HasKey("WItemSelected"))
            PlayerPrefs.SetInt("WItemSelected", 0);

        ItemEquippedImageON(PlayerPrefs.GetInt("WItemSelected"));
    }

    public void LoadEquippedSkin(int itemIndex)
    {
        PlayerPrefs.SetInt("WItemSelected", itemIndex);
        //Equipped image aktif
        GameObject equippedImage = ShopScrollView.GetChild(itemIndex).GetChild(3).gameObject;
        equippedImage.SetActive(true);

        //tum groundtilelari destroy
        GameManager.inst.DestroyGroundTiles();

        //yeni  skinle ground tileolustur
        groundSpawner.GetComponent<GroundSpawner>().SpawnTile7();

        DeloadPreviousCostume(PlayerPrefs.GetInt("WpreviousSelectedItemIndex"));

        PlayerPrefs.SetInt("WpreviousSelectedItemIndex", itemIndex);
    }
    void DeloadPreviousCostume(int previousSelectedItemIndex)
    {
        if (PlayerPrefs.GetInt("WItemSelected") == previousSelectedItemIndex)
        {
            return;
        }
        else
        {

            int itemIndex = PlayerPrefs.GetInt("WpreviousSelectedItemIndex");

            GameObject equippedImage = ShopScrollView.GetChild(itemIndex).GetChild(3).gameObject;
            equippedImage.SetActive(false);

        }
    }



    void OnShopItemButtonClicked(int itemIndex)
    {
        if (!ShopItemsList[itemIndex].IsPurchased && GameManager.inst.IsMoneyEnough(ShopItemsList[itemIndex].Price))
        {
            GameManager.inst.SuccesfullBuy(ShopItemsList[itemIndex].Price);
            //purchase 
            ShopItemsList[itemIndex].IsPurchased = true;

            buyButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "BOUGHT";

            ShopScrollView.GetChild(itemIndex).GetChild(1).gameObject.SetActive(false);


            //esya satin alindiktan sonra select butonunu aktif et 
            SelectButtonActivate(itemIndex);

            marketMenu.whenMenuActive();
            PlayerPrefs.SetInt("WItemIndexIsBought_" + itemIndex, ShopItemsList[itemIndex].IsPurchased ? 1 : 0);

        }


    }
    public void OnSelectItemButtonClick(int itemIndex)
    {
        //check image aktif
        GameObject equippedImage = ShopScrollView.GetChild(itemIndex).GetChild(3).gameObject;
        equippedImage.SetActive(true);

        PlayerPrefs.SetInt("WItemSelected", itemIndex);

        LoadEquippedSkin(itemIndex);

    }

    void DeselectItemEquippedImage(int itemIndex)
    {
        GameObject equippedImage = ShopScrollView.GetChild(itemIndex).GetChild(3).gameObject;
        equippedImage.SetActive(false);

    }
    void ItemEquippedImageON(int itemIndex)
    {
        GameObject equippedImage = ShopScrollView.GetChild(itemIndex).GetChild(3).gameObject;
        equippedImage.SetActive(true);

    }

    void SelectButtonOff(int itemIndex)
    {
        GameObject selectbutton = ShopScrollView.GetChild(itemIndex).GetChild(4).gameObject;
        selectbutton.SetActive(false);
    }

    void SelectButtonActivate(int itemIndex)
    {
        GameObject selectButton = ShopScrollView.GetChild(itemIndex).GetChild(4).gameObject;
        selectButton.SetActive(true);

        SelectButtonActivateInteract(itemIndex);

    }

    void SelectButtonActivateInteract(int itemIndex)
    {
        selectButton = ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();
        selectButton.AddEventListener(itemIndex, OnSelectItemButtonClick);
        selectButton.interactable = true;
    }


}


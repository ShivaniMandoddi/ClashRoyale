using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationBannerScript : MonoBehaviour
{
    public GameObject useImage;
    public Image cardImage;
    public Image gamecardImage;

    private void Awake()
    {
        CardsData.OnDataFromCard += DisplayUIBanner;
    }
    private void OnEnable()
    {
        CardsData.OnDataFromCard += DisplayUIBanner;
       
    }
    private void OnDisable()
    {
        CardsData.OnDataFromCard -= DisplayUIBanner;

    }

    public void BannerUse(GameObject obj)
    {

        //Data data = CardsData.Instance.GetData();
        BannerPanel1 data = CollectionsBannerPanel.Instance.GetData();
        for (int i = 0; i < data.bannerPanel.bannerdata.Length; i++)
        {
            
            if (this.gameObject == data.bannerPanel.bannerdata[i].card )
            {
                data.bannerPanel.bannerdata[i].currentuse = true;
                useImage.SetActive(true);
            }
            else
            {
                data.bannerPanel.bannerdata[i].currentuse = false;
                useImage.SetActive(false);
            }
            CollectionsBannerPanel.Instance.UpdateData(data);
            //CardsData.Instance.UpdateFile(data, data.bannerPanel.bannerdata[i], data.bannerPanel.bannerdata[i].card);
        }
       

    }
    public void FrameUse(GameObject obj)
    {
        // Data data = CardsData.Instance.GetData();
        BannerPanel1 data = CollectionsBannerPanel.Instance.GetData();
        for (int i = 0; i < data.bannerPanel.framedata.Length; i++)
        {

            if (this.gameObject == data.bannerPanel.framedata[i].card)
            {
                data.bannerPanel.framedata[i].currentuse = true;
                useImage.SetActive(true);
            }
            else
            {
                data.bannerPanel.framedata[i].currentuse = false;
                useImage.SetActive(false);
            }
            CollectionsBannerPanel.Instance.UpdateData(data);
            //CardsData.Instance.UpdateFile(data, data.bannerPanel.framedata[i], data.bannerPanel.framedata[i].card);
        }
    }
    public void DisplayUIBanner(GameObject obj, Cards1 carddata, Data data)
    {

        if (obj == this.gameObject)
        {

            if (((BannerData)carddata).currentuse)
            {
                useImage.SetActive(true);

            }
            else
            {
                useImage.SetActive(false);

            }
            if (((BannerData)carddata).isFrame)
            {

                gamecardImage.color = ((BannerData)carddata).colour;
            }
            else
            {
                cardImage.sprite = carddata.cardSprite;
            }

        }
    }
    public void DisplayUIBanner(GameObject obj, Cards1 carddata)
    {

        if (obj == this.gameObject)
        {

            if (((BannerData)carddata).currentuse)
            {
                useImage.SetActive(true);

            }
            else
            {
                useImage.SetActive(false);

            }
            if (((BannerData)carddata).isFrame)
            {

                gamecardImage.color = ((BannerData)carddata).colour;
            }
            else
            {
                cardImage.sprite = carddata.cardSprite;
            }

        }
    }


        
   
}

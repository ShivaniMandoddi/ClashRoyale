using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsBannerPanel : MonoBehaviour
{
    public BannerPanel1 bannerPanel1;
    private static CollectionsBannerPanel instance;
    public static CollectionsBannerPanel Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CollectionsBannerPanel>();
            return instance;
        }
    }
    void Start()
    {
        BannerPanel1 data = bannerPanel1;
        for (int i = 0; i < data.bannerPanel.bannerdata.Length; i++)
        {
            GameObject obj = Instantiate(data.bannerdecorationcard);
            obj.transform.parent = data.bannercontent.transform;

            data.bannerPanel.bannerdata[i].card = obj;
            DecorationBannerScript[] decorationBannerScripts = FindObjectsOfType<DecorationBannerScript>(true);
            for (int k = 0; k < decorationBannerScripts.Length; k++)
            {
                decorationBannerScripts[k].DisplayUIBanner(obj, data.bannerPanel.bannerdata[i]);
            }
            //UpdateFile(data, data.bannerPanel.bannerdata[i], data.bannerPanel.bannerdata[i].card);

            //StartCoroutine(GettingData(obj, data.bannerPanel.bannerdata[i], data, CardAction));

        }

        for (int i = 0; i < data.bannerPanel.framedata.Length; i++)
        {
            GameObject obj = Instantiate(data.bannerframecard);
            obj.transform.parent = data.bannerframecontent.transform;
            data.bannerPanel.framedata[i].card = obj;
            DecorationBannerScript[] decorationBannerScripts = FindObjectsOfType<DecorationBannerScript>(true);
            for (int k = 0; k < decorationBannerScripts.Length; k++)
            {
                decorationBannerScripts[k].DisplayUIBanner(obj, data.bannerPanel.framedata[i]);
            }
            //UpdateFile(data, data.bannerPanel.framedata[i], data.bannerPanel.framedata[i].card);
            //StartCoroutine(GettingData(obj, data.bannerPanel.framedata[i], data, CardAction));
        }
        bannerPanel1 = data;
    }
    public BannerPanel1 GetData()
    {
        return bannerPanel1;
    }
    public void UpdateData(BannerPanel1 data)
    {
        bannerPanel1 = data;
        UpdateUI();

    }
    public void UpdateUI()
    {
        BannerPanel1 data = bannerPanel1;
        for (int i = 0; i < data.bannerPanel.bannerdata.Length; i++)
        {
           
            DecorationBannerScript[] decorationBannerScripts = FindObjectsOfType<DecorationBannerScript>(true);
            for (int k = 0; k < decorationBannerScripts.Length; k++)
            {
                decorationBannerScripts[k].DisplayUIBanner(data.bannerPanel.bannerdata[i].card, data.bannerPanel.bannerdata[i]);
            }
          
        }

        for (int i = 0; i < data.bannerPanel.framedata.Length; i++)
        {
            
            DecorationBannerScript[] decorationBannerScripts = FindObjectsOfType<DecorationBannerScript>(true);
            for (int k = 0; k < decorationBannerScripts.Length; k++)
            {
                decorationBannerScripts[k].DisplayUIBanner(data.bannerPanel.framedata[i].card, data.bannerPanel.framedata[i]);
            }
           
        }
        
    }

}
[System.Serializable]
public class BannerPanel1
{
    public GameObject bannerdecorationcard;
    public GameObject bannercontent;
    public BannerPanel bannerPanel;

    public GameObject bannerframecard;
    public GameObject bannerframecontent;
}

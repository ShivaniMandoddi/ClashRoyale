using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum Coins
{
    RUPEE,GOLD
}
public class CardsData : MonoBehaviour
{
    //public delegate void Data1(in T1,in T2,out)
    public delegate void DataFromCard(GameObject obj,Cards1 data,Data data1);
    public static event DataFromCard OnDataFromCard;
    public Text coins;
    public Text money;
   
    public Data data=new Data();
    Dictionary<string, List<Image>> section = new Dictionary<string, List<Image>>();
    List<Image> cards;
    string filepath;
   
   
    
    
    #region SINGLETON


    private static CardsData instance;
    public static CardsData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.Find("Handling").GetComponent<CardsData>();
            }
            return instance;
        }
    }
    #endregion
    #region MONOBEHAVIOUR METHODS



    void Start()
    {
        filepath = Path.Combine(Application.persistentDataPath, "SectionsData");
        
        string jsondata=JsonUtility.ToJson(data);
        Debug.Log(jsondata);
        
        WriteFile(jsondata);
        
       // ReadFile();
       // CreateUI();
    }
   /* public void Adddata(Data data)
    {
        
        data3.datas.Add(data);
        Debug.Log(data3.datas.Count);
        UpdateFile(data3);
      
    }*/
    IEnumerator GettingData(GameObject obj, Cards1 data, Data data1, System.Action<GameObject, Cards1, Data> callback)
    {

        callback?.Invoke(obj, data, data1);
        yield return null;
    }
    #endregion
    #region PUBLIC METHODS

    
    public void ReadFile()                         //Reading the Data
    {
        string data=File.ReadAllText(filepath);
        Debug.Log(data);
    }
    public Data GetData()              // Getting data from json
    {
        
        string jsondata = File.ReadAllText(filepath);
        Data data2 = JsonUtility.FromJson<Data>(jsondata);
        return (data2);
    }
    

    public void CreateUI()            // Creating Sections and Cards
    {
        data = GetData();
        coins.text = data.currentcoins.ToString();
        money.text = data.currentmoney + "$";
     
       


    }
    
  
    public static void CardAction(GameObject obj,Cards1 data, Data data1) //Calling event to change data in ui
    {
        
            OnDataFromCard?.Invoke(obj, data, data1);
        
            
    }
   
    
    public void UpdateFile(Data data,Cards1 carddata,GameObject obj)  //Updating card data and json data
    {
       
        coins.text = data.currentcoins.ToString();
        money.text = data.currentmoney + "$";
      
        
        string jsondata = JsonUtility.ToJson(data);
        File.WriteAllText(filepath, jsondata);
       
       
    }
    public void UpdateFile(Data data) 
    {
        string jsondata = JsonUtility.ToJson(data);
        File.WriteAllText(filepath, jsondata);
    }
   

    public void WriteFile(string data)    // Creating json data
    {
       // if(!File.Exists(filepath))
        //{
            
            File.WriteAllText(filepath,data);
        //}
    }
    #endregion

}
#region CLASSES





[System.Serializable]
public class Data 
{
   
    public int currentcoins;
    public int currentmoney;
    public Sprite coinSprite;
   
    
   
}
[System.Serializable]
public class Cards1
{
    public GameObject card;
    public int cost;
    public string name;
    public Sprite cardSprite;
    public Coins cointype;
    public bool btn;
    public int updatevalue;
    public int availablecards;
    public Image inventorycard;
    
}
[System.Serializable]
public class Sections1
{
    public string sectionname;
    public string secdesc;
    public Text InfoText;
    public GameObject info;
    public GameObject sectionobject;
    public Cards1[] cards;  
}
[System.Serializable]
public class Collections 
{
    public GameObject unusedcontent;
    public int totaldeckcards = 8;
    public int totalpresentcards = 8;
    public InfoCards[] topcards;
    public Image[] image;
    //public Sections1[] sections;
}
[System.Serializable]
public class InfoCards : Cards1
{
   
    public int remainedupgrades;
    public int speed;
    public int accuracy;
    public int points;
    public int upgradecoins;
    public bool isdisplay;
    public GameObject button;
    public bool isused;
    public Vector3 position;
    public float totallevels;
    public float levels;

}
[System.Serializable]
public class InfoPanel
{
    public GameObject presentcard;
    public GameObject infopanel;
    public Text upgradecost;
   // public Text infodescription;
    public Text speed;
    public Text hitpoints;
    public Text accuracy;
    public Image cardImage;
    public Button upgradeButton;
}
[System.Serializable]
public class BottomCollectionPanel
{
    public GameObject content;
    public GameObject cardprefab;
    public Text sectionname;
    public Sections1[] bottomSection;
}
[System.Serializable]
public class BannerPanel 
{
    public Sections1[] sections;
    public BannerData[] bannerdata;
    public BannerData[] framedata;

}
[System.Serializable]
public class BannerData : Cards1
{
    public bool currentuse;
    public bool isFrame;
    public Color colour;
}
#endregion


#region Unused


/*
 * INStart
 *   for (int i = 0; i < data.sections.Length; i++)
        {
            GameObject temp = Instantiate<GameObject>(data.sectionprefab);
            data.sections[i].sectionobject = temp;
            temp.transform.parent = data.dataContent.transform;
            temp.transform.GetChild(0).GetComponent<Text>().text = data.sections[i].sectionname;
            cards = new List<Image>();
            for (int j = 0; j < data.sections[i].cards.Length; j++)
            {
                Image obj = Instantiate(data.cardPrefab);
                data.sections[i].cards[j].card = obj.gameObject;
                obj.transform.parent = temp.transform.GetChild(1).transform;

                UpdateFile(data, data.sections[i].cards[j], obj.gameObject);

                StartCoroutine(GettingData(data.sections[i].cards[j].card.gameObject, data.sections[i].cards[j], data, CardAction));
                cards.Add(obj);


            }

            section.Add(data.sections[i].sectionname, cards);
        }

//CreateBannerDecoration();
// TopSectionCardsDisplay();
//BottomSectionDisplay(0);
 private void TopSectionCardsDisplay() //loading character cards data
{
    /* Data data = GetData();
     data.collections.totalpresentcards = 0;
     for (int i = 0; i < data.collections.topcards.Length; i++)
     {
         GameObject obj;

         if (i >= data.collections.totaldeckcards)
         {
             obj = Instantiate(data.topcardprefab);
             obj.transform.parent = data.collections.unusedcontent.transform;
             data.collections.topcards[i].card = obj;
             data.collections.topcards[i].position = data.collections.topcards[i].card.transform.position;
             data.collections.topcards[i].isused = true;
             data.collections.topcards[i].card.GetComponent<CollectionsTopCard>().buttonText.text = "Use";
             data.collections.topcards[i].button = obj.GetComponent<CollectionsTopCard>().buttons;
         }
         else
         {
             obj = Instantiate(data.topcardprefab);
             obj.transform.parent = data.topcardContent.transform;
             data.collections.topcards[i].isused = false;
             data.collections.topcards[i].card = obj;
             data.collections.image[i].gameObject.SetActive(false);
             data.collections.totalpresentcards++;
             data.collections.topcards[i].position = data.collections.topcards[i].card.transform.position;
             data.collections.topcards[i].button = obj.GetComponent<CollectionsTopCard>().buttons;
         }
         UpdateFile(data, data.collections.topcards[i], obj);
         StartCoroutine(GettingData(data.collections.topcards[i].card, data.collections.topcards[i], data, CardAction));
     }
     for (int i = 0; i < data.collections.totaldeckcards; i++)
     {
         int v = i + data.collections.totaldeckcards;
         data.collections.topcards[i].card.transform.SetSiblingIndex(i);
         data.collections.image[i].gameObject.transform.SetSiblingIndex(v);

         UpdateFile(data, data.collections.topcards[i], data.collections.topcards[i].card);
     }


     for (int i = 0; i < data.bottomCollectionPanel.bottomSection.Length; i++)
     {
         for (int j = 0; j < data.bottomCollectionPanel.bottomSection[i].cards.Length; j++)
         {
             GameObject temp = Instantiate(data.bottomCollectionPanel.cardprefab);
             temp.transform.parent = data.bottomCollectionPanel.content.transform;
             temp.GetComponent<Image>().sprite = data.bottomCollectionPanel.bottomSection[i].cards[j].cardSprite;
             data.bottomCollectionPanel.bottomSection[i].cards[j].card = temp;
             data.bottomCollectionPanel.bottomSection[i].cards[j].card.SetActive(false);

             UpdateFile(data, data.bottomCollectionPanel.bottomSection[i].cards[j], temp);
         }
     }
}
public void CreateBannerDecoration() // Banner Panel Data loading
    {
        
        Data data = GetData();
        for (int i = 0; i < data.bannerPanel.bannerdata.Length; i++)
        {
            GameObject obj = Instantiate(data.bannerdecorationcard);
            obj.transform.parent = data.bannercontent.transform;
            
            data.bannerPanel.bannerdata[i].card = obj;
            
            UpdateFile(data, data.bannerPanel.bannerdata[i], data.bannerPanel.bannerdata[i].card);
            
            StartCoroutine(GettingData(obj, data.bannerPanel.bannerdata[i], data,CardAction));
            
        }
      
        for (int i = 0; i < data.bannerPanel.framedata.Length; i++)
        {
            GameObject obj = Instantiate(data.bannerframecard);
            obj.transform.parent = data.bannerframecontent.transform;
            data.bannerPanel.framedata[i].card = obj;
            UpdateFile(data, data.bannerPanel.framedata[i], data.bannerPanel.framedata[i].card);
            StartCoroutine(GettingData(obj, data.bannerPanel.framedata[i], data, CardAction));
        }
    }

public void BottomSectionDisplay(int j) // Loading unlock characters data
{
    for (int k = 0; k < data.bottomCollectionPanel.bottomSection.Length; k++)
    {
        for (int i = 0; i < data.bottomCollectionPanel.bottomSection[k].cards.Length; i++)
        {
            if (k == j)
            {
                data.bottomCollectionPanel.sectionname.text = data.bottomCollectionPanel.bottomSection[k].sectionname;
                data.bottomCollectionPanel.bottomSection[k].cards[i].card.SetActive(true);
            }
            else
                data.bottomCollectionPanel.bottomSection[k].cards[i].card.SetActive(false);
            UpdateFile(data, data.bottomCollectionPanel.bottomSection[k].cards[i], data.bottomCollectionPanel.bottomSection[k].cards[i].card);
        }
    }

}

public Data InventoryDisplay()//upgrading data in inventory panel
{
    data = GetData();
    for (int i = 0; i < data.sections.Length; i++)
    {
        for (int j = 0; j < data.sections[i].cards.Length; j++)
        {
            if (data.sections[i].cards[j].availablecards>0 && data.sections[i].cards[j].inventorycard==null)
            {
                Image temp = Instantiate(data.inventorycardPrefab);

                temp.transform.parent = data.inventoryContent.transform;
                data.sections[i].cards[j].inventorycard = temp;

            }
            else if(data.sections[i].cards[j].availablecards > 0 && data.sections[i].cards[j].inventorycard.gameObject.activeInHierarchy==false)
            {
                data.sections[i].cards[j].inventorycard.gameObject.SetActive(true);

            }
            else if (data.sections[i].cards[j].availablecards==0 && data.sections[i].cards[j].inventorycard!=null)
            {

            }

        }
    }
    return data;
}*/
#endregion
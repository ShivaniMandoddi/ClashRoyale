using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionsDisplayPanel1 : MonoBehaviour
{
    public CollectionsPanel1 collectionsPanel;
    private static CollectionsDisplayPanel1 instance;
    public static CollectionsDisplayPanel1 Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CollectionsDisplayPanel1>();
            return instance;
        }
    }
    void Start()
    {
        CollectionsPanel1 data= collectionsPanel;
       
      
        CardsData.Instance.WriteData(collectionsPanel,1);
        
        data.collections.totalpresentcards = 0;
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            GameObject obj;

            if (i >= data.collections.totaldeckcards)
            {
                obj = Instantiate(data.topcardprefab);
                obj.transform.parent = data.collections.unusedcontent.transform;
                data.collections.topcards[i].card = obj;
               // data.collections.topcards[i].position = data.collections.topcards[i].card.transform.position;
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
                //data.collections.topcards[i].position = data.collections.topcards[i].card.transform.position;
                data.collections.topcards[i].button = obj.GetComponent<CollectionsTopCard>().buttons;
            }
            CollectionsTopCard[] collectionsTopCards = FindObjectsOfType<CollectionsTopCard>(true);
            for (int k = 0; k < collectionsTopCards.Length; k++)
            {
                collectionsTopCards[k].Display(data.collections.topcards[i].card, data.collections.topcards[i]);
            }
            //UpdateFile(data, data.collections.topcards[i], obj);
            //StartCoroutine(GettingData(data.collections.topcards[i].card, data.collections.topcards[i], data, CardAction));
        }
        for (int i = 0; i < data.collections.totaldeckcards; i++)
        {
            int v = i + data.collections.totaldeckcards;
            data.collections.topcards[i].card.transform.SetSiblingIndex(i);
            data.collections.image[i].gameObject.transform.SetSiblingIndex(v);
            CollectionsTopCard[] collectionsTopCards = FindObjectsOfType<CollectionsTopCard>(true);
            for (int k = 0; k < collectionsTopCards.Length; k++)
            {
                collectionsTopCards[k].Display(data.collections.topcards[i].card, data.collections.topcards[i]);
            }

            // UpdateFile(data, data.collections.topcards[i], data.collections.topcards[i].card);
        }

        
        for (int i = 0; i < data.bottomCollectionPanel.bottomSection.Length; i++)
        {
            for (int j = 0; j < data.bottomCollectionPanel.bottomSection[i].cards.Length; j++)
            {
                GameObject temp = Instantiate(data.bottomCollectionPanel.cardprefab);
                temp.transform.parent = data.bottomCollectionPanel.content.transform;
                temp.GetComponent<Image>().sprite = data.bottomCollectionPanel.bottomSection[i].cards[j].cardSprite;
                data.bottomCollectionPanel.bottomSection[i].cards[j].card = temp;
                //data.bottomCollectionPanel.bottomSection[i].cards[j].card.SetActive(false);

               // UpdateFile(data, data.bottomCollectionPanel.bottomSection[i].cards[j], temp);
            }
        }
        collectionsPanel = data;
    }
    public void UpdateData(CollectionsPanel1 data)
    {
        collectionsPanel = data;
    }
    public CollectionsPanel1 GetData()
    {
        return collectionsPanel;
    }

   
}
[System.Serializable]
public class CollectionsPanel1:Data1
{
    public Collections collections;
    public GameObject topcardContent;
    public GameObject topcardprefab;
    public InfoPanel infoPanel;
    public BottomCollectionPanel bottomCollectionPanel;

}

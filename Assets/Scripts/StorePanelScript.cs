using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StorePanelScript : MonoBehaviour
{
    public StoreItems items;


    private static StorePanelScript instance;
    public static StorePanelScript Instance
    {
        get
        { 
            if (instance == null)
                instance=FindObjectOfType<StorePanelScript>();
            return instance; 
        }
    }
    private void Awake()
    {
      
        CardsData.Instance.WriteData(items,0);
    }
    void Start()
    {
        
        CreateCards();

    }

    private void CreateCards()
    {
        
        for (int i = 0; i < items.sections.Length; i++)
        {
            GameObject temp = Instantiate<GameObject>(items.sectionprefab);
            items.sections[i].sectionobject = temp;
            temp.transform.parent = items.sectioncontent.transform;
            temp.transform.GetChild(0).GetComponent<Text>().text = items.sections[i].sectionname;

            for (int j = 0; j < items.sections[i].cards.Length; j++)
            {
                Image obj = Instantiate(items.cardPrefab);
                items.sections[i].cards[j].card = obj.gameObject;
                obj.transform.parent = temp.transform.GetChild(1).transform;


                ButtonScript[] buttonScript = FindObjectsOfType<ButtonScript>(true);
                for (int k = 0; k < buttonScript.Length; k++)
                {
                    buttonScript[k].UploadingData(items.sections[i].cards[j].card.gameObject, items.sections[i].cards[j]);
                }



            }
           


        }
        UpdateData(items);
    }
    public StoreItems GetData()
    {
        return items;
    }
    public void UpdateData(StoreItems items)
    {
        this.items = items;
        InventoryUpdateData();
        for (int i = 0; i < items.sections.Length; i++)
        {
            for (int j = 0; j < items.sections[i].cards.Length; j++)
            {
                ButtonScript[] buttonScript = FindObjectsOfType<ButtonScript>(true);
                
                for (int k = 0; k < buttonScript.Length; k++)
                {
                    buttonScript[k].UploadingData(items.sections[i].cards[j].card.gameObject, items.sections[i].cards[j]);
                }
               
            }
        }
        
        

    }
    public void InventoryUpdateData()
    {
        for (int i = 0; i < items.sections.Length; i++)
        {
            for (int j = 0; j < items.sections[i].cards.Length; j++)
            {
                if (items.sections[i].cards[j].availablecards > 0 && items.sections[i].cards[j].inventorycard == null)
                {
                    Image temp = Instantiate(items.inventorycardPrefab);

                    temp.transform.parent = items.inventoryContent.transform;
                    items.sections[i].cards[j].inventorycard = temp;

                }
                else if (items.sections[i].cards[j].availablecards > 0 && items.sections[i].cards[j].inventorycard.gameObject.activeInHierarchy == false)
                {
                    items.sections[i].cards[j].inventorycard.gameObject.SetActive(true);

                }
                
                InventoryScript[] inventoryScripts = FindObjectsOfType<InventoryScript>(true);
                Debug.Log(inventoryScripts.Length);
                for (int k = 0; k < inventoryScripts.Length; k++)
                {
                    if (items.sections[i].cards[j].inventorycard != null)
                    {
                        inventoryScripts[k].DisplayUI(items.sections[i].cards[j].inventorycard.gameObject, items.sections[i].cards[j]);
                    }
                }

            }
        }
    }

}
[System.Serializable]
public class StoreItems  :Data1
{
    public Image inventorycardPrefab;
    public GameObject inventoryContent;
    public GameObject sectioncontent;
    public Image cardPrefab;
    public GameObject sectionprefab;
    public Sections1[] sections;
}



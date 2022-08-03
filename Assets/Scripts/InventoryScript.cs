using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Image cardSprite;
    public Text cardAvailable;
    public Text cardname;

   
    private void Awake()
    {
        CardsData.OnDataFromCard += DisplayUI;
    }

    public void UseButton() //Using the items in inventory panel
    {
        Data data = CardsData.Instance.GetData();
        StoreItems items = StorePanelScript.Instance.GetData();
        for (int i = 0; i < items.sections.Length; i++)
        {
            for (int j = 0; j < items.sections[i].cards.Length; j++)
            {
                if (items.sections[i].cards[j].inventorycard != null)
                {
                    if (items.sections[i].cards[j].inventorycard.gameObject.Equals(this.gameObject))
                    {
                        items.sections[i].cards[j].availablecards = Mathf.Clamp(items.sections[i].cards[j].availablecards - 1, 0, int.MaxValue);
                        if (items.sections[i].cards[j].availablecards == 0)
                            items.sections[i].cards[j].inventorycard.gameObject.SetActive(false);
                        // CardsData.Instance.UpdateFile(data, items.sections[i].cards[j], this.gameObject);
                        StorePanelScript.Instance.UpdateData(items);
                    }
                }
            }
        }
        
    }
    private void OnEnable()
    {
        CardsData.OnDataFromCard += DisplayUI;
    }
    private void OnDisable()
    {
        CardsData.OnDataFromCard -= DisplayUI;
    }
    public void DisplayUI(GameObject obj,Cards1 carddata,Data data) //Displaying Data
    {
       
        if(obj==this.gameObject)
        {
            cardSprite.sprite = carddata.cardSprite;
            cardAvailable.text = carddata.availablecards.ToString();
            cardname.text = carddata.name;
        }
    }
    public void DisplayUI(GameObject obj, Cards1 carddata) //Displaying Data
    {
       
        if (obj == this.gameObject)
        {
            cardSprite.sprite = carddata.cardSprite;
            cardAvailable.text = carddata.availablecards.ToString();
            cardname.text = carddata.name;
        }
    }
}

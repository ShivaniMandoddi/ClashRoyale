using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CollectionsTopCard : MonoBehaviour
{
    public Image cardImage;
    public Text remainingupgrades;
    public Slider level;
    public Text levelText;
    //public Button upgrade;
    public GameObject buttons;
    public Text buttonText;
   
    Vector3 position1;
    GameObject swappingcard;
    
    private void Awake()
    {
        CardsData.OnDataFromCard += Display;
       
        
    }
    public void InfoButton() // Displaying Info Panel
    {
       
        SwitchingPages.Instance.InfoPanel();
        
        Data data1 = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            if (this.gameObject == data.collections.topcards[i].card)
            {
                data.infoPanel.presentcard = this.gameObject;
                
                data.infoPanel.cardImage.sprite = data.collections.topcards[i].cardSprite;
                data.infoPanel.speed.text = "Speed\n" + data.collections.topcards[i].speed;
                data.infoPanel.accuracy.text = "Accuracy\n" + data.collections.topcards[i].accuracy;
                data.infoPanel.hitpoints.text = "HitPoints\n" + data.collections.topcards[i].points;
                //data.infoPanel.infodescription.text = "Speed: " + data.collections.topcards[i].speed + "\nAccuracy: " + data.collections.topcards[i].accuracy + "\nPoints: " + data.collections.topcards[i].points;
                data.infoPanel.upgradecost.text = data.collections.topcards[i].upgradecoins.ToString();
                if (data1.currentcoins < data.collections.topcards[i].upgradecoins)
                    data.infoPanel.upgradeButton.interactable = false;
                else
                    data.infoPanel.upgradeButton.interactable = true;

            }
            CollectionsDisplayPanel1.Instance.UpdateData(data);
            CardsData.Instance.UpdateFile(data1, data.collections.topcards[i], this.gameObject);
        }
       

    }
    public void UpgradingPower()  //Upgrading the data, when card is upgraded
    {

        Data data1 = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            if (data.infoPanel.presentcard == data.collections.topcards[i].card)
            {

                data.collections.topcards[i].remainedupgrades--;
                data1.currentcoins = Mathf.Clamp(data1.currentcoins - data.collections.topcards[i].upgradecoins, 0, int.MaxValue);
                data.collections.topcards[i].upgradecoins += 100;
                data.collections.topcards[i].speed += 1;
                
                data.collections.topcards[i].accuracy += 3;
                data.collections.topcards[i].points += 2;
                data.infoPanel.cardImage.sprite = data.collections.topcards[i].cardSprite;
                data.infoPanel.speed.text = "Speed\n" + data.collections.topcards[i].speed;
                data.infoPanel.accuracy.text = "Accuracy\n" + data.collections.topcards[i].accuracy;
                data.infoPanel.hitpoints.text = "HitPoints\n" + data.collections.topcards[i].points;
                //data.infoPanel.infodescription.text = "Speed: " + data.collections.topcards[i].speed + "\nAccuracy: " + data.collections.topcards[i].accuracy + "\nPoints: " + data.collections.topcards[i].points;
                data.infoPanel.upgradecost.text = data.collections.topcards[i].upgradecoins.ToString();
                if (data1.currentcoins < data.collections.topcards[i].upgradecoins)
                    data.infoPanel.upgradeButton.interactable = false;
                else
                    data.infoPanel.upgradeButton.interactable = true;


            }
            CollectionsDisplayPanel1.Instance.UpdateData(data);
            CardsData.Instance.UpdateFile(data1, data.collections.topcards[i], this.gameObject);
        }
    }
    
    private void OnEnable()
    {
        CardsData.OnDataFromCard += Display;
    }
    private void OnDisable()
    {
        CardsData.OnDataFromCard -= Display;
    }
    public void Display(GameObject obj,Cards1 carddata,Data data)
    {
        
        if (obj==this.gameObject)
        {
            
            cardImage.sprite = ((InfoCards)carddata).cardSprite;
            remainingupgrades.text= ((InfoCards)carddata).remainedupgrades.ToString();
            level.value = ((InfoCards)carddata).levels / ((InfoCards)carddata).totallevels;
            levelText.text = ((InfoCards)carddata).levels + "/" + ((InfoCards)carddata).totallevels;
        }
    }
    public void Display(GameObject obj, Cards1 carddata)
    {

        if (obj == this.gameObject)
        {

            cardImage.sprite = ((InfoCards)carddata).cardSprite;
            remainingupgrades.text = ((InfoCards)carddata).remainedupgrades.ToString();
            level.value = ((InfoCards)carddata).levels / ((InfoCards)carddata).totallevels;
            levelText.text = ((InfoCards)carddata).levels + "/" + ((InfoCards)carddata).totallevels;
        }
    }
    public void CardClicking()  //Displaying Info and Remove Buttons of particular card
    {
       // Data data = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            if (data.collections.topcards[i].card == this.gameObject && !data.collections.topcards[i].isdisplay)
            {
                data.collections.topcards[i].button.SetActive(true);
                data.collections.topcards[i].isdisplay = true;
            }
            else if (data.collections.topcards[i].card == this.gameObject && data.collections.topcards[i].isdisplay)
            {
                data.collections.topcards[i].button.SetActive(false);
                data.collections.topcards[i].isdisplay = false;
            }
            else
            {
                data.collections.topcards[i].button.SetActive(false);
                data.collections.topcards[i].isdisplay = false;

            }
            CollectionsDisplayPanel1.Instance.UpdateData(data);
            //CardsData.Instance.UpdateFile(data, data.collections.topcards[i], this.gameObject);
        }

      
    }
    public void OnPointingDown()  // Taking Intial Card Position
    {
        //Data data=CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            if (this.gameObject == data.collections.topcards[i].card)
            {
               // position1 = data.collections.topcards[i].position;
            }
        }
        position1 = transform.position;

    }
    public void OnPointingDrag()       // Dragging The Card
    {
        //Data data = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            if (data.collections.topcards[i].card==this.gameObject && (!data.collections.topcards[i].isused || (data.collections.topcards[i].isused &&  data.collections.totalpresentcards==data.collections.totaldeckcards) ))
            {
                transform.position = Input.mousePosition;
            }
        }
        
      
        var hit = new List<RaycastResult>();
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        EventSystem.current.RaycastAll(eventData, hit);
       
        
        for (int i = 0; i < hit.Count; i++)
        {
            if (hit[i].gameObject.layer == 7  && hit[i].gameObject!=this.gameObject &&hit[i].gameObject!=null)
            {
                
                swappingcard = hit[i].gameObject;
                
                break;
               
            }
            else if(hit[i].gameObject == null && swappingcard !=null)
            {
                swappingcard = null;
            }
                
        }
        
        
    }
   
    public void OnPointExit()
    {


        //Data data = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        int k=-1;
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            
            if (data.collections.topcards[i].card == swappingcard && !data.collections.topcards[i].isused && swappingcard!=null)
            {
                
                k = i;
                break;
            }
            else if(data.collections.topcards[i].card == swappingcard && data.collections.topcards[i].isused && data.collections.totalpresentcards <= data.collections.totaldeckcards)
            {
                swappingcard = null;
                break;
            }
        }
       
        if (swappingcard != null && k!=-1)
        {

            int i;
            for (i = 0; i < data.collections.topcards.Length; i++)
            {
                if (data.collections.topcards[i].card == this.gameObject && !data.collections.topcards[i].isused)
                {
                    
                    data.collections.topcards[i].card.transform.SetSiblingIndex(k);
                    data.collections.topcards[k].card.transform.SetSiblingIndex(i);
                   
           
                    (data.collections.topcards[k], data.collections.topcards[i]) = (data.collections.topcards[i], data.collections.topcards[k]);
                    // CardsData.Instance.UpdateFile(data, data.collections.topcards[i], data.collections.topcards[i].card);
                    //CardsData.Instance.UpdateFile(data, data.collections.topcards[k], data.collections.topcards[k].card);
                    CollectionsDisplayPanel1.Instance.UpdateData(data);
                    break;
                }
                else if(data.collections.topcards[i].card == this.gameObject && data.collections.topcards[i].isused)
                {
                    data.collections.topcards[i].card.transform.parent = data.topcardContent.transform;
                    data.collections.topcards[k].card.transform.parent = data.collections.unusedcontent.transform;
                    data.collections.topcards[i].isused = false;
                    data.collections.topcards[k].isused = true;
                    data.collections.topcards[i].card.GetComponent<CollectionsTopCard>().buttonText.text = "Remove";
                    data.collections.topcards[k].card.GetComponent<CollectionsTopCard>().buttonText.text = "Use";
                    // CardsData.Instance.UpdateFile(data, data.collections.topcards[i], data.collections.topcards[i].card);
                    //CardsData.Instance.UpdateFile(data, data.collections.topcards[k], data.collections.topcards[k].card);
                    CollectionsDisplayPanel1.Instance.UpdateData(data);
                    break;

                }
            }
            
                
            
        }
        else 
        {
            //Debug.Log("Swapping should not done");
            this.transform.position = position1;
           
        }
       
        swappingcard = null;
    }
    public void Remove()            // Removing and Inserting Cards Into top section
    {
        // Data data = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        for (int i = 0; i < data.collections.topcards.Length; i++)
        {
            if (data.collections.topcards[i].card == this.gameObject && !data.collections.topcards[i].isused)
            {
                for (int j = 0; j < data.collections.totaldeckcards; j++)
                {
                    if (data.collections.image[j].gameObject.activeInHierarchy == false)
                    {
                        data.collections.topcards[i].card.transform.parent = data.collections.unusedcontent.transform;
                        data.collections.totalpresentcards--;
                        data.collections.topcards[i].button.SetActive(false);
                        data.collections.image[j].gameObject.SetActive(true);
                        buttonText.text = "Use";
                        data.collections.topcards[i].isused = true;
                        CollectionsDisplayPanel1.Instance.UpdateData(data);
                        // CardsData.Instance.UpdateFile(data, data.collections.topcards[i], this.gameObject);
                        break;
                    }
                }
            }
            else if (data.collections.topcards[i].card == this.gameObject && data.collections.topcards[i].isused && data.collections.totalpresentcards < data.collections.totaldeckcards)
            {

                for (int j = 0; j < data.collections.totaldeckcards; j++)
                {
                    if (data.collections.image[j].gameObject.activeInHierarchy == true)
                    {
                        data.collections.image[j].gameObject.SetActive(false);
                        data.collections.image[j].gameObject.transform.SetSiblingIndex(j + data.collections.totaldeckcards);
                        data.collections.topcards[i].card.transform.parent = data.topcardContent.transform;
                        data.collections.topcards[i].button.SetActive(false);
                        data.collections.totalpresentcards++;
                        buttonText.text = "Remove";
                        data.collections.topcards[i].isused = false;
                        CollectionsDisplayPanel1.Instance.UpdateData(data);
                        //CardsData.Instance.UpdateFile(data, data.collections.topcards[i], this.gameObject);
                        break;
                    }
                }
               

            }
        }
       
    }
  


}

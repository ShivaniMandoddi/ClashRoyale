using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchingPages : MonoBehaviour
    {
        public GameObject storePanel;
        public GameObject inventoryPanel;
        public GameObject homePanel;
        public GameObject backButton;
        public GameObject infopanel;
        public GameObject displayPanel;
        public GameObject display2Panel;
        public GameObject display3Panel;
        public GameObject display4Panel;
        public GameObject collectionsPanel;
    public GameObject bannerpanel;
    public GameObject badgesPanel;
    public Image[] buttonImages;
    public Sprite buttonSprite;
    public Sprite UnbuttonSprite;
     
        int value = 1;
         bool isdisplay;
        private static SwitchingPages instance;
        public static SwitchingPages Instance
        {
            get 
            {
            if (instance == null)
                instance = GameObject.Find("Canvas").GetComponent<SwitchingPages>();
                return instance; 
            }
        }


    void Start()
        {
        SetAllPanelsFalse();
        SettingCollectionPanelsFalse();
        displayPanel.SetActive(true);
        storePanel.SetActive(true);
            homePanel.SetActive(true);
            //backButton.SetActive(false);
        }
        public void Back()
        {
            SetAllPanelsFalse();
            homePanel.SetActive(true);
            //backButton.SetActive(false);
        }
        public void InventoryPanel()
        {
            SetAllPanelsFalse();
            inventoryPanel.SetActive(true);
           // backButton.SetActive(true);
        }
       
        public void StorePanel()
        {
            SetAllPanelsFalse();
            storePanel.SetActive(true);
            //backButton.SetActive(true);
        }
        public void InfoPanel()
        {
           
            displayPanel.SetActive(false);
             infopanel.SetActive(true);
            
            //backButton.SetActive(false);

        }
        public void DeckofCollection(GameObject paneld)
        {

        //SetAllPanelsFalse();
        SettingCollectionPanelsFalse();
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].sprite = UnbuttonSprite;
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = buttonSprite;
        collectionsPanel.SetActive(true);
        paneld.SetActive(true);
        }
        public void CollectionsPanel()
        {
            SetAllPanelsFalse();
       
        collectionsPanel.SetActive(true);
     
        }
    public void ExitInfoPanel()
    {
        displayPanel.SetActive(true);
        infopanel.SetActive(false);
    }

        public void SetAllPanelsFalse()
        {
            storePanel.SetActive(false);
            inventoryPanel.SetActive(false);
         
            collectionsPanel.SetActive(false);
       
        }
    public void SettingCollectionPanelsFalse()
    {
        display2Panel.SetActive(false);
        display3Panel.SetActive(false);
        display4Panel.SetActive(false);
        displayPanel.SetActive(false);
    }
    public void BottomCollectionPanel()
    {
        
        /*CardsData.Instance.BottomSectionDisplay(value);
        value++;
        Data data = CardsData.Instance.GetData();
        if(value==data.bottomCollectionPanel.bottomSection.Length)
        {
            value = 0;
        }*/
    }
    public void ClearCards()
    {

        // Data data=CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
          for (int i = 0; i < data.collections.topcards.Length; i++)
            {
                if (!data.collections.topcards[i].isused)
                    data.collections.topcards[i].card.GetComponent<CollectionsTopCard>().Remove();
            }
        CollectionsDisplayPanel1.Instance.UpdateData(data);
        
    }
    public void MenuButton(GameObject buttons)
    {
        buttons.SetActive(!buttons.activeInHierarchy);
    }
    public void CloseMenuPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void GetCards()
    {
        //Data data = CardsData.Instance.GetData();
        CollectionsPanel1 data = CollectionsDisplayPanel1.Instance.GetData();
        int i;
        List<int> numbers = new List<int>();
        
       
        i = data.collections.totaldeckcards-data.collections.totalpresentcards;
        while (i>0)
        {
            
            int j=Random.Range(0,data.collections.topcards.Length-1);
            if ( !numbers.Contains(j) && data.collections.topcards[j].isused)
            {
                data.collections.topcards[j].card.GetComponent<CollectionsTopCard>().Remove();
                numbers.Add(j);
                i--;
               
           }
        }
        
        
    }
  
   
    public void BannerDisplay()
    {
        bannerpanel.SetActive(true);
        badgesPanel.SetActive(false);
    }
    public void BadgesDisplay()
    {
        bannerpanel.SetActive(false);

        badgesPanel.SetActive(true);
    }
    public void InfoTextDisplay(GameObject sectionname)
    {
        //Data data=CardsData.Instance.GetData();
        BannerPanel1 data = CollectionsBannerPanel.Instance.GetData();
        for (int i = 0; i < data.bannerPanel.sections.Length; i++)
        {
            if (data.bannerPanel.sections[i].sectionobject==sectionname)
            {
                data.bannerPanel.sections[i].InfoText.text = data.bannerPanel.sections[i].secdesc;
                if (data.bannerPanel.sections[i].info.activeInHierarchy)
                    data.bannerPanel.sections[i].info.SetActive(false);
                else
                    data.bannerPanel.sections[i].info.SetActive(true);
            }
            else
            {
                data.bannerPanel.sections[i].info.SetActive(false);
            }
           // CardsData.Instance.UpdateFile(data);
        }
    }
   
 }


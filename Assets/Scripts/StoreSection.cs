using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreSection : MonoBehaviour
{

    public Text desctext;
    public GameObject displaytext;
    bool isclicked;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           
            displaytext.SetActive(false);
            isclicked = !isclicked;
        }
    }
    public void StoreDislpay()
    {
        if (!isclicked)
        {
           // displaytxt.SetActive(true);

            Data data = CardsData.Instance.GetData();
            StoreItems items = StorePanelScript.Instance.GetData();
            for (int i = 0; i < items.sections.Length; i++)
            {
                if (items.sections[i].sectionobject == this.gameObject)
                {
                    displaytext.SetActive(true);
                    desctext.text = items.sections[i].secdesc;
                }
            }
            isclicked = !isclicked;
        }
        else
        {
            displaytext.SetActive(false);
            isclicked = !isclicked;
        }
            
    }
   
   
   
   
    
}

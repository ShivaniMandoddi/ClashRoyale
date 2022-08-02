using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreData : MonoBehaviour
{

    public GameObject sectionprefab;
    public GameObject content;
    public Image card;
 
    public Card[] sections;
    void Start()
    {
        for (int i = 0; i < sections.Length; i++)
        {
           
            GameObject temp=Instantiate(sectionprefab);
            temp.transform.GetChild(0).GetComponent<Text>().text = sections[i].title;
            
            for (int j = 0; j < sections[i].number; j++)
            {
                Image obj = Instantiate(card);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = sections[i].cardSprite;
                obj.transform.GetChild(1).GetComponent<Text>().text = sections[i].cost+"$";
                obj.transform.parent = temp.transform.GetChild(1).transform;
            }
            temp.transform.parent = content.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class Card
{
    public int number;
   
    public string title;
    public string cost;
    public Sprite cardSprite;
    //public Image cardSprite;

}

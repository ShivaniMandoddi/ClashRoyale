using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    // Start is called before the first frame update
    Data data;
    public bool btn;
    public Image cardImage;
    public Text cardtitle;
    public Text cardCost;
    public GameObject cardlogo;
    Cards1 carddata;
    public Button button;
    public void Awake()
    {
        CardsData.OnDataFromCard += UploadingData;
    }
    public void ButtonClicked()  //When button clicked
    {


        btn = true;
        
        //carddata = CardsData.Instance.GetCardData(this.gameObject);
        Data data = CardsData.Instance.GetData();
        StoreItems items = StorePanelScript.Instance.GetData();
        for (int i = 0; i < items.sections.Length; i++)
        {
            for (int j = 0; j < items.sections[i].cards.Length; j++)
            {
                if (this.gameObject == items.sections[i].cards[j].card)
                {
                    carddata = items.sections[i].cards[j];
                    
                    break;
                }

            }
        }
        if (carddata != null)
        {
            
            for (int i = 0; i < items.sections.Length; i++)
            {
                for (int j = 0; j < items.sections[i].cards.Length; j++)
                {
                    if (this.gameObject == items.sections[i].cards[j].card)
                    {

                        items.sections[i].cards[j].availablecards = items.sections[i].cards[j].availablecards + 1;
                        //Debug.Log(data.sections[i].cards[j].availablecards++);
                        break;
                    }
                }
            }
            //carddata.availablecards++;
            if (carddata.cointype == Coins.RUPEE)
            {
                data.currentmoney = Mathf.Clamp(data.currentmoney - carddata.cost, 0, int.MaxValue);
            }
            else
            {
                data.currentcoins = Mathf.Clamp(data.currentcoins - carddata.cost, 0, int.MaxValue);
               
            }
            if (carddata.name.Contains("Gold"))
            {
                data.currentcoins += carddata.updatevalue;
            }
            CardsData.Instance.UpdateFile(data, carddata, this.gameObject);
           
        }
        StorePanelScript.Instance.UpdateData(items);


    }
    private void OnEnable()
    {
        CardsData.OnDataFromCard += UploadingData;

    }
    private void OnDisable()
    {
        CardsData.OnDataFromCard -= UploadingData;
    }
    public void UploadingData(GameObject obj, Cards1 data, Data totaldata) //Setting ui according to json data
    {
        if (obj == this.gameObject)
        {

            cardImage.sprite = data.cardSprite;

            cardtitle.text = data.name;
            btn = data.btn;
            button.interactable = true;
            if (data.cointype == Coins.RUPEE)
            {
                cardCost.text = data.cost + "$";
                cardlogo.SetActive(false);
                if (data.cost > totaldata.currentmoney)
                {
                    button.interactable = false;
                }
            }
            else
            {
                cardCost.text = data.cost.ToString();
                cardlogo.SetActive(true);
                if (data.cost > totaldata.currentcoins)
                {
                    button.interactable = false;
                }
            }

        }


    }
    public void UploadingData(GameObject obj, Cards1 data) //Setting ui according to json data
    {
        Data totaldata = CardsData.Instance.GetData();
        if (obj == this.gameObject)
        {

            cardImage.sprite = data.cardSprite;

            cardtitle.text = data.name;
            btn = data.btn;
            button.interactable = true;
            if (data.cointype == Coins.RUPEE)
            {
                cardCost.text = data.cost + "$";
                cardlogo.SetActive(false);
                if (data.cost > totaldata.currentmoney)
                {
                    button.interactable = false;
                }
            }
            else
            {
                cardCost.text = data.cost.ToString();
                cardlogo.SetActive(true);
                if (data.cost > totaldata.currentcoins)
                {
                    button.interactable = false;
                }
            }

        }


    }

}

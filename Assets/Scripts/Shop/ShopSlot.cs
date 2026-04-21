using TMPro;
using UnityEngine;

    public class ShopSlot : MonoBehaviour
    {
        //Figure out how to deal with currentItem (UseItem) vs the item Prefab
        public UseItem currentItem;
        public GameObject currentItemText;

        public int itemPrice;
        public TMP_Text priceText;
        public bool isShopSlot = true;

        private void Awake()
        {
            if (!priceText)
            {
                priceText = transform.Find("PriceText").GetComponent<TMP_Text>();
            }

        }

        public void UpdatePriceDisplay()
        {
            if (priceText && currentItem)
            {
                priceText.text = itemPrice.ToString() + " AG";
            }
        }

        public void SetItem(GameObject itemText, int price, UseItem useItem)
        {


            currentItem = useItem;
            itemPrice = price;
            currentItemText = itemText;

            UpdatePriceDisplay();

        }

        public void MoveItem(GameObject itemText, UseItem useItem)
        {


            currentItem = useItem;

            //TMP_Text textReplace = GetComponent<TMP_Text>();
            //Debug.Log(textReplace.text);
            priceText.text = "";
            priceText.text = currentItem.name; //Careful how you name stuff because I think it is getting the name of the object itself 



        }


    }



using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShopController : MonoBehaviour
{

            public static ShopController ShopControllerInstance;

    [Header("UI")]
    public GameObject shopPanel;
    public Transform shopInventoryGrid, playerInventoryGrid;
    public GameObject shopSlotPrefab; //name of item
    public TMP_Text playerMoneyText, shopTitleText;

    public TMP_Text ownText;
    public TMP_Text itemDescriptionText;
    public Transform itemDescriptionPanel;

    public GameObject buyText;
    public GameObject sellText;
    private ShopNPC currentShop;

    public GameObject[] slots;


    private void Awake()
    {
        if (ShopControllerInstance == null) ShopControllerInstance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        itemDescriptionText.text = "";
       
        shopPanel.SetActive(false);
        if (CurrencyController.CurrencyInstance != null)
        {
            CurrencyController.CurrencyInstance.OnGoldChanged += UpdateMoneyDisplay;
            UpdateMoneyDisplay(CurrencyController.CurrencyInstance.GetGold());
        }
    }


    private void UpdateMoneyDisplay(int amount)
    {
        if (playerMoneyText != null)
        {
            playerMoneyText.text = amount.ToString() + " AG";
        }
    }

    public void OpenShop(ShopNPC shop)
    {
        Debug.Log("Selected: " + EventSystem.current.currentSelectedGameObject);
        Debug.Log("Opening the Shop");
        currentShop = shop;
        shopPanel.SetActive(true);
        if (shopTitleText != null) shopTitleText.text = shop.shopName;
        RefreshShopDisplay();
        RefreshParentInventoryDisplay();
       // PauseController.SetPause(true);
        EventSystem.current.SetSelectedGameObject(buyText);
        Debug.Log("Selected: " + EventSystem.current.currentSelectedGameObject);
        StartCoroutine(DelayedHover());
        Debug.Log("Selected: " + EventSystem.current.currentSelectedGameObject);
        int gold = CurrencyController.CurrencyInstance.GetGold();
        Debug.Log("new gold: " + gold);
        UpdateMoneyDisplay(gold);

    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        currentShop = null;
        // PauseController.SetPause(false);

    }

    public void RefreshShopDisplay()
    {
        Debug.Log("refreshing");
        if (currentShop == null) return;
        foreach (Transform child in shopInventoryGrid) Destroy(child.gameObject);

        foreach (var stockItem in currentShop.GetCurrentStock())
        {
            if (stockItem.quantity <= 0) continue;

            CreateShopSlot(shopInventoryGrid, stockItem.generalItemID, true, stockItem.quantity);
        }
    }



    public void RefreshParentInventoryDisplay()
    {
       
       

        
    }

    public void HoverOnBuyText()
    {

        Debug.Log("hovering");
        foreach (Transform child in ShopController.ShopControllerInstance.itemDescriptionPanel)
            Destroy(child.gameObject);

        // UseItem item = firstSlot.item;
        //   ShopController.ShopControllerInstance.itemDescriptionText.text = item.name;



        Transform firstSlotTransform = ShopController.ShopControllerInstance.shopInventoryGrid.transform.GetChild(0); //Reference to Slot Transform
        ShopSlot slot = firstSlotTransform.GetComponent<ShopSlot>(); //Getting the Slot

        // Debug.Log(slot);
        Debug.Log(firstSlotTransform);

        Debug.Log(slot);
        Debug.Log("Slot object: " + slot.gameObject.name);
        Debug.Log("Instance ID: " + slot.GetInstanceID());

        if (slot.currentItem == null)
        {
            Debug.Log("No items in Shop");
            return;

        }

        if (slot.currentItem != null)
        {
            Debug.Log("Item(s) in Shop");

        }
    }

    private IEnumerator DelayedHover()
    {
        yield return null; // wait one frame
        HoverOnBuyText();
    }

    public void MoveToShopItems()
    {
        StartCoroutine(DelayMove());
    }

    private IEnumerator DelayMove()
    {
        yield return new WaitUntil(() => !Input.GetKey(KeyCode.Z));

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        Transform firstSlotTransform = ShopController.ShopControllerInstance.shopInventoryGrid.transform.GetChild(0); //Reference to Slot Transform
        ShopSlot slot = firstSlotTransform.GetComponent<ShopSlot>(); //Getting the Slot
                                                                     // Debug.Log(slot);
        Transform item = firstSlotTransform.GetChild(1); //Reference to Item Transform

        GameObject itemGO = item.gameObject; //Getting the Item

        EventSystem.current.SetSelectedGameObject(itemGO);
        Debug.Log("Selected: " + EventSystem.current.currentSelectedGameObject);


    }
    private void CreateShopSlot(Transform grid, int generalItemID, bool isShop, int quantity //Slot originalSlot = null
                                                                                              )
    {
        Debug.Log("Creating shop slot");
        GameObject slotObj = Instantiate(shopSlotPrefab, grid); //grid is the Panel
                                                                // UseItem item = useItemDictionary.GetItemPrefab(generalItemID);

        TMP_Text itemText = slotObj.GetComponentInChildren<TMP_Text>();
        if (itemText == null)
        {
            Debug.Log("itemText null");
        }
      

        GameObject itemInstance = Instantiate(itemText.gameObject, slotObj.transform); //is this the best way to do it?

        itemInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        //check if "item" is the right thing to use
       
        int itemQuantity = quantity;

        //Debug.Log(UseInventoryController.Instance.itemsCountCache[id]);

       // int price = isShop ? item.buyPrice : item.GetSellPrice();

        ShopSlot slot = slotObj.GetComponent<ShopSlot>();
        slot.isShopSlot = isShop;
       // slot.SetItem(itemInstance, price, item);

        //Add Shop Item Handler here
       // ShopItemHandler handler = itemInstance.AddComponent<ShopItemHandler>();
        
    }

    public void AddItemToShop(int itemID, int quantity)
    {
        if (!currentShop) return;
       // currentShop.AddToStock(itemID, quantity);
    }

    public bool RemoveItemFromShop(int itemID, int quantity)
    {
        if (!currentShop) return false;
        //bool success = currentShop.RemoveFromShopStock(itemID, quantity);
        //if (success) RefreshShopDisplay();
        //return success;
        return true;
    }




}

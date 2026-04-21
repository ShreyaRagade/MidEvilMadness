//using System.Collections.Generic;
//using NUnit.Framework;
//using Unity.VisualScripting;
//using UnityEngine;


//    public class ShopNPC : MonoBehaviour, IInteractable
//    {

//        public string shopID = "SpikeThePimp";
//        public string shopName = "Spike the Pimp's Shop";

//        public List<ShopStockItem> defaultShopStock = new();
//        private List<ShopStockItem> currentShopStock = new();

//        private bool isInitialized = false;

//        public NPC NPC;

//        [System.Serializable]
//        public class ShopStockItem
//        {
//            public int generalItemID;
//            public int quantity;
//        }

//        void Start()
//        {
//            InitializeShop();
//            NPC = GetComponent<NPC>();
//        }

//        private void InitializeShop()
//        {
//            if (isInitialized) return;

//            currentShopStock = new List<ShopStockItem>();

//            Dictionary<int, int> itemCounts = UseInventoryController.Instance.GetItemCounts();

//            //Default stock - overwritten by save system if save exists
//            foreach (var item in defaultShopStock)
//            {

//                currentShopStock.Add(new ShopStockItem
//                {
//                    generalItemID = item.generalItemID,
//                    quantity = 1 //UseInventoryController.Instance.itemsCountCache[item.generalItemID]
//                });
//            }

//            isInitialized = true;
//        }
//        public bool CanInteract()
//        {
//            //Can check your daytracker or whatever. for now its open at all times
//            return true;
//        }

//        public void Interact()
//        {


//            if (ShopController.ShopControllerInstance == null)
//            {

//                return;
//            }

//            if (ShopController.ShopControllerInstance.shopPanel.activeSelf) //is the panel visible right now?
//            {
//                //ShopController.ShopControllerInstance.CloseShop();
//            }
//            else
//            {
//                if (NPC != null)
//                {
//                    NPC.Interact();
//                    if (NPC.internalIsDialogueComplete)
//                    {
//                        ShopController.ShopControllerInstance.OpenShop(this);
//                    }
//                    NPC.internalIsDialogueComplete = false;

//                }
//                else
//                {
//                    ShopController.ShopControllerInstance.OpenShop(this);
//                }

//            }


//        }



//        public List<ShopStockItem> GetCurrentStock()
//        {
//            return currentShopStock;
//        }

//        public void SetStock(List<ShopStockItem> stock)
//        {
//            currentShopStock = stock;
//        }

//        public void AddToStock(int itemID, int quantity)
//        {
//            ShopStockItem existing = currentShopStock.Find(s => s.generalItemID == itemID);
//            if (existing != null)
//            {
//                existing.quantity += quantity;
//            }
//            else
//            {
//                currentShopStock.Add(new ShopStockItem { generalItemID = itemID, quantity = quantity });
//            }

//        }


//        public bool RemoveFromShopStock(int itemID, int quantity)
//        {
//            ShopStockItem existing = currentShopStock.Find(s => s.generalItemID == itemID);
//            if (existing != null && existing.quantity >= quantity)
//            {
//                existing.quantity -= quantity;
//                return true;
//            }
//            return false;


//        }
//    }


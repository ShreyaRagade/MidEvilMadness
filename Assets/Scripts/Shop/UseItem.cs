using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory/Equipment")]
public class UseItem : ScriptableObject
{
    public string itemName;
    public int generalItemID;
    public string uniqueItemID;

    public GameObject itemPrefab; //prolly get rid of this

    public int HP;

    [TextArea(1, 5)]
    public string itemDescription; 

    //Shop
    public int buyPrice = 10;

    [Range(0, 1)]
    public float sellPriceMultiplier = 0.5f;
    private void OnEnable()
    {

        if (string.IsNullOrEmpty(uniqueItemID))
        {
            uniqueItemID = itemName + Guid.NewGuid().ToString();

        }
    }
    public virtual void Use()
    { 
        Health.HealthInstance.healthAmount += HP;
    }

    


}



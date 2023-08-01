using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
    }

    public void AddItem(ItemData item)
    {
        if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            if (value.StackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(item);
            }
            else
                value.RemoveStack();
        }
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ItemData newItem = inventoryItems[inventoryItems.Count - 1].ItemData;

            RemoveItem(newItem);
        }
            
    }*/
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.Progress;

[Serializable]
public struct Item
{
    public string itemName;
    public int capacity;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            FillInventory(items[i]);
        }
        FindObjectOfType<UIManager>().RefreshQ();
    }

    public void FillInventory(Item item)
    {
        Item toChange = items[FindItem(item.itemName)];
        toChange.quantity = toChange.capacity;
        items[FindItem(item.itemName)] = toChange;
    }

    public void TakeIngredient(Item item, int q)
    {
        if (CanTakeIngredient(item, q))
        {
            Item toChange = items[FindItem(item.itemName)];
            toChange.quantity -= q;
            items[FindItem(item.itemName)] = toChange;
        }
    }

    public bool CanTakeIngredient(Item item, int q)
    {
        if (q < items[FindItem(item.itemName)].quantity) return true;
        else return false;
    }

    public bool IsFilled(Item item)
    {
        return item.quantity == item.capacity;
    }

    public int FindItem(string searchName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (searchName == items[i].itemName)
            {
                return i;
            }
        }

        return -1;
    }

    public IEnumerator actionFill(Item item)
    {
        yield return null;
        FillInventory(item);
    }
}

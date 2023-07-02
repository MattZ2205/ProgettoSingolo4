using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Item
{
    public string itemName;
    public int capacity;
    int quantity;

    public void FillInventory()
    {
        quantity = capacity;
    }

    public bool TakeIngredient(int q)
    {
        if (q > quantity) return false;
        else
        {
            quantity -= q;
            return true;
        }
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;

    private void Start()
    {
        foreach (Item item in items)
        {
            item.FillInventory();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storage"))
        {
            StartCoroutine(actionFill(other.transform.name));
        }
    }

    IEnumerator actionFill(string itemToFIll)
    {
        yield return null;
        items[FindItem(itemToFIll)].FillInventory();
    }
}

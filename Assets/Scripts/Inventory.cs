using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Item
{
    public string itemName;
    public int capacity;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;

    void Start()
    {
         
    }

    void Update()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[Serializable]
public struct Ingredient
{
    public Item ing;
    public int quantity;
}

[Serializable]
public struct Plate
{
    public string namePlate;

    public List<Ingredient> ingredients;

    public float cookingTime;
}

public class CookManager : MonoBehaviour
{
    [SerializeField] List<Plate> plates = new List<Plate>();

    public delegate void PlateOrdered(Plate plate);
    public static PlateOrdered onOrdination;

    int nPlate;

    private void OnEnable()
    {
        Cooking.onPlateCompleted += Order;
    }

    private void OnDisable()
    {
        Cooking.onPlateCompleted -= Order;
    }

    private void Start()
    {
        Order();
    }

    void Order()
    {
        onOrdination?.Invoke(plates[UnityEngine.Random.Range(0, plates.Count)]);
    }
}

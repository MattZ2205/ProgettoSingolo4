using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Inventory inv;
    [SerializeField] List<TMP_Text> quantityText;
    [SerializeField] List<Image> plateImages;

    private void OnEnable()
    {
        CookManager.onOrdination += printPlate;
        Cooking.onPlateCompleted += ClearUI;
    }

    private void OnDestroy()
    {
        CookManager.onOrdination -= printPlate;
        Cooking.onPlateCompleted -= ClearUI;
    }

    void ClearUI()
    {
        for (int i = 0; i < plateImages.Count; i++)
        {
            plateImages[i].gameObject.SetActive(false);
        }
    }

    void printPlate(Plate p)
    {
        for (int i = 0; i < plateImages.Count; i++)
        {
            if (p.namePlate == plateImages[i].transform.name)
            {
                plateImages[i].gameObject.SetActive(true);
                return;
            }
        }
    }

    public void RefreshQ()
    {
        for (int i = 0; i < quantityText.Count; i++)
        {
            quantityText[i].text = inv.items[i].quantity.ToString() + " / " + inv.items[i].capacity.ToString();
        }
    }
}

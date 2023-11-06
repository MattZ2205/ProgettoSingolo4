using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Routines : MonoBehaviour
{
    [SerializeField] UIManager UIM;
    [SerializeField] Image cookFillable;
    [SerializeField] Image fillFillable;
    [SerializeField] float timeFill;

    [HideInInspector] public bool isC = false;
    [HideInInspector] public bool isF = false;
    [HideInInspector] public bool filling = false;

    public void Cook(float t)
    {
        StartCoroutine(Cooking(t));
    }

    public void Fill()
    {
        if (!filling) StartCoroutine(Filling());
    }

    IEnumerator Filling()
    {
        filling = true;
        fillFillable.gameObject.SetActive(true);
        float fillQuant = 0f;
        while (fillFillable.fillAmount < 1f)
        {
            fillQuant += Time.deltaTime;
            fillFillable.fillAmount = fillQuant / timeFill;
            yield return null;
        }
        isF = true;
        fillFillable.gameObject.SetActive(false);
        fillFillable.fillAmount = 0;
        UIM.RefreshQ();
    }

    IEnumerator Cooking(float time)
    {
        cookFillable.gameObject.SetActive(true);
        float fillQuant = 0f;
        while (cookFillable.fillAmount < 1f)
        {
            fillQuant += Time.deltaTime;
            cookFillable.fillAmount = fillQuant / time;
            yield return null;
        }
        isC = true;
        cookFillable.gameObject.SetActive(false);
        cookFillable.fillAmount = 0;
        UIM.RefreshQ();
    }
}

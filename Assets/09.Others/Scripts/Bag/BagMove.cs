using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BagMove : MonoBehaviour, IDragHandler
{
    //public Canvas canvas;
    RectTransform curentRect;

    void Awake()
    {
        curentRect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        curentRect.anchoredPosition += eventData.delta;
    }
}

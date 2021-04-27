using UnityEngine;
using UnityEngine.EventSystems;
public class cBagMove : MonoBehaviour, IDragHandler
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

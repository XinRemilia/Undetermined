using UnityEngine;
using UnityEngine.EventSystems;
public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParemt;
    public int currentItemID;
    private int checkItemID;

    InventoryClassification startInventory;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParemt = transform.parent;
        currentItemID = originalParemt.GetComponent<Slot>().slotID;
        transform.SetParent(transform.root);
        transform.position = eventData.position;
        startInventory = originalParemt.GetComponent<Slot>().whichInventory;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject dragEndGameObject = eventData.pointerCurrentRaycast.gameObject ?? null;

        if (dragEndGameObject && dragEndGameObject.CompareTag("SlotCheck"))
        {
            Transform dragEndTransform = dragEndGameObject.transform;
            switch (dragEndTransform.name)
            {
                case "SlotItem":
                    transform.SetParent(dragEndTransform.parent.parent);
                    transform.position = dragEndTransform.parent.parent.position;

                    checkItemID = dragEndTransform.parent.GetComponentInParent<Slot>().slotID;
                    ExchangeItemCheck(dragEndTransform.parent.GetComponent<Slot>().whichInventory);

                    dragEndTransform.transform.parent.SetParent(originalParemt);
                    dragEndTransform.transform.parent.position = originalParemt.position;
                    break;
                case "Slot(Clone)":
                    transform.SetParent(dragEndTransform);
                    transform.position = dragEndTransform.position;

                    checkItemID = dragEndTransform.GetComponentInParent<Slot>().slotID;
                    ExchangeItemCheck(dragEndTransform.GetComponent<Slot>().whichInventory);

                    break;
            }
        }
        else
        {
            transform.SetParent(originalParemt);
            transform.position = originalParemt.position;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        InventoryManager.RefreshItem();
    }

    /// <summary>
    /// チェンジのアイテムチェック
    /// </summary>
    /// <param name="end">ドラッグの最後の場所</param>
    private void ExchangeItemCheck(InventoryClassification end)
    {
        if (startInventory == end)//同じ
        {
            foreach (var datas in InventoryManager.instance.inventoryList)
            {
                if (datas.inventoryType == startInventory)
                {
                    ExchangeItemData(datas);
                }
            }
        }
        else
        {
            Inventory startData = null, endData = null;
            foreach (var datas in InventoryManager.instance.inventoryList)
            {
                if(datas.inventoryType == startInventory)
                    startData = datas;

                if(datas.inventoryType == end)
                    endData = datas;
            }
            ExchangeItemData(startData, endData);
        }
    }

    private void ExchangeItemData(Inventory startData, Inventory endData = null)
    {
        var temp = startData.itemList[currentItemID];
        if (endData == null) 
        {
            startData.itemList[currentItemID] = startData.itemList[checkItemID];
            startData.itemList[checkItemID] = temp;
        }
        else
        {
            startData.itemList[currentItemID] = endData.itemList[checkItemID];
            endData.itemList[checkItemID] = temp;
        }
        
    }
}

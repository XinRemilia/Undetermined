using UnityEngine;
using UnityEngine.EventSystems;
public class cItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParemt;
    public int currentItemID;
    private int checkItemID;

    InventoryClassification startInventory;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParemt = transform.parent;
        currentItemID = originalParemt.GetComponent<cSlot>().slotID;
        transform.SetParent(transform.root);
        transform.position = eventData.position;
        startInventory = originalParemt.GetComponent<cSlot>().whichInventory;
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

                    checkItemID = dragEndTransform.parent.GetComponentInParent<cSlot>().slotID;
                    ExchangeItemCheck(dragEndTransform.parent.GetComponent<cSlot>().whichInventory);

                    dragEndTransform.transform.parent.SetParent(originalParemt);
                    dragEndTransform.transform.parent.position = originalParemt.position;
                    break;
                case "Slot(Clone)":
                    transform.SetParent(dragEndTransform);
                    transform.position = dragEndTransform.position;

                    checkItemID = dragEndTransform.GetComponentInParent<cSlot>().slotID;
                    ExchangeItemCheck(dragEndTransform.GetComponent<cSlot>().whichInventory);

                    break;
            }
        }
        else
        {
            transform.SetParent(originalParemt);
            transform.position = originalParemt.position;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        cInventoryManager.RefreshItem();
    }

    /// <summary>
    /// チェンジのアイテムチェック
    /// </summary>
    /// <param name="end">ドラッグの最後の場所</param>
    private void ExchangeItemCheck(InventoryClassification end)
    {
        if (startInventory == end)//同じ
        {
            foreach (var datas in cInventoryManager.instance.inventoryList)
            {
                if (datas.inventoryType == startInventory)
                {
                    ExchangeItemData(datas);
                }
            }
        }
        else
        {
            cInventory startData = null, endData = null;
            foreach (var datas in cInventoryManager.instance.inventoryList)
            {
                if(datas.inventoryType == startInventory)
                    startData = datas;

                if(datas.inventoryType == end)
                    endData = datas;
            }
            ExchangeItemData(startData, endData);
        }
    }

    private void ExchangeItemData(cInventory startData, cInventory endData = null)
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

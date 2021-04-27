using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class cSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotID;

    public cItem slotItem;
    public Image slotImage;
    public Text slotNum;

    public GameObject itemInSlot;

    public InventoryClassification whichInventory;
    /// <summary>
    /// マウス入る
    /// </summary>
    /// <param name="eventData">PointerEventData</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotItem == null || whichInventory != InventoryClassification.PlayerBag)
            cInventoryManager.UpdateItemInfo();
        else
            cInventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }
    /// <summary>
    /// マウス出て
    /// </summary>
    /// <param name="eventData">PointerEventData</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        cInventoryManager.UpdateItemInfo();
    }

    public void SetupSlot(cItem item = null)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotItem = item;
        slotImage.sprite = item.itemSprite;
        slotNum.text = item.itemHeldNumber.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotID;

    public Item slotItem;
    public Image slotImage;
    public Text slotNum;

    public GameObject itemInSlot;

    public InventoryClassification whichInventory;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotItem == null || whichInventory != InventoryClassification.PlayerBag)
            InventoryManager.UpdateItemInfo();
        else
            InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.UpdateItemInfo();
    }

    public void SetupSlot(Item item = null)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItemOnWorld : MonoBehaviour
{
    public cItem thisItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// バックにアイテムを追加
    /// </summary>
    public void AddNewItem()
    {
        List<cInventory> temp = cInventoryManager.instance.inventoryList;
        int index = 0;
        if (!temp[index].itemList.Contains(thisItem) && !temp[index + 1].itemList.Contains(thisItem))//バックにこのアイテムなしの場合
        {
            for (int i = 0; i < temp[0].itemList.Count; i++)
            {
                if (temp[index].itemList[i] == null)
                {
                    thisItem.itemHeldNumber++;//+1
                    temp[index].itemList[i] = thisItem;
                    break;
                }
            }
        }
        else//バックにこのアイテムあるの場合
        {
            thisItem.itemHeldNumber++;//+1
        }
        cInventoryManager.RefreshItem();
    }
}


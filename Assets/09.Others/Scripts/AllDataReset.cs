using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDataReset : MonoBehaviour
{
    public List<Item> dbItemList = new List<Item>();
    public List<Inventory> dbInventoryList = new List<Inventory>();
    public int size = 12;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            foreach (Item item in dbItemList)
            {
                item.itemHeldNumber = 0;
            }

            foreach (Inventory inventory in dbInventoryList)
            {
                for (int i = 0; i < inventory.itemList.Count; i++)
                {
                    inventory.itemList[i] = null;
                }
            }
            InventoryManager.RefreshItem();
        }
    }
}

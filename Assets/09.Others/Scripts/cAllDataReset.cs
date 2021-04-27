using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAllDataReset : MonoBehaviour
{
    public List<cItem> dbItemList = new List<cItem>();
    public List<cInventory> dbInventoryList = new List<cInventory>();
    public int size = 12;


    public GameObject f1, f2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            foreach (cItem item in dbItemList)
            {
                item.itemHeldNumber = 1;
            }

            //foreach (cInventory inventory in dbInventoryList)
            //{
            //    for (int i = 0; i < inventory.itemList.Count; i++)
            //    {
            //        inventory.itemList[i] = null;
            //    }
            //}
            cInventoryManager.RefreshItem();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            f1.SetActive(!f1.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            f2.SetActive(!f2.activeSelf);
        }
    }
}

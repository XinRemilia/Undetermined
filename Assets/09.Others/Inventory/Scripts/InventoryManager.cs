using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Inventory> inventoryList = new List<Inventory>();
    public Dictionary<InventoryClassification, Transform> gridTransform = new Dictionary<InventoryClassification, Transform>();

    public GameObject slotPrefabe;
    public Text itemInfromation;

    public List<GameObject> slotsList = new List<GameObject>();

    const float imageSize = 65f;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

        gridTransform.Add(InventoryClassification.PlayerBag, transform.Find("PlayerBag/PlayerBag_Grid"));
        gridTransform.Add(InventoryClassification.PropsColumn, transform.Find("PropsColumn/PropsColumn_Grid"));
    }


    private void OnEnable()
    {
        RefreshItem();
        UpdateItemInfo();
    }
    /// <summary>
    /// アイテム説明更新
    /// </summary>
    /// <param name="itemDescription">更新の文字</param>
    public static void UpdateItemInfo(string itemDescription = "")
    {
        instance.itemInfromation.text = itemDescription;
    }

    /// <summary>
    /// バック中表示更新
    /// </summary>
    public static void RefreshItem()
    {
        foreach (var slotGrid in instance.gridTransform)
        {
            for (int i = 0; i < slotGrid.Value.childCount; i++)
            {
                if (slotGrid.Value.transform.childCount == 0)
                    break;

                Destroy(slotGrid.Value.transform.GetChild(i).gameObject);
            }
        }

        foreach (var slotData in instance.inventoryList)
        {
            InventoryClassification currInventory = slotData.inventoryType;
            int count = slotData.itemList.Count;

            instance.SetImageSize(out float setImageSize, currInventory);
            instance.slotsList.Clear();

            for (int i = 0; i < count; i++) 
            {
                instance.slotsList.Add(Instantiate(instance.slotPrefabe));
                instance.slotsList[i].transform.SetParent(instance.gridTransform[currInventory].transform, false);
                Slot tempSlot = instance.slotsList[i].GetComponent<Slot>();

                if (slotData.itemList[i] == null || slotData.itemList[i].itemHeldNumber < slotData.itemList[i].itemDefNumber)
                {
                    slotData.itemList[i] = null;
                }
                tempSlot.slotID = i;
                tempSlot.whichInventory = currInventory;
                tempSlot.slotImage.GetComponent<RectTransform>().sizeDelta = new Vector2(setImageSize, setImageSize);
                tempSlot.SetupSlot(slotData.itemList[i]);
            }
        }
        //for (int i = 0; i < instance.slotsList.Count; i++)
        //{
        //    Debug.Log(instance.slotsList[i].name);
        //}
    }

    private void SetImageSize(out float size, InventoryClassification currInventory)
    {
        switch (currInventory)
        {
            case InventoryClassification.PlayerBag:
                size = imageSize;
                break;
            case InventoryClassification.PropsColumn:
                size = imageSize - 20f;
                break;
            default: 
                size = imageSize; 
                break;
        }
    }

    public GameObject bagGameObject;
    private bool bagOn = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            bagOn = !bagOn;
            bagGameObject.SetActive(bagOn);
            bagGameObject.transform.position = transform.position;
            RefreshItem();
        }
    }
    public void PlayerExitButton()
    {
        bagOn = false;
        bagGameObject.SetActive(bagOn);
        bagGameObject.transform.position = transform.position;
        RefreshItem();
    }
}

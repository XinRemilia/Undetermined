using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class cInventory : ScriptableObject
{
    public List<cItem> itemList = new List<cItem>();
    public InventoryClassification inventoryType;
}
/// <summary>
/// 目録分類
/// </summary>
public enum InventoryClassification
{
    PlayerBag,//プレイヤーバック
    PlayerBag2,//2
    PropsColumn//道具欄
}
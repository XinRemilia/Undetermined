using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
    public InventoryClassification inventoryType;
}
public enum InventoryClassification
{
    PlayerBag,//プレイヤーバック
    PropsColumn//道具欄
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/New Item")]
public class cItem : ScriptableObject
{
    public string itemName;//アイテム名
    public Sprite itemSprite;//アイテム表示画像
    public int itemHeldNumber;//持つ数
    public int itemDefNumber = 1;
    public float coolDownTime;//使用間隔
    public GameObject itemPrefabe;
    [TextArea]
    public string itemInfo;//アイテム説明

   //public   ItemType //アイテムタイプ
}

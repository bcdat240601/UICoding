using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
    public ItemType Item;
}

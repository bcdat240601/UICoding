using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDataSO", menuName = "InventoryDataSO")]
public class InventoryDataSO : ScriptableObject
{
    [System.Serializable]
    public class InventoryData
    {
        public ItemType Item;
        public Sprite ImageSprite;
        public int quantity;
    }
    public List<InventoryData> InventoryDatas;
}

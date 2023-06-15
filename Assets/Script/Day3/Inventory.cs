using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : SetupBehaviour
{
    [SerializeField] protected List<SlotData> slotDatas;
    [SerializeField] protected InventoryDataSO inventoryDataSO;
    [SerializeField] protected List<ItemSO> listItem;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetInventorySO();
        GetSlotData();
        GetListItem();
    }

    protected virtual void GetListItem()
    {
        if (listItem.Count != 0) return;
        string path = "ItemSO";
        ItemSO[] itemSOList = Resources.LoadAll<ItemSO>(path);
        listItem = new List<ItemSO>(itemSOList);
        Debug.Log("Reset " + nameof(listItem) + " in " + GetType().Name);
    }

    protected virtual void GetSlotData()
    {
        if (slotDatas.Count != 0) return;
        SlotData[] slotDataArray = transform.Find("ItemList").GetComponentsInChildren<SlotData>();
        slotDatas = new List<SlotData>(slotDataArray);
        Debug.Log("Reset " + nameof(slotDatas) + " in " + GetType().Name);
    }

    protected virtual void GetInventorySO()
    {
        if (inventoryDataSO != null) return;
        string path = "InventorySO/InventoryDataSO";
        inventoryDataSO = Resources.Load<InventoryDataSO>(path);
        if (inventoryDataSO == null)
        {
            CreateInventoryDataSO();
            inventoryDataSO = Resources.Load<InventoryDataSO>(path);
        }
        Debug.Log("Reset " + nameof(inventoryDataSO) + " in " + GetType().Name);
    }

    protected virtual void CreateInventoryDataSO()
    {
        InventoryDataSO setting = ScriptableObject.CreateInstance<InventoryDataSO>();

        string path = "Assets/Resources/InventorySO/InventoryDataSO.asset";
        AssetDatabase.CreateAsset(setting, path);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }

    protected override void Awake()
    {
        base.Awake();
        LoadInventoryData();
    }
    protected virtual void LoadInventoryData()
    {
        for (int i = 0; i < slotDatas.Count; i++)
        {
            if (i >= inventoryDataSO.InventoryDatas.Count)
            {
                slotDatas[i].SetImage(null);
                slotDatas[i].SetTextQuantity(null);
                slotDatas[i].SetItemType(ItemType.None);
                continue;
            }
            slotDatas[i].SetImage(inventoryDataSO.InventoryDatas[i].ImageSprite);
            slotDatas[i].SetTextQuantity(inventoryDataSO.InventoryDatas[i].quantity.ToString());
            slotDatas[i].SetItemType(inventoryDataSO.InventoryDatas[i].Item);
        }
    }
    public virtual void FindSmallestIndexEmptySlot(InventoryDataSO.InventoryData inventoryData)
    {
        int i = 0;
        bool isFound = false;
        for (i = 0; i < slotDatas.Count; i++)
        {
            if (slotDatas[i].ItemImage.sprite == null)
            {
                isFound = true;
                break;
            }
        }
        if (!isFound) return;
        FillSlot(i, inventoryData);
    }
    public virtual void FindBiggestIndexNotEmpltSlot(InventoryDataSO.InventoryData inventoryData)
    {
        int indexSlot = 0;
        for (int i = 0; i < slotDatas.Count; i++)
        {
            if (slotDatas[i].ItemImage.sprite != null)
                indexSlot = i;
        }
        FillSlot(indexSlot, inventoryData);
    }
    public virtual void FillSlot(int slotIndex, InventoryDataSO.InventoryData inventoryData)
    {
        slotDatas[slotIndex].SetImage(inventoryData.ImageSprite);
        slotDatas[slotIndex].SetTextQuantity(inventoryData.quantity.ToString());
        slotDatas[slotIndex].SetItemType(inventoryData.Item);
    }
    public virtual ItemSO SelectRandomItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, listItem.Count);
        return listItem[randomIndex];
    }
    public virtual void AddItem()
    {
        ItemSO itemSO = SelectRandomItem();
        InventoryDataSO.InventoryData inventoryData = new InventoryDataSO.InventoryData();
        inventoryData.ImageSprite = itemSO.ItemImage;
        inventoryData.Item = itemSO.Item;
        inventoryData.quantity = 1;
        FindSmallestIndexEmptySlot(inventoryData);
    }
    public virtual void RemoveItem()
    {
        InventoryDataSO.InventoryData inventoryData = new InventoryDataSO.InventoryData();
        inventoryData.ImageSprite = null;
        inventoryData.Item = ItemType.None;
        inventoryData.quantity = 0;
        FindBiggestIndexNotEmpltSlot(inventoryData);
    }
}

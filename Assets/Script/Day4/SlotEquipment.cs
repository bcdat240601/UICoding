using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotEquipment : SlotContainer
{
    [SerializeField] protected Sprite defaultSprite;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        SetItemType();
    }
    protected virtual void Update()
    {
        SetDefaultImage();
    }

    protected virtual void SetDefaultImage()
    {
        Image image = GetComponent<Image>();
        if (defaultSprite == null)
        {
            Debug.LogError("defaultSprite is null");
            return;
        }
        if (itemImage.sprite != null)
        {
            image.sprite = null;
            image.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0f);
        }
        else
        {
            image.sprite = defaultSprite;
            image.gameObject.SetActive(true);
            image.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0.4f);
        }
    }

    protected virtual void SetItemType()
    {
        if (Item != ItemType.None) return;
        Enum.TryParse<ItemType>(transform.name,out item);
        Debug.Log("Reset " + nameof(Item) + " in " + GetType().Name);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragFromInventory dragFromInventory;
        dropped.TryGetComponent<DragFromInventory>(out dragFromInventory);
        if (dragFromInventory == null) return;
        InventoryDataSO.InventoryData inventoryData = new InventoryDataSO.InventoryData();
        inventoryData.ImageSprite = itemImage.sprite;
        if (dragFromInventory.SlotData.Item != Item) return;
        if (itemImage.sprite != null)
        {
            inventoryData.quantity = 1;
            inventoryData.Item = dragFromInventory.SlotData.Item;
            inventory.FindSmallestIndexEmptySlot(inventoryData);
            SetImage(dragFromInventory.SlotData.ItemImage.sprite);
            inventoryData.ImageSprite = null;
            inventoryData.quantity = 0;
            dragFromInventory.newSlotData = inventoryData;
            dragFromInventory.isSwap = true;
        }
        else
        {
            inventoryData.quantity = 0;
            dragFromInventory.newSlotData = inventoryData;
            dragFromInventory.isSwap = true;
            SetImage(dragFromInventory.SlotData.ItemImage.sprite);
        }
        
    }
}

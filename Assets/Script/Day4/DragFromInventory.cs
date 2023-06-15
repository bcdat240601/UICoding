using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragFromInventory : DraggableItem
{
    [SerializeField] protected SlotData slotData;
    public SlotData SlotData => slotData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetSlotData();
    }
    protected virtual void GetSlotData()
    {
        if (slotData != null) return;
        slotData = GetComponentInParent<SlotData>();
        Debug.Log("Reset " + nameof(slotData) + " in " + GetType().Name);
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        InventoryDataSO.InventoryData inventoryData = new InventoryDataSO.InventoryData();

        inventoryData.ImageSprite = slotData.ItemImage.sprite;       
        int.TryParse(slotData.Quantity.text, out inventoryData.quantity);
        inventoryData.Item = slotData.Item;

        newSlotData = inventoryData;
        base.OnBeginDrag(eventData);
        slotData.ItemImage.raycastTarget = false;
        slotData.Quantity.enabled = false;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        slotData.Quantity.enabled = true;
        slotData.ItemImage.raycastTarget = true;
    }
    public override void SwapItem()
    {
        base.SwapItem();
        slotData.SetImage(newSlotData.ImageSprite);
        slotData.SetTextQuantity(newSlotData.quantity.ToString());
        slotData.SetItemType(newSlotData.Item);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragFromEquipment : DraggableItem
{
    [SerializeField] protected SlotEquipment slotEquipment;
    public SlotEquipment SlotEquipment => slotEquipment;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetSlotEquipment();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        SlotEquipment.ItemImage.raycastTarget = false;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        SlotEquipment.ItemImage.raycastTarget = true;
    }
    protected virtual void GetSlotEquipment()
    {
        if (slotEquipment != null) return;
        slotEquipment = GetComponentInParent<SlotEquipment>();
        Debug.Log("Reset " + nameof(slotEquipment) + " in " + GetType().Name);
    }
    public override void SwapItem()
    {
        if (!isSwap) return;
        slotEquipment.SetImage(newSlotData.ImageSprite);
        isSwap = false;
    }
}

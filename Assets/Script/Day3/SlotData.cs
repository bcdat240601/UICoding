using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotData : SlotContainer, IDropHandler
{
    [SerializeField] protected Text quantity;
    public Text Quantity => quantity;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetText();
    }
    
    protected virtual void GetText()
    {
        if (quantity != null) return;
        quantity = GetComponentInChildren<Text>();
        Debug.Log("Reset " + nameof(quantity) + " in " + GetType().Name);
    }    
    public virtual void SetTextQuantity(string textQuantity)
    {
        quantity.text = textQuantity;
        if (textQuantity == "0")
            quantity.text = null;
    }
    public virtual void SetItemType(ItemType itemType)
    {
        item = itemType;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;        
        DragFromInventory dragFromInventory;
        dropped.TryGetComponent<DragFromInventory>(out dragFromInventory);
        DragFromEquipment dragFromEquipment;
        dropped.TryGetComponent<DragFromEquipment>(out dragFromEquipment);
        InventoryDataSO.InventoryData inventoryData = new InventoryDataSO.InventoryData();
        inventoryData.ImageSprite = itemImage.sprite;
        if (dragFromInventory != null)
        {
            if (quantity.text == null)
                inventoryData.quantity = 0;
            else
                int.TryParse(quantity.text,out inventoryData.quantity);
            inventoryData.Item = item;
            dragFromInventory.newSlotData = inventoryData;
            dragFromInventory.isSwap = true;
            SetImage(dragFromInventory.SlotData.ItemImage.sprite);
            SetTextQuantity(dragFromInventory.SlotData.Quantity.text);
            SetItemType(dragFromInventory.SlotData.item);
        }
        else
        {
            if(itemImage.sprite == null)
            {
                inventoryData.Item = item;
                dragFromEquipment.newSlotData = inventoryData;
                dragFromEquipment.isSwap = true;
                SetImage(dragFromEquipment.SlotEquipment.ItemImage.sprite);
                SetTextQuantity("1");
                SetItemType(dragFromEquipment.SlotEquipment.Item);
            }
            else
            {                
                inventoryData.ImageSprite = dragFromEquipment.SlotEquipment.ItemImage.sprite;
                inventoryData.Item = dragFromEquipment.SlotEquipment.Item;
                inventory.FindSmallestIndexEmptySlot(inventoryData);
                inventoryData.ImageSprite = null;
                dragFromEquipment.isSwap = true;
            }
            
        }  
    }
}

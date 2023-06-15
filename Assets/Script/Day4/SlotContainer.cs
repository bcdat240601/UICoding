using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SlotContainer : SetupBehaviour, IDropHandler
{
    [SerializeField] protected ItemType item;
    public ItemType Item => item;
    [SerializeField] protected Image itemImage;
    public Image ItemImage => itemImage;
    [SerializeField] protected Inventory inventory;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetImage();
        GetInventory();
    }

    protected virtual void GetInventory()
    {
        if (inventory != null) return;
        inventory = FindObjectOfType<Inventory>();
        Debug.Log("Reset " + nameof(inventory) + " in " + GetType().Name);
    }
    

    public virtual void SetImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
        if (sprite == null)
            itemImage.gameObject.SetActive(false);
        else
            itemImage.gameObject.SetActive(true);
    }
    protected virtual void GetImage()
    {
        if (itemImage != null) return;
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        if (itemImage.sprite == null)
            itemImage.gameObject.SetActive(false);
        Debug.Log("Reset " + nameof(itemImage) + " in " + GetType().Name);
    }
    public abstract void OnDrop(PointerEventData eventData);
}

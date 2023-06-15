using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableItem : SetupBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected Vector2 rectPosition;   
    public InventoryDataSO.InventoryData newSlotData;
    public bool isSwap = false;
    [SerializeField] protected Transform selfParent;
    [SerializeField] protected Transform inventoryUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetRectTransform();
        GetInventoryUITransform();
    }
    protected virtual void GetRectTransform()
    {
        if (rectTransform != null) return;
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("Reset " + nameof(rectTransform) + " in " + GetType().Name);
    }
    protected virtual void GetInventoryUITransform()
    {
        if (inventoryUI != null) return;
        inventoryUI = GameObject.Find("InventoryUI").transform;
        Debug.Log("Reset " + nameof(inventoryUI) + " in " + GetType().Name);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectPosition = rectTransform.anchoredPosition;
        selfParent = transform.parent;
        transform.SetParent(inventoryUI);
        transform.SetAsLastSibling();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {        
        transform.SetParent(selfParent);
        rectTransform.anchoredPosition = rectPosition;
        SwapItem();                      
    }   
    public virtual void SwapItem()
    {
        if (!isSwap) return;        
        isSwap = false;
    }
}

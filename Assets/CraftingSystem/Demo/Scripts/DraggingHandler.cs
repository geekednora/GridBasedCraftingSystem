using System;
using System.Collections.Generic;
using CraftingSystem.Core;
using CraftingSystem.Demo.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CraftingSystem.Demo.Scripts
{
    public class DraggingHandler : MonoBehaviour
    {
        public Item item;
        private Image _image;
        public EventSystem eventSystem;
        private ItemSlot _itemSlot;
        
        public event Action<ItemSlot> OnItemDropped;
        private bool Dragging { get; set; } = false;

        private void Awake()
        {
            _image = GetComponent<Image>();
            eventSystem = FindObjectOfType<EventSystem>();
        }

        private void Update()
        {
            transform.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                OnDragBegin();
            }
            else if (Input.GetMouseButtonUp(0) && Dragging)
            {
                OnDragEnd();
            }
        }
        
        private void OnDragBegin()
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                ItemSlot itemSlot = result.gameObject.GetComponent<ItemSlot>();
                if (itemSlot != null)
                {
                    TakeItemFromSlot(itemSlot);
                    break;
                }
            }
        }
        
        private void OnDragEnd()
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                ItemSlot itemSlot = result.gameObject.GetComponent<ItemSlot>();
                if (itemSlot != null)
                {
                    DropItemInSlot(itemSlot);
                    return;
                }
            }
            Dragging = false;
            _image.sprite = null;
            _image.enabled = false;
        }

        private void DropItemInSlot(ItemSlot itemSlot)
        {
            if (itemSlot == _itemSlot)
            {
                // If the item slot is the same as the one we are dragging, do nothing
                _image.sprite = null;
                _image.enabled = false;
                
                Dragging = false;
                return;
            }
            if (itemSlot.Item == null)
            {
                // If the item slot is empty, just drop the item in it
                itemSlot.Count = _itemSlot.Count;
                
                itemSlot.Item = item;
                _itemSlot.Item = null;
            }
            else if (itemSlot.Item.name == item.name)
            {
                // If the item slot has the same item, add the count to the item slot
                itemSlot.Count += _itemSlot.Count;
                _itemSlot.Item = null;
            }
            else
            {
                // If the item slot has a different item, swap the items
                Item temp = itemSlot.Item;
                itemSlot.Item = item;
                _itemSlot.Item = temp;
                
                (itemSlot.Count, _itemSlot.Count) = (_itemSlot.Count, itemSlot.Count);
            }

            // Update the graphics of the cursor
            _image.sprite = null;
            _image.enabled = false;

            Dragging = false;
            
            // Calling event to update the crafting system
            OnItemDropped?.Invoke(_itemSlot);
        }
        
        private void TakeItemFromSlot(ItemSlot itemSlot)
        {
            

            if (itemSlot.Item != null)
            {
                item = itemSlot.Item;
                _itemSlot = itemSlot;
                _image.sprite = item.icon;
                _image.enabled = true;
                
                Dragging = true;
            }
            else
            {
                _image.sprite = null;
                _image.enabled = false;
            }

            Debug.Log("Hit item slot: " + itemSlot.GetItemName());
        }
    }
    
}
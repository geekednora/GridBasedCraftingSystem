using System;
using CraftingSystem.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
namespace CraftingSystem.Demo.Scripts.InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Item startingItem = null;
        private Item _item;
        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                UpdateGraphic();
            }
        }
        
        
        public Inventory inventory = null;

        private RectTransform _rectTransform;
        
        [SerializeField] private TextMeshProUGUI descriptionText;

        [SerializeField] private TextMeshProUGUI nameText;

        [SerializeField] private Image itemIcon;

        [SerializeField] private TMP_Text itemCountText;

        [SerializeField] private int count = 0;

        public int Count
        {
            get => count;
            set
            {
                count = value;
                UpdateGraphic();
            }
        }


        // Start is called before the first frame update
        private void Start()
        {
            _item = startingItem;
            UpdateGraphic();
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Item != null)
            {
                descriptionText.text = Item.description;
                nameText.text = Item.name;
            }
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            if (Item != null)
            {
                descriptionText.text = "";
                nameText.text = "";
            }
        }


        //Change Icon and count
        private void UpdateGraphic()
        {
            if (count < 1 || Item == null)
            {
                _item = null;
                itemIcon.gameObject.SetActive(false);
                itemCountText.gameObject.SetActive(false);
            }
            else
            {
                //set sprite to the one from the item
                itemIcon.sprite = Item.icon;
                itemIcon.gameObject.SetActive(true);
                itemCountText.gameObject.SetActive(true);
                itemCountText.text = count.ToString();
            }
        }

        public void UseItemInSlot()
        {
            if (CanUseItem())
            {
                Item.Use();
                if (Item.isConsumable) Count--;
            }
        }

        private bool CanUseItem()
        {
            return Item != null && count > 0;
        }

        // Check if the slot has an item
        public bool HasItem()
        {
            return !string.IsNullOrEmpty(nameText.ToString()) && GetItemCount() > 0;
        }

        // Get the name of the item in the slot
        public string GetItemName()
        {
            return nameText.ToString();
        }

        // Get the count of the item in the slot
        public int GetItemCount()
        {
            return Count;
        }

        // Remove the item from the slot
        public void RemoveFromSlot()
        {
            Item = null;
            UpdateGraphic();
        }

        // Add an item to the slot
        public void AddItemToSlot(Item item)
        {
            this.Item = item;
            UpdateGraphic();
        }

        // Remove an item from the slot
        public void RemoveItemFromSlot()
        {
            Item = null;
            UpdateGraphic();
        }
    }
}
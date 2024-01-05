using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
namespace CraftingSystem.Demo.Scripts.InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Item item = null;
        public Inventory inventory = null;

        [SerializeField] private TextMeshProUGUI descriptionText;

        [SerializeField] private TextMeshProUGUI nameText;

        [SerializeField] private Image itemIcon;

        [SerializeField] private TextMeshProUGUI itemCountText;

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
            UpdateGraphic();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (item != null)
            {
                descriptionText.text = item.description;
                nameText.text = item.name;
            }
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            if (item != null)
            {
                descriptionText.text = "";
                nameText.text = "";
            }
        }


        //Change Icon and count
        private void UpdateGraphic()
        {
            if (count < 1)
            {
                item = null;
                itemIcon.gameObject.SetActive(false);
                itemCountText.gameObject.SetActive(false);
            }
            else
            {
                //set sprite to the one from the item
                itemIcon.sprite = item.icon;
                itemIcon.gameObject.SetActive(true);
                itemCountText.gameObject.SetActive(true);
                itemCountText.text = count.ToString();
            }
        }

        public void UseItemInSlot()
        {
            if (CanUseItem())
            {
                item.Use();
                if (item.isConsumable) Count--;
            }
        }

        private bool CanUseItem()
        {
            return item != null && count > 0;
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
        public void ClearSlot()
        {
            item = null;
            UpdateGraphic();
        }

        // Add an item to the slot
        public void AddItemToSlot(Item item)
        {
            this.item = item;
            UpdateGraphic();
        }

        // Remove an item from the slot
        public void RemoveItemFromSlot()
        {
            item = null;
            UpdateGraphic();
        }
    }
}
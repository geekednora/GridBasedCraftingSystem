using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
namespace CraftingSystem.Demo.Scripts.InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Item item = null;
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
        
        public void StackItems(ItemSlot other)
        {
            if (item.name != other.name)
            {
                Debug.LogException(new UnityException("Cannot stack items of different types"));
            }
            
            Count += other.Count;
            Destroy(other.gameObject);
        }


        // Start is called before the first frame update
        private void Start()
        {
            UpdateGraphic();
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
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
        public void RemoveFromSlot()
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                var tempParent = GameObject.FindGameObjectWithTag("TempParent");
                if (tempParent == null)
                    throw new MissingReferenceException("Editor can't find TempParent");

                _rectTransform.SetParent(tempParent.transform);

                RemoveFromSlot();

                itemIcon.raycastTarget = false;
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                var tempParent = GameObject.FindGameObjectWithTag("TempParent");
                if (tempParent == null)
                    throw new MissingReferenceException("Editor can't find TempParent");
                _rectTransform.SetParent(tempParent.transform);

                var slot = this;
                RemoveFromSlot();
                
                if (count > 1)
                {
                    
                    var newDraggableItem = Instantiate(inventory.itemPrefab, transform.position, Quaternion.identity).GetComponent<ItemSlot>();
                    newDraggableItem.AddItemToSlot(item);
                    newDraggableItem.count = count - 1;
                    slot.AddItemToSlot(newDraggableItem.item);
                    newDraggableItem.GoToSlot();
                    Count = 1;
                }

                itemIcon.raycastTarget = false;
            }
        }
        
        public void GoToSlot()
        {
            if (this == null)
                return;

            _rectTransform.SetParent(this.transform);
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemIcon.raycastTarget = true;
            if (this != null)
            {
                inventory.AddItem(this.name, this.count);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
    }
}
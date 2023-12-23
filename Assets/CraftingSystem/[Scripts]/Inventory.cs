using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem
{
    public class Inventory : MonoBehaviour
    {
        List<ItemSlot> _itemSlots = new List<ItemSlot>();
        private ItemSlot _itemSlot = _inventoryPanel.AddComponent<ItemSlot>();

        [SerializeField] static GameObject _inventoryPanel;

        private void Awake()
        {
            _itemSlot = GetComponent<ItemSlot>();
        }

        void Start()
        {
            //Read all itemSlots as children of inventory panel
            _itemSlots = new List<ItemSlot>(_inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
        }
    
        public void AddItem(string itemName, int itemCount)
        {
            Item item = null;
            item.name = itemName;
            _itemSlot.Count = itemCount;
        }
    }
}




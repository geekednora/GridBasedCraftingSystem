using System.Collections.Generic;
using UnityEngine;
// Crafting System

namespace CraftingSystem.Core
{
    public class Inventory : MonoBehaviour
    {
        List<ItemSlot> itemSlots = new List<ItemSlot>();
        
        [SerializeField]
        public static GameObject InventoryPanel;
        private ItemSlot itemSlot = InventoryPanel.AddComponent<ItemSlot>();
        
        private void Awake()
        {
            //Read all itemSlots as children of inventory panel
            itemSlots = new List<ItemSlot>(InventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
        }

        private void Start()
        {
           
            
            
        }

        public void AddItem(string itemName, int itemCount)
        {
            Item item = null;
            item.name = itemName;
            itemSlot.Count = itemCount;
        }
    }
}
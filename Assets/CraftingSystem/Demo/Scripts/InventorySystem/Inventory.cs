using System.Collections.Generic;
using UnityEngine;

/*
 * Inventory class
 * This class is responsible for managing the inventory.
 */

namespace CraftingSystem.Demo.Scripts.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        // Inventory items in a list.
        private List<ItemSlot> itemSlots = new();


        [SerializeField] private GameObject InventoryPanel;
        [SerializeField] private GameObject itemPrefab;

        private void Awake()
        {
            //Read all itemSlots as children of inventory panel
            itemSlots = new List<ItemSlot>(InventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
        }

        // OnEnable -> CreateItems
        private void OnEnable()
        {
            //CreateItems();
        }

        // Create items in the inventory.
        private void CreateItems()
        {
            var slotIndex = 0;
            foreach (var item in itemSlots)
            {
                if (slotIndex >= itemSlots.Count)
                {
                    // Edit starting configuration of inventory
                    print("Inventory is full");
                    break;
                }

                var gameItem = Instantiate(itemPrefab);
                var inventoryItem = gameItem.GetComponent<ItemSlot>();
                inventoryItem.AddItemToSlot(item.item);
                itemSlots[slotIndex].AddItemToSlot(inventoryItem.item);
                slotIndex++;
            }
        }

        // Add items to the inventory.
        public void AddItem(string itemName, int itemCount)
        {
            ItemSlot emptySlot = null;

            foreach (var slot in itemSlots)
                if (slot.item == null)
                {
                    if (emptySlot == null)
                        emptySlot = slot;
                }
                else if (slot.item.name == itemName)
                {
                    slot.Count += itemCount;
                    return;
                }

            if (emptySlot != null) return;
        }

        // Remove items from the inventory.
        public void RemoveItem(string itemName, int itemCount)
        {
            foreach (var slot in itemSlots)
                if (slot.item != null && slot.item.name == itemName)
                {
                    slot.Count -= itemCount;
                    return;
                }
        }
    }
}
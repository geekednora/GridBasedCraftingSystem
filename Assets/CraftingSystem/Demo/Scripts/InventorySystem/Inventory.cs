using System.Collections;
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
        List<ItemSlot> itemSlots = new List<ItemSlot>();

        [SerializeField] GameObject InventoryPanel;
        [SerializeField] public GameObject itemPrefab;

        private void Awake()
        {
            //Read all itemSlots as children of inventory panel
            itemSlots = new List<ItemSlot>( InventoryPanel.transform.GetComponentsInChildren<ItemSlot>() );
        }
    }
}
using System.Collections.Generic;
using CraftingSystem.Demo.Scripts.InventorySystem;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class CraftingSystem : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private CraftingGrid _craftingGrid;
        [SerializeField] private List<Recipe> recipes;

        private void Start()
        {
        }
        
        // TODO: Implement crafting logic
        // Crafting logic:
        // 1. Check if there is a recipe for the items in the crafting grid
        // 2. If there is a recipe, check if there is enough items in the inventory
        // 3. If there is enough items in the inventory, remove the items from the inventory and add the crafted item to the inventory
        // 4. If there is not enough items in the inventory, do not craft the item and display a message to the player
        
        public void CraftItem()
        {
        }
        
    }
}
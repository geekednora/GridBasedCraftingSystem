using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem
{
    public class CraftingGrid : MonoBehaviour
    {
        public List<ItemSlot> slots = new(); // Assuming you have a list of slots

        public CraftingSystem craftingSystem; // Reference to your crafting system

        // Check the crafting grid for a valid recipe
        public void CheckCraftingGrid()
        {
            foreach (var recipe in craftingSystem.craftingRecipes)
                if (IsRecipeMatch(recipe))
                {
                    craftingSystem.CraftItem(recipe.resultItemName);
                    ClearCraftingGrid();
                    return;
                }
        }

        // Check if the crafting grid matches a given recipe
        private bool IsRecipeMatch(CraftingRecipe recipe)
        {
            for (var i = 0; i < 9; i++)
                if (!IsSlotMatch(recipe.ingredients[i], slots[i]))
                    return false;

            return true;
        }

        // Check if a slot matches the required ingredient
        private bool IsSlotMatch(Ingredient ingredient, ItemSlot slot)
        {
            if (slot == null || slot.GetItemName() != ingredient.itemName ||
                slot.GetItemCount() < ingredient.amount) return false;

            return true;
        }

        // Clear the crafting grid after crafting
        private void ClearCraftingGrid()
        {
            foreach (var slot in slots)
                if (slot != null)
                    slot.ClearSlot();
        }
    }
}
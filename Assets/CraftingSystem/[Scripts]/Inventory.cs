using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    GameObject inventoryPanel;

    void Start()
    {
        //Read all itemSlots as children of inventory panel
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
        );
    }

    public bool CraftItem(List<Item> recipe, Item outputItem, int outputCount)
    {
        // Check if the inventory contains the required items for the recipe
        if (CheckRecipe(recipe))
        {
            // Remove items from the inventory based on the recipe
            RemoveItemsFromInventory(recipe);

            // Add the crafted item to the inventory
            AddItemToInventory(outputItem, outputCount);

            return true;
        }
        else
        {
            Debug.Log("Crafting failed. Missing required items.");
            return false;
        }
    }

    private bool CheckRecipe(List<Item> recipe)
    {
        // Check if the inventory contains the required items for the recipe
        foreach (Item recipeItem in recipe)
        {
            if (!ContainsItem(recipeItem))
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveItemsFromInventory(List<Item> items)
    {
        // Remove items from the inventory based on the recipe
        foreach (Item recipeItem in items)
        {
            RemoveItemFromInventory(recipeItem);
        }
    }

    private void AddItemToInventory(Item item, int count)
    {
        // Add the crafted item to the inventory
        for (int i = 0; i < itemSlots.Count; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot.item == null)
            {
                slot.item = item;
                slot.Count = count;
                break;
            }
        }
    }

    private bool ContainsItem(Item item)
    {
        // Check if the inventory contains a specific item
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == item && slot.Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    private void RemoveItemFromInventory(Item item)
    {
        // Remove a specific item from the inventory
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == item && slot.Count > 0)
            {
                slot.Count--;
                break;
            }
        }
    }

}




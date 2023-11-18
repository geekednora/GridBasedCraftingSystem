using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
{
    public enum ItemType
    {
        Consumable,
        Weapon,
        Tool,
        // Add more item types based on your game's needs
    }

    public ItemType itemType;

    public Sprite icon;
    [TextArea]
    public string description = "";
    public bool isConsumable = false;

    // Add a variable to store the maximum stack size for the item
    public int maxStackSize = 1;

    // Add a variable to store the crafting recipe (optional)
    public List<Item> craftingRecipe;


    public void Use()
    {
        Debug.Log("This is the Use() function of item: " + name + " - " + description);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftingRecipe
{
    public string resultItemName;
    public List<Ingredient> ingredients;
}

[System.Serializable]
public class Ingredient
{
    public string itemName;
    public int amount;
}

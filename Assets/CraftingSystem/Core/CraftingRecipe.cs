using System;
using System.Collections.Generic;

namespace CraftingSystem
{
    [Serializable]
    public class CraftingRecipe
    {
        public string resultItemName;
        public List<Ingredient> ingredients;
    }

    [Serializable]
    public class Ingredient
    {
        public string itemName;
        public int amount;
    }
}
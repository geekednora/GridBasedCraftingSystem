using System.Collections.Generic;

namespace CraftingSystem
{
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
}
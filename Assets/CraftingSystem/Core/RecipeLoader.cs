using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CraftingSystem.Core
{
    public class RecipeLoader : MonoBehaviour
    {
        private readonly List<List<Recipe>> _recipes = new();
        private bool isLoaded;

        [FormerlySerializedAs("RecipesPath")]
        [Header("Recipes Folder Path")]
        // Path to recipes folder - ../Assets/Resources/Recipes/
        // Need to be added to Resources folder
        [SerializeField] private string recipesPath = "Recipes/";


        private void Start()
        {
            LoadRecipesAsync();
        }


        private void LoadRecipesAsync()
        {
            _recipes.Clear();
            
            var recipesRequest = Resources.LoadAll<RecipeSO>(recipesPath);
            // yield return recipesRequest;
            
            foreach (var recipe in recipesRequest)
                if (recipe.IsValid)
                {
                    // Check if there is enough space in the list
                    CheckCapacity(recipe.Recipe.Count);
                    _recipes[recipe.Recipe.Count - 1].Add(recipe.Recipe);
                }
            

            isLoaded = true;
        }

        private void CheckCapacity(int capacity)
        {
            while (_recipes.Count < capacity) _recipes.Add(new List<Recipe>());
        }

        // TODO: Add a method to unload recipes from memory

        // Checking grid state to find matching recipes and return the result item
        public Item CheckGridState(Item[] items, Vector2Int gridSize, out int resultCount)
        {
            if (!isLoaded)
            {
                Debug.LogError("Recipes are not loaded. Please, load them first!");
                resultCount = 0;
                return null;
            }

            var craftingItems = new CraftingGrid(items, gridSize);
            var count = craftingItems.Count;

            if (count == 0)
            {
                resultCount = 0;
                return null;
            }

            if (count > _recipes.Count || _recipes[count - 1] == null)
            {
                resultCount = 0;
                return null;
            }

            foreach (var recipe in _recipes[count - 1])
                if (recipe.IsValid(craftingItems))
                {
                    resultCount = recipe.ResultCount;
                    return recipe.ResultItem;
                }

            resultCount = 0;
            return null;
        }
    }
}
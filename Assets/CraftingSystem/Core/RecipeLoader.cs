using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class RecipeLoader : MonoBehaviour
    {
        private readonly List<List<Recipe>> _recipes = new();
        private bool _isRecipeLoaderNull;
        private bool isLoaded;

        [field: Header("Recipes Folder Path")]
        // Path to recipes folder - ../Assets/Resources/Recipes/
        // Need to be added to Resources folder
        [SerializeField]
        private IEnumerable<string> RecipesPath { get; } = new[] { "Recipes/" };

        private void Awake()
        {
            _isRecipeLoaderNull = this == null;
        }

        private void Start()
        {
            StartCoroutine(LoadRecipesAsync());
        }


        private IEnumerator LoadRecipesAsync()
        {
            _recipes.Clear();

            foreach (var path in RecipesPath)
            {
                var recipesRequest = Resources.LoadAll<RecipeSO>(path);
                yield return recipesRequest;

                if (_isRecipeLoaderNull) yield break; // Check if the object is destroyed before continuing.

                foreach (var recipe in recipesRequest)
                    if (recipe.IsValid)
                    {
                        // Check if there is enough space in the list
                        CheckCapacity(recipe.Recipe.Count);
                        _recipes[recipe.Recipe.Count - 1].Add(recipe.Recipe);
                    }
            }

            isLoaded = true;
        }

        private void CheckCapacity(int capacity)
        {
            while (_recipes.Count < capacity) _recipes.Add(new List<Recipe>());
        }

        // TODO: Add a method to unload recipes from memory

        // Checking grid state to find matching recipes and return the result item
        public BaseItem CheckGridState(BaseItem[] items, Vector2Int gridSize, out int resultCount)
        {
            if (!isLoaded)
            {
                Debug.LogError("Recipes are not loaded. Please, load them first!");
                resultCount = 0;
                return null;
            }

            var craftingItems = new CraftingGrid(items, gridSize);
            var count = craftingItems.Count;

            switch (count)
            {
                case 0:
                    resultCount = 0;
                    return null;

                default:
                    if (count > _recipes.Count || _recipes[count - 1] == null)
                    {
                        resultCount = 0;
                        return null;
                    }

                    foreach (var recipe in _recipes[count - 1])
                        if (recipe.isValid(craftingItems))
                        {
                            resultCount = recipe.ResultCount;
                            return recipe.ResultItem;
                        }

                    resultCount = 0;
                    return null;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingSystem.Core
{
    public class RecipeLoader : MonoBehaviour
    {
        [field: Header("Recipes Folder Path")]
        // Path to recipes folder - ../Assets/Resources/Recipes/
        // Need to be added to Resources folder
        [SerializeField] private IEnumerable<string> RecipesPath { get; } = new string[] { "Recipes/" };
        
        private readonly List<List<Recipe>> _recipes = new List<List<Recipe>>();
        private bool _isLoaded = false;
        private bool _isRecipeLoaderNull;

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
                {
                    if (recipe.IsValid)
                    {
                        // Check if there is enough space in the list
                        CheckCapacity(recipe.Recipe.Count);
                        _recipes[recipe.Recipe.Count - 1].Add(recipe.Recipe);
                    }
                }
            }

            _isLoaded = true;
        }
        
        private void CheckCapacity(int capacity)
        {
            while (_recipes.Count < capacity)
            {
                _recipes.Add(new List<Recipe>());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using CraftingSystem.Demo.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace CraftingSystem.Core
{
    [Serializable]
    public class Recipe 
    {
        public Item _ResultItem;
        public int _ResultCount = 1;
        
        public Item ResultItem => _ResultItem;
        public int ResultCount => _ResultCount;
        
        public Recipe(Vector2Int gridSize, Item[] recipeItems, Item resultItem, int resultCount) // : base(recipeItems, gridSize)
        {
            _ResultItem = resultItem;
            _ResultCount = resultCount;
        }
    }
    
    // creating a recipe from a scriptable object
    //Attribute which allows right click->Create
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    public class RecipeItem : ScriptableObject
    {
        [SerializeField] public Item _ResultItem;
        [SerializeField] public int _ResultCount = 1;

        [SerializeField] public Vector2Int _SizeOfGrid;
        [SerializeField] public Item[] ingredients;
        
        //Used in game logic
        private Recipe _recipe;
        
        // Used in editor
        private bool _isInitialized = false;
        private bool _isRecipeValid = false;

        public Recipe Recipe
        {
            get
            {
                if (Application.isEditor || !_isInitialized)
                {
                    CreateRecipe();
                    _isInitialized= true;
                }

                return _recipe;
            }
        }


        // Creating a CreateRecipe() method
        
        private void CreateRecipe()
        {
            if (_SizeOfGrid == Vector2Int.zero)
            {
                _isRecipeValid = false;
                return;
            }

            _isRecipeValid = false;
            // check if at least one item is not null
            foreach (var item in ingredients)
            {
                if (item != null)
                {
                    _recipe = new Recipe(_SizeOfGrid, ingredients, _ResultItem, _ResultCount);
                    _isRecipeValid = _ResultCount > 0;
                    return;
                }
            }
        }
    }
    
}
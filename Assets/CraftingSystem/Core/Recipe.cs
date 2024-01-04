using System;
using System.Collections.Generic;
using CraftingSystem.Demo.Scripts.InventorySystem;
using UnityEngine;

namespace CraftingSystem.Core
{
    //Attribute which allows right click->Create
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    public class Recipe : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
    {
        public Item _ResultItem;
        public int _ResultCount = 1;

        public Item[] _RecipeItems;
        public Vector2Int _SizeOfGrid;
        
        public bool IsValid => _SizeOfGrid != Vector2Int.zero;
        public Item ResultItem => _ResultItem;
        public int ResultCount => _ResultCount;
        
        public Recipe(Vector2Int gridSize, Item[] recipeItems, Item resultItem, int resultCount) // : base(recipeItems, gridSize)
        {
            _ResultItem = resultItem;
            _ResultCount = resultCount;
        }
    }
}
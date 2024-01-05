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
        public BaseItem _ResultItem;
        public int _ResultCount;
        
        public BaseItem ResultItem => _ResultItem;
        public int ResultCount => _ResultCount;
        
        public Recipe(Vector2Int gridSize, BaseItem[] recipeItems, BaseItem resultItem, int resultCount) // : base(recipeItems, gridSize)
        {
            _ResultItem = resultItem;
            _ResultCount = resultCount;
        }
    }
    
    
    
}
using System;
using System.Collections.Generic;
using CraftingSystem.Demo.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace CraftingSystem.Core
{
    [Serializable]
    public class Recipe : CraftingGrid // Inheriting from CraftingGrid ti get access to grid state
    {
        public BaseItem _ResultItem;
        public int _ResultCount;
        
        public BaseItem ResultItem => _ResultItem;
        public int ResultCount => _ResultCount;
        public int Count { get; set; }

        public Recipe(Vector2Int gridSize, BaseItem[] recipeItems, BaseItem resultItem, int resultCount) : base(recipeItems, gridSize)
        {
            _ResultItem = resultItem;
            _ResultCount = resultCount;
        }
    }
    
    
    
}
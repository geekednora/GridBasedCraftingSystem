using System;
using UnityEngine;

namespace CraftingSystem.Core
{
    [Serializable]
    public class Recipe : CraftingGrid // Inheriting from CraftingGrid ti get access to grid state
    {
        public BaseItem _ResultItem;
        public int _ResultCount;

        public Recipe(Vector2Int gridSize, BaseItem[] recipeItems, BaseItem resultItem, int resultCount) : base(
            recipeItems, gridSize)
        {
            _ResultItem = resultItem;
            _ResultCount = resultCount;
        }

        public BaseItem ResultItem => _ResultItem;
        public int ResultCount => _ResultCount;
        public new int Count { get; set; }
    }
}
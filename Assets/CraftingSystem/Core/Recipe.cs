using System;
using UnityEngine;

namespace CraftingSystem.Core
{
    [Serializable]
    public class Recipe : CraftingGrid // Inheriting from CraftingGrid ti get access to grid state
    {
        public Item _ResultItem;
        public int _ResultCount;

        public Recipe(Vector2Int gridSize, Item[] recipeItems, Item resultItem, int resultCount) : base(
            recipeItems, gridSize)
        {
            _ResultItem = resultItem;
            _ResultCount = resultCount;
        }

        public Item ResultItem => _ResultItem;
        public int ResultCount => _ResultCount;
        public new int Count { get; set; }
    }
}
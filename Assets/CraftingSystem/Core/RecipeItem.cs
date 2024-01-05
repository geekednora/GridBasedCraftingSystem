using System;
using UnityEngine;

namespace CraftingSystem.Core
{
    [Serializable]
    public struct RecipeItem // Creating struct for recipe item
    {
        public BaseItem item;
        // Initial position = (0, 0)
        public Vector2Int position;

        public static bool operator ==(RecipeItem a, RecipeItem b) => a.item == b.item && a.position == b.position; // Overloading == operator

        public static bool operator !=(RecipeItem a, RecipeItem b) => !(a == b); // Overloading != operator

        public bool Equals(RecipeItem other) => this == other; // Overriding Equals method

        public override bool Equals(object obj) => obj is RecipeItem other && Equals(other); // Overriding Equals method

        public override int GetHashCode() => HashCode.Combine(item, position); // Overriding GetHashCode method
    }
}
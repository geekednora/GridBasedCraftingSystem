using System;
using UnityEngine;

namespace CraftingSystem.Core
{
    [Serializable]
    public struct RecipeItem // Creating struct for recipe item
    {
        public Item item;

        // Initial position = (0, 0)
        public Vector2Int position;

        public static bool operator ==(RecipeItem a, RecipeItem b)
        {
            return a.item == b.item && a.position == b.position;
            // Overloading == operator
        }

        public static bool operator !=(RecipeItem a, RecipeItem b)
        {
            return !(a == b);
            // Overloading != operator
        }

        public bool Equals(RecipeItem other)
        {
            return this == other;
            // Overriding Equals method
        }

        public override bool Equals(object obj)
        {
            return obj is RecipeItem other && Equals(other);
            // Overriding Equals method
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(item, position);
            // Overriding GetHashCode method
        }
    }
}
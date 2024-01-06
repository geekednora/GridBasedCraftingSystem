using System.Linq;
using System.Collections.Generic;
using CraftingSystem.Demo.Scripts;
using UnityEngine;

/*
 * This class is responsible for storing the state of the grid
 * and checking if the grid state matches the recipe. Also,
 * this class is responsible for creating the grid and called
 * in the CraftingSystem class.
 */

namespace CraftingSystem.Core
{
    public class CraftingGrid
    {
        private List<RecipeItem> RecipeItems { get; } = new();

        public int Count => RecipeItems.Count;

        
        
        public CraftingGrid(IReadOnlyList<Item> ingredients, Vector2Int gridSize)
        {
            Vector2Int defaultGridSize = new Vector2Int(0, 0);
            Vector2Int initialPosition = new Vector2Int(-1, -1);
            
            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    var index = x + y * gridSize.x;
                    // check for null
                    if (ingredients[index] == null) continue;

                    // if -- Initial position = (-1, -1), change it to current position
                    if (initialPosition == new Vector2Int(-1, -1))
                        initialPosition.Set(x, y);

                    var item = ingredients[index];
                    var distanceX = x - initialPosition.x;
                    var distanceY = y - initialPosition.y;

                    if (distanceX > defaultGridSize.x) defaultGridSize.Set(distanceX, defaultGridSize.y);

                    if (distanceY > defaultGridSize.y) defaultGridSize.Set(defaultGridSize.x, distanceY);

                    RecipeItems.Add(new RecipeItem
                    {
                        item = item,
                        position = new Vector2Int(distanceX, distanceY)
                    });
                }
            }
        }

        public bool IsValid(CraftingGrid item)
        {
            return item.Count == RecipeItems.Count && item.RecipeItems.SequenceEqual(RecipeItems);
        }
    }
}
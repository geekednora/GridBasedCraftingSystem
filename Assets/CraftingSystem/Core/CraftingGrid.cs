using System.Linq;
using System.Collections.Generic;
using CraftingSystem.Demo.Scripts.InventorySystem;
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
        private readonly List<RecipeItem> _recipeItems = new List<RecipeItem>();

        private List<RecipeItem> RecipeItems => _recipeItems;
        public int Count => _recipeItems.Count;

        private Vector2Int DefaultGridSize { get; set; } = new Vector2Int(3, 3);
        private Vector2Int InitialPosition { get; set; } = new Vector2Int(0, 0);

        public CraftingGrid(IReadOnlyList<BaseItem> ingredients, Vector2Int gridSize)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    var index = x + y * gridSize.x;
                    // check for null
                    if (ingredients[index] == null) continue;
                    
                    // if -- Initial position = (0, 0), change it to current position
                    if (InitialPosition == new Vector2Int(0, 0))
                        InitialPosition.Set(x, y);

                    var item = ingredients[index];
                    var distanceX = x - InitialPosition.x;
                    var distanceY = y - InitialPosition.y;

                    if (distanceX > DefaultGridSize.x)
                    {
                        DefaultGridSize.Set(distanceX, DefaultGridSize.y);
                    }

                    if (distanceY > DefaultGridSize.y)
                    {
                        DefaultGridSize.Set(DefaultGridSize.x, distanceY);
                    }

                    RecipeItems.Add(new RecipeItem()
                    {
                        item = item,
                        position = new Vector2Int(distanceX, distanceY)
                    });
                }
            }
        }

        public bool isValid(CraftingGrid item)
        {
            return item.Count == _recipeItems.Count && item.RecipeItems.SequenceEqual(_recipeItems);
        }
    }
}
using UnityEngine;


namespace CraftingSystem.Core
{
    //Attribute which allows right click->Create
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
    [Icon("Assets/CraftingSystem/Icons/Recipe.png")]
    public class RecipeSO : ScriptableObject
    {
        [SerializeField] public BaseItem _ResultItem;
        [SerializeField] public int _ResultCount = 1;

        [SerializeField] public Vector2Int _SizeOfGrid;
        [SerializeField] public BaseItem[] ingredients;
        
        //Used in game logic
        private Recipe _recipe;
        
        // Used in editor
        private bool _isInitialized = false;
        private bool _isRecipeValid = false;

        // Used in editor
        public Recipe Recipe
        {
            get
            {
                if (!Application.isEditor && _isInitialized) return _recipe;
                CreateRecipe();
                _isInitialized = true;

                return _recipe;
            }
        }

        // Validating the recipe
        public bool IsValid
        {
            get
            {
                if (!Application.isEditor && _isInitialized) return _isRecipeValid;
                CreateRecipe();
                _isInitialized = true;
                return _isRecipeValid;
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

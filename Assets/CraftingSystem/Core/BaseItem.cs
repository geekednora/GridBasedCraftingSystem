using UnityEngine;

/*
 * This is a base class for an item for a Crafting System. It is a ScriptableObject, which means
 * it is an object which exists in the project, not in the scene. It has a Sprite icon,
 * which is used to display the item in the UI. Used to inherit to make different types of items
 * with a custom logic.
 */

namespace CraftingSystem.Core
{
    public abstract class BaseItem : ScriptableObject
    {
        public Sprite icon;
    }
}

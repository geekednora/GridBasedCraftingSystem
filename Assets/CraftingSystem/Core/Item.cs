using UnityEngine;

namespace CraftingSystem.Core
{
    //Attribute which allows right click->Create
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
    public class Item : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
    {
        public new string name;
        public Sprite icon;

        // This is a multiline text field, which is useful for descriptions
        [TextArea] public string description = "";

        // This is a flag which allows us to check if the item is consumable
        public bool isConsumable;


        public void Use()
        {
            Debug.Log("This is the Use() function of item: " + name + " - " + description);
        }
        
        
    }
}
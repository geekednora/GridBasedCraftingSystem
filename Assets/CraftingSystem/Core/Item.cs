using UnityEngine;

//Attribute which allows right click->Create

namespace CraftingSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
    public class
        Item : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
    {
        public Sprite icon;

        [TextArea] public string description = "";

        public bool isConsumable;


        public void Use()
        {
            Debug.Log("This is the Use() function of item: " + name + " - " + description);
        }
    }
}
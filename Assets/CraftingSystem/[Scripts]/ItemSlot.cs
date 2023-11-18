using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item = null;
    public Inventory inventory;
    
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [SerializeField]
    private int count = 0;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            UpdateGraphic();
        }
    }

    [SerializeField]
    Image itemIcon;

    [SerializeField]
    TextMeshProUGUI itemCountText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGraphic();
    }

    //Change Icon and count
    void UpdateGraphic()
    {
        if (count < 1)
        {
            item = null;
            itemIcon.gameObject.SetActive(false);
            itemCountText.gameObject.SetActive(false);
        }
        else
        {
            //set sprite to the one from the item
            itemIcon.sprite = item.icon;
            itemIcon.gameObject.SetActive(true);
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = count.ToString();
        }
    }

    public void UseItemInSlot()
    {
        if (CanUseItem())
        {
            item.Use();
            if (item.isConsumable)
            {
                Count--;
            }
        }
    }

    private bool CanUseItem()
    {
        return (item != null && count > 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = item.description;
            nameText.text = item.name;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }

    public void CraftItem()
    {
        
        // Implement crafting logic here
        // Example: Craft a wooden sword using 2 sticks and 2 planks
        List<Item> recipe = new List<Item> { };
        Item outputItem = null /*add item name*/;
        int outputCount = 1;

        if (inventory.CraftItem(recipe, outputItem, outputCount))
        {
            // Crafting successful, update the UI
            UpdateGraphic();
        }
    }
}


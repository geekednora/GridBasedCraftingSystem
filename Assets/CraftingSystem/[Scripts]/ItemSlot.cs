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
    Image itemIcon;
    [SerializeField]
    TextMeshProUGUI itemCountText;
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
    
    public void AddItemToSlot(Item item)
    {
        this.item = item;
        UpdateGraphic();
    }
    
    public bool HasItem()
    {
        return !string.IsNullOrEmpty(nameText.ToString()) && GetItemCount() > 0;
    }

    // Get the name of the item in the slot
    public string GetItemName()
    {
        return nameText.ToString();
    }

    // Get the count of the item in the slot
    public int GetItemCount()
    {
        return Count;
    }

    // Set the item in the slot
    public void SetItem(string itemName, int itemCount)
    {
        this.nameText.text = itemName;
        this.Count = itemCount;
    }

    public void ClearSlot()
    {
        this.item = null;
        UpdateGraphic();
    }
    
    
}


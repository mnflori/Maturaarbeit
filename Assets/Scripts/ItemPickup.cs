using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable, IItem
{
    [SerializeField] private string itemName;

    public string getItemName()
    {
        return itemName;
    }

    public void Interact()
    {
       

    }
}

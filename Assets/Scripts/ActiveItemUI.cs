using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    [SerializeField] private Image activeItemIcon; //Dies hier wird mit dem Gameobject Kind vom Slot, also das Icon ausgew�hl�t.
    [SerializeField] private Sprite defaultEmptyIcon; //Hier habe ich einfach das Bild vom leeren Slot eingef�gt.
    [SerializeField] private InventoryManager inventoryManager; 

    public void UpdateActiveItemDisplay(string itemName)
    {
        Sprite sprite = inventoryManager.getSpriteForItem(itemName);
        activeItemIcon.sprite = sprite != null ? sprite : defaultEmptyIcon; //sollte kein Sprite da sein, wird das defaultEmptyIcon angezeigt. Wenn nicht, dann wird das entsprechende Icon eingeblendet.
    }

    public void ClearDisplay()
    {
        activeItemIcon.sprite = defaultEmptyIcon; 
    }
}

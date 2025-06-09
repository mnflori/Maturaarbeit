using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Image> slotImages;
    [SerializeField] private Sprite keySprite;
    [SerializeField] private Sprite mushroomSprite;
    private int selectedIndex = 0;
    [SerializeField] private ActiveItemUI activeItemUI; // Referenz im Inspector setzen

    private List<string> collectedItems = new List<string>();

    public void addItem(string itemName)
    {
        if (collectedItems.Count >= slotImages.Count) return; //Damit kein neues Item hinzugefügt wird, wenn es mehr collected Items hat als Slots. (Sollte eigentlich nicht möglich sein)

        collectedItems.Add(itemName); //Hier wird die Liste mit den Namen von den Items erweitert.
        updateInventoryUI();        
    }

    private void updateInventoryUI()
    {
        for (int i = 0; i < slotImages.Count; i++) //Sodass nicht mehr Icons gezeigt werden, als es slots gibt.
        {
            Transform slot = slotImages[i].transform;

            Image icon = slot.Find("Icon").GetComponent<Image>(); //Hier wird die Position vom Slot ermittelt und danach den Image Pfad vom Kinderobjekt "Icon" vomn Slot

            if (i < collectedItems.Count)
            {
                icon.enabled = true;
                icon.sprite = getSpriteForItem(collectedItems[i]); //Sollte die Anzahl collectedItems kleiner oder gleich gross sein wie Slots, wird das neue Icon ersetzt durch den Sprites vom entsprechenden Item
            }
            else
            {
                icon.enabled = false; //Sollte es irgendwie mehr collected Items haben, wird das Icon einfach nicht ersetzt.
                icon.sprite = null;
            }
        }
    }

    public Sprite getSpriteForItem(string itemName) //Hier weiss das Skript, welches Icon gezeigt werden soll
    {
        switch (itemName)
        {
            case "Key": return keySprite;
            case "Mushrooms": return mushroomSprite;
            default: return null;
        }
    }

    public void SelectNextItem() //Dies zählt die Items durch, die der Spieler besitzt und zeigt korrekt an, welches ausgewählt wurde
    {
        if (collectedItems.Count == 0) return;

        selectedIndex = (selectedIndex + 1) % collectedItems.Count;
        activeItemUI.UpdateActiveItemDisplay(collectedItems[selectedIndex]);
    }


}

using UnityEngine;

public class InventoryPreviewUI : MonoBehaviour
{
    [SerializeField] private GameObject miniInventoryUI;

    public void ShowMiniInventory()
    {
        miniInventoryUI.SetActive(true); //hier wird das UI vom Inventory angezeigt.
    }

    public void HideMiniInventory()
    {
        miniInventoryUI.SetActive(false); //hier wird das UI vom Inventory versteckt.
    }

    public bool isVisible()
    {
        return miniInventoryUI.activeSelf; //Dies ist ein Bool, der dem MC Skript sagt, ob das Inventar schon gezeigt wird.
    }
}

using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    public MainCMovement player;
    public TextMeshProUGUI debugText;

    void Update()
    {
        if (DebugManager.Instance != null && DebugManager.Instance.debugMode)
        {
            debugText.enabled = true;
            debugText.text = $"Leben: {player.playerHealth}";
        }
        else
        {
            debugText.enabled = false;
        }
    }
}

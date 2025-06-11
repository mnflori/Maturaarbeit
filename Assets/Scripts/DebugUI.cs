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
            
            debugText.text = $"Leben: {player.playerHealth}";
            
        }

    }
}

using Unity.VisualScripting;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance { get; private set; }
    public GameObject debugUI;

    public bool debugMode = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))  // z.B. F3 schaltet DebugMode um
        {
            debugMode = !debugMode;
            checkIfNeededToBeShown();
        }
    }

    private void checkIfNeededToBeShown()
    {
        if (debugMode)
        {
            debugUI.SetActive(true);
        }
        else
        {
            debugUI.SetActive(false);
        }
    }
}

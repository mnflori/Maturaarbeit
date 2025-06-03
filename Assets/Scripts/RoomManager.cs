using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private Dictionary<Vector2Int, GameObject> rooms = new();
    private Vector2Int currentRoom;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        RoomScript[] roomScripts = FindObjectsByType<RoomScript>(FindObjectsSortMode.None);
        foreach (RoomScript r in roomScripts)
        {
            rooms[r.roomCoordinates] = r.gameObject;
            r.gameObject.SetActive(false);
        }

        // Start-Raum aktivieren
        currentRoom = new Vector2Int(0, 0); // ← Anpassen je nach Startposition
        SetActiveRoom(currentRoom);


    }

    public void SetActiveRoom(Vector2Int newRoom)
    {
        if (rooms.ContainsKey(currentRoom))
            rooms[currentRoom].SetActive(false);

        if (rooms.ContainsKey(newRoom))
        {
            rooms[newRoom].SetActive(true);
            currentRoom = newRoom;
        }
        else
        {
            Debug.LogWarning("Unbekannter Raum: " + newRoom);
        }
    }
}

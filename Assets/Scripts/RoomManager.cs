using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private Dictionary<Vector2Int, GameObject> rooms = new();
    
    private Vector2Int currentRoom;
    public int startingRoomX;   //Die drei Variabeln sind, so damit, der "Startraum" ggbfalls verändert werden kann.
    public int startingRoomY;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        //Hier wird jeder Raum, der nicht die Startkoordinaten hat, deaktiviert
        RoomScript[] roomScripts = FindObjectsByType<RoomScript>(FindObjectsSortMode.None); 
        foreach (RoomScript r in roomScripts)
        {
            rooms[r.roomCoordinates] = r.gameObject;

            r.gameObject.SetActive(false);
        }

        // Start-Raum wird aktivieren
        currentRoom = new Vector2Int(startingRoomX, startingRoomY); 
        SetActiveRoom(currentRoom); 


    }

    //Funktion, mit der beim berühren eines Randes von einem Raum, der andere Raum aktiviert wird und der Raum, aus dem
    //raus gegangen wird, deaktiviert wird.
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


    //Das ist da, damit beim TileTransition die Raumkoordinaten gemerkt werden. Ein bisschen unvorteilhafte Bezeichnung der Variabeln
    public void changeRoomCoordinates(int roomX, int roomY)
    {
        startingRoomX = roomX;
        startingRoomY = roomY;
    }
}

using System.Collections;
using UnityEngine;

public class TileTransition : MonoBehaviour
{

    public RoomManager roommanger;   //Das Roommangager Skript
    private Vector2 cameraDirection; //Die Verschiebung der Kamera
    private Vector2Int targetRoom; // Damit die Funktion der wechselnden Räumen im Roommanger aufgerufen werden kann

    public string directionNextRoom; //Damit das Skript weiss, in welche Richtung es geht

    private int currentRoomX; //Damit die Koordinaten in ints umgewandelt werden
    private int currentRoomY;


    //Wird jedes mal aufgerufen, wenn ein Raum wieder aktiviert wird.
    private void OnEnable()
    {
        currentRoomX = roommanger.startingRoomX;
        currentRoomY = roommanger.startingRoomY;
    }
   

    //Hier wird berechnet, wohin es geht
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(currentRoomX);
        switch(directionNextRoom)
        {
            case "up":
                currentRoomY++;
                cameraDirection = new Vector2(0, 1.1111111f);
                break;
            case "right":
                currentRoomX = currentRoomX + 1;
                cameraDirection = new Vector2(1.1111111f, 0);
                Debug.Log("Right");
                break;
            case "down":
                currentRoomY = currentRoomY - 1;
                cameraDirection = new Vector2(0, -1.1111111f);
                break;
            case "left":
                currentRoomX = currentRoomX - 1;
                cameraDirection = new Vector2(-1.1111f, 0);
                Debug.Log("Left");
                break;

                
        }

        //Damit die Koordinaten im Roommanger gespeichert werden
        roommanger.changeRoomCoordinates(currentRoomX, currentRoomY);



        if (!other.CompareTag("Player")) return;

        //Damit die Kameraposition geändert wird
        var cam = Camera.main.GetComponent<ScreenCameraMover>();
        if (cam != null)
        {
            cam.MoveCamera(cameraDirection);
            

            //Die Ints werden wieder in Vector2Int umgewandelt, damit der neue Raum aktiviert werden kann
            targetRoom = new Vector2Int(currentRoomX, currentRoomY);
            RoomManager.Instance?.SetActiveRoom(targetRoom);
        }
    }
}

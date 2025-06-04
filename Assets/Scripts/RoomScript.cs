using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Vector2Int roomCoordinates;  // Hier sind die Koordinaten des Raumes
    private int roomPositionX;
    private int roomPositionY;

    private void Start()
    {
        //Hier werden die Koordinaten in ints umgewandelt
        (roomPositionX, roomPositionY) = (roomCoordinates.x, roomCoordinates.y); 

        //Hier werden die Positionen, der Grenzen von den Räumen berechnet
        transform.position = new Vector2((17.782292f * roomPositionX),(10 * roomPositionY)); 
        
    }
}

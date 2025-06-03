using UnityEngine;

public class TileTransition : MonoBehaviour
{
    public Vector2 direction;
    public Vector2 playerOffset;
    public Vector2Int targetRoom; // z.B. (0,1)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var cam = Camera.main.GetComponent<ScreenCameraMover>();
        if (cam != null)
        {
            cam.MoveCamera(direction);
            other.transform.position += (Vector3)playerOffset;

            RoomManager.Instance?.SetActiveRoom(targetRoom);
        }
    }
}

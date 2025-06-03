using UnityEngine;

public class TileTransition : MonoBehaviour
{
    public Vector2 cameraShift;

    public Vector2 direction; 
    public Vector2 playerOffset; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        ScreenCameraMover camMover = Camera.main.GetComponent<ScreenCameraMover>();
        if (camMover != null && !camMover.IsMoving())
        {
            Debug.Log("Kamera sollte sich verschieben!");

            camMover.MoveCamera(direction);
            other.transform.position += (Vector3)playerOffset;
        }

    }
}
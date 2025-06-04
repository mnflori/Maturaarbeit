using UnityEngine;

public class ScreenCameraMover : MonoBehaviour
{
    
    public float screenWidth = 16f;
    public float screenHeight = 9f;
    public float moveSpeed = 5f;

    
    public static ScreenCameraMover Instance;

    void Start()
    {
        Instance = this;
        
  
    }

  



    public void MoveCamera(Vector2 direction)
    {
        // Sofortige Positionsänderung der Kamera
        Vector3 shift = new Vector3(direction.x * screenWidth, direction.y * screenHeight, 0f);
        transform.position += shift;
    }


    
}

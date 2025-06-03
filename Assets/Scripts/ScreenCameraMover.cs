using UnityEngine;

public class ScreenCameraMover : MonoBehaviour
{
    public float screenWidth = 16f;
    public float screenHeight = 9f;
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }


    public void MoveCamera(Vector2 direction)
    {
        if (isMoving) return;
        Debug.Log("Neues Ziel: " + targetPosition);

        Vector3 shift = new Vector3(direction.x * screenWidth, direction.y * screenHeight, 0f);
        targetPosition = transform.position + shift;
        isMoving = true;
    }


    public bool IsMoving() => isMoving;
}

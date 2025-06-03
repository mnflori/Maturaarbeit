using UnityEngine;

public class TileExitSpawner : MonoBehaviour
{
    public float tileWidth = 16f;
    public float tileHeight = 9f;
    public GameObject exitPrefab;
    public float offsetFromCamera = 0.1f;


    private void Start()
    {
        SpawnExit(Vector2.right);
        SpawnExit(Vector2.left);
        SpawnExit(Vector2.up);
        SpawnExit(Vector2.down);
    }

    void SpawnExit(Vector2 dir)
    {
        float thickness = 0.1f;

        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;
        Vector2 camSize = new Vector2(camWidth, camHeight);

        Vector2 localPos = Vector2.zero;

        if (dir == Vector2.right)
            localPos = new Vector2(camSize.x / 2f + offsetFromCamera + thickness / 2f, 0f);
        else if (dir == Vector2.left)
            localPos = new Vector2(-camSize.x / 2f - offsetFromCamera - thickness / 2f, 0f);
        else if (dir == Vector2.up)
            localPos = new Vector2(0f, camSize.y / 2f + offsetFromCamera + thickness / 2f);
        else if (dir == Vector2.down)
            localPos = new Vector2(0f, -camSize.y / 2f - offsetFromCamera - thickness / 2f);



        GameObject exit = Instantiate(exitPrefab, transform);
        exit.transform.localPosition = localPos;

        BoxCollider2D col = exit.GetComponent<BoxCollider2D>();
        if (col != null)
        {
            if (Mathf.Abs(dir.x) > 0)
                col.size = new Vector2(thickness, camHeight);
            else
                col.size = new Vector2(camWidth, thickness);
        }

        TileTransition tt = exit.GetComponent<TileTransition>();
        if (tt != null)
        {
            tt.cameraShift = dir * camSize;
            tt.playerOffset = -dir * (camSize / 2f - new Vector2(0.5f, 0.5f));
        }
    }



}

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Vector2Int centerRoom;
    private Transform player;
    public float speed = 2f;
    private RoomManager roomManager;
    public RoomScript ownerRoom;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        roomManager = RoomManager.Instance;
    }

    void Update()
    {



        if (player == null || roomManager == null) return;
        Vector2Int playerRoom = roomManager.GetCurrentRoom();

        // Wenn Spieler außerhalb des erlaubten Bereichs → löschen
        if (Mathf.Abs(playerRoom.x - centerRoom.x) > 1 || Mathf.Abs(playerRoom.y - centerRoom.y) > 1)
        {
            if (ownerRoom != null)
            {
                ownerRoom.NotifyEnemyDestroyed();
            }

            if (ownerRoom != null)
            {
                ownerRoom.NotifyEnemyDestroyed();
            }

            RoomManager.Instance.UnregisterEnemy(gameObject);
            
            Destroy(gameObject);


            Destroy(gameObject);

            return;
        }

        //Verfolgen
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);


    }

    private Vector2Int GetRoomCoordinates(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x / 17.782292f);
        int y = Mathf.FloorToInt(position.y / 10f);
        return new Vector2Int(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MainCMovement player = other.GetComponent<MainCMovement>();
            if (player != null)
            {
                player.takeDamage(34);
                Destroy(gameObject);
            }
        }
    }
}

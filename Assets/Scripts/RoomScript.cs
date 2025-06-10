using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Vector2Int roomCoordinates;  // Hier sind die Koordinaten des Raumes
    private int roomPositionX;
    private int roomPositionY;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public float spawnChance = 0.2f;     // 1 zu 5
    public float despawnChance = 0.33f;  // 2 zu 3

    private GameObject spawnedEnemy;
    private bool hasSpawnAttempted = false;

    public bool isEnemyThere = false;

    private void Start()
    {
        //Hier werden die Koordinaten in ints umgewandelt
        (roomPositionX, roomPositionY) = (roomCoordinates.x, roomCoordinates.y); 

        //Hier werden die Positionen, der Grenzen von den Räumen berechnet
        transform.position = new Vector2((17.782292f * roomPositionX),(10 * roomPositionY)); 
        
    }

    private void OnEnable()
    {
        if (!hasSpawnAttempted && !isEnemyThere)
        {
            TrySpawnEnemy();
            hasSpawnAttempted = true;
        }
        
    }

    private void OnDisable()
    {
        
        hasSpawnAttempted = false;
    }

    private void TrySpawnEnemy()
    {
        if (RoomManager.Instance.IsEnemyActive())
        {
            return; // Schon ein Gegner aktiv → dieser Raum spawnt keinen
        }

        if (enemyPrefab == null || enemySpawnPoints.Length == 0) return;

        if (Random.value < spawnChance)
        {
            Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            isEnemyThere = true;


            RoomManager.Instance.RegisterEnemy(spawnedEnemy);
            EnemyAI ai = spawnedEnemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.centerRoom = roomCoordinates; // ← wichtig!
                ai.ownerRoom = this;
            }
        }
    }


    public GameObject GetSpawnedEnemy()
    {
        return spawnedEnemy;
    }

    public void ForceDespawnEnemy(float chance)
    {
        float roll = Random.value;

        if (roll < chance)
        {
            Destroy(spawnedEnemy);
            NotifyEnemyDestroyed();
        }
        
    }

    public bool HasEnemy()
    {
        return spawnedEnemy != null;
    }

    public void NotifyEnemyDestroyed()
    {
        spawnedEnemy = null;
        isEnemyThere = false;
    }
}

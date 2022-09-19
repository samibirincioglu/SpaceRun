using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    Vector3 nextSpawnPoint,defaultNextSpawnPoint;
    public GameObject groundTile;
    public GameObject startGroundTile;
    public int lastAsteroid;

    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }
    public void SpawnTile7()
    {
        nextSpawnPoint = defaultNextSpawnPoint;
        for (int i = 0; i < 7; i++)
        {
            SpawnTile();
        }
    }

    void Start()
    {
        defaultNextSpawnPoint = nextSpawnPoint;
        for (int i = 0; i < 7; i++)
        {
            SpawnTile();
        }
        Debug.Log("started");
    }
}

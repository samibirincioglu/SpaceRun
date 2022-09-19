using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    GameObject wallPrefab;
    [SerializeField] GameObject wallSkin0, wallSkin1, wallSkin2, wallSkin3, wallSkin4, wallSkin5, wallSkin6;
    [SerializeField] GameObject middleWallSkin0, middleWallSkin1, middleWallSkin2, middleWallSkin3, middleWallSkin4, middleWallSkin5, middleWallSkin6;
    GameObject middleWallPrefab;
    public GameObject AsteroidPrefab, AsteroidPrefab2, AsteroidPrefab3, AsteroidPrefab4;
    private bool colliderExit = true;
    private int _spawnPoint1, _spawnPoint2, _spawnPoint3;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("WItemSelected"))
            PlayerPrefs.SetInt("WItemSelected", 0);

        GameObject[] wallPrefabs = { wallSkin0, wallSkin1, wallSkin2, wallSkin3, wallSkin4, wallSkin5, wallSkin6 };
        wallPrefab = wallPrefabs[PlayerPrefs.GetInt("WItemSelected")];
        GameObject[] middleWallPrefabs = { middleWallSkin0, middleWallSkin1, middleWallSkin2, middleWallSkin3, middleWallSkin4, middleWallSkin5, middleWallSkin6 };
        middleWallPrefab = middleWallPrefabs[PlayerPrefs.GetInt("WItemSelected")];

        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();


        SpawnWall();
        CoinPos();
        AsteroidSpawn();
    }
    private void OnTriggerExit(Collider other)
    {
        if (colliderExit)
        {
            if (other.gameObject.tag == "Player")
            {
                colliderExit = false;
                groundSpawner.SpawnTile();
                Destroy(gameObject, 2);
            }
        }
    }
    void SpawnWall()
    {
        int topLeftwall = 5;
        int topRightwall = 6;
        Transform leftWall = transform.GetChild(topLeftwall).transform;
        Transform rightWall = transform.GetChild(topRightwall).transform;

        int spawnWallIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(spawnWallIndex).transform;
        _spawnPoint1 = spawnWallIndex;

        int spawnWallIndex2 = Random.Range(2, 5);
        
        while(spawnWallIndex == spawnWallIndex2)
        {
             spawnWallIndex2 = Random.Range(2, 5);
        }
        Transform spawnPoint2 = transform.GetChild(spawnWallIndex2).transform;
        _spawnPoint2 = spawnWallIndex2;


        int spawnWallIndex3 = Random.Range(5, 7);
        Transform spawnPoint3 = transform.GetChild(spawnWallIndex3).transform;
        _spawnPoint3 = spawnWallIndex3;

        //eger ortada duvar varsa hemen ilerisinde 2 taraftada duvar olduguna emin ol
        if (spawnWallIndex == 2 || spawnWallIndex2 == 2 )
        {
            if(spawnWallIndex3 == 6)
            Instantiate(wallPrefab, leftWall.position, Quaternion.identity, transform);
            if(spawnWallIndex3 == 5)
            Instantiate(wallPrefab, rightWall.position, Quaternion.identity, transform);
        }

        //orta duvar oluþturma ve  kontrolü
        if (spawnWallIndex == 2 )
        {
            Instantiate(middleWallPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
        else 
        {
            Instantiate(wallPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
        if (spawnWallIndex2 == 2)
        {
            Instantiate(middleWallPrefab, spawnPoint2.position, Quaternion.identity, transform);
        }
        else
        {
            Instantiate(wallPrefab, spawnPoint2.position, Quaternion.identity, transform);
        }
        if (spawnWallIndex3 == 2)
        {
            Instantiate(middleWallPrefab, spawnPoint3.position, Quaternion.identity, transform);
        }
        else
        {
            Instantiate(wallPrefab, spawnPoint3.position, Quaternion.identity, transform);
        }

    }

    public GameObject coinPrefab; 

    void CoinPos()
    {
        int[] spawnPositions = {_spawnPoint1, _spawnPoint2, _spawnPoint3 };
        int randomIndex = Random.Range(0, 3);
        int spawnPoint = spawnPositions[randomIndex];

        int randomCoinPoint = Random.Range(2, 8);
        int randomCoinPoint2 = Random.Range(2, 8);

        if (spawnPoint == 2)
        {

                Vector3 coinSpawnPos = new Vector3
                (
                   -0.3f, transform.GetChild(2).transform.position.y, transform.GetChild(2).transform.position.z + randomCoinPoint
                );
            Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity, transform);

                Vector3 coinSpawnPos2 = new Vector3
                (
                    0.3f, transform.GetChild(2).transform.position.y, transform.GetChild(2).transform.position.z + randomCoinPoint2
                );
            Instantiate(coinPrefab, coinSpawnPos2, Quaternion.identity, transform);
        }

        if(spawnPoint == 3)
        {
            Vector3 coinSpawnPos = new Vector3
                (
                    -3.1f, transform.GetChild(3).transform.position.y, transform.GetChild(3).transform.position.z + 0.5f
                );
            Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity, transform);
        }

        if (spawnPoint == 4)
        {
            Vector3 coinSpawnPos = new Vector3
                (
                    3.1f, transform.GetChild(4).transform.position.y, transform.GetChild(4).transform.position.z + 0.5f
                );
            Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity, transform);
        }


    }

    void AsteroidSpawn()
    {

        GameObject[] prefabIndex = { AsteroidPrefab, AsteroidPrefab2, AsteroidPrefab3, AsteroidPrefab4};
        int prefabRandom = Random.Range(0, 4);

        prefabRandom = CheckIfSameAsteroid(prefabRandom);
        groundSpawner.lastAsteroid = prefabRandom;

        Transform spawnPoint = transform.GetChild(7).transform;
        Instantiate(prefabIndex[prefabRandom], spawnPoint.position, Quaternion.identity,transform);


    }

    public int CheckIfSameAsteroid(int _prefabRandom)
    {
        
        while (_prefabRandom == groundSpawner.lastAsteroid)
        {
            _prefabRandom = Random.Range(0, 4);
        }
        return _prefabRandom;
    }

}

    
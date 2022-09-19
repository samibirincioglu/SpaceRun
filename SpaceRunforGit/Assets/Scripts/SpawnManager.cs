using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Planet1, Planet2, Planet3, Planet4, Planet5;
    GameObject Player;
    int lastPlanet,lastPlanetLocation;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        InvokeRepeating("SpawnPlanets", 5f, 23f);
    }

    void SpawnPlanets()
    {
        GameObject[] planets = { Planet1, Planet2, Planet3, Planet4, Planet5 };
        int yValue = Random.Range(4,25);
        int spawnPointRandom = Random.Range(0,2);
        spawnPointRandom = CheckIfSamePlanetLocation(spawnPointRandom);
        lastPlanetLocation = spawnPointRandom;

        int planetPicker = Random.Range(0, 5);
        planetPicker = CheckIfSamePlanet(planetPicker);
        lastPlanet = planetPicker;

        Vector3[] spawnPoints = 
            { 
                    new Vector3
                    (
                        40,
                        yValue,
                        Player.transform.position.z + 400f
                    ),
                    new Vector3
                    (
                        -40,
                        yValue,
                        Player.transform.position.z + 400f
                    ) 
             };


        Instantiate(planets[planetPicker], spawnPoints[spawnPointRandom], Quaternion.identity, transform);

    }

     public int  CheckIfSamePlanet(int _planetPicker)
    {
        while (_planetPicker == lastPlanet)
        {
            _planetPicker = Random.Range(0,5);

        }
        return _planetPicker;
    }

    public int CheckIfSamePlanetLocation(int _spawnPointRandom)
    {
        while (_spawnPointRandom == lastPlanetLocation)
        {
            _spawnPointRandom = Random.Range(0, 2);

        }
        return _spawnPointRandom;
    }

}

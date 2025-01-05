using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using EventHandler = Utilities.EventHandler;

public class TerrainManager : MonoBehaviour
{
    public List<GameObject> terrainObjects;
    public float offsetY;
    private GameObject spawnObject;
    private int lastIndex;

    private void OnEnable()
    {
        EventHandler.GetPointEvent += OnGetPointEvent;
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
    }

    private void OnGetPointEvent(int obj)
    {
        CheckPosition();
    }

    private void Start()
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        if (transform.position.y-Camera.main.transform.position.y<offsetY/2)
        {
            transform.position = new Vector3(0, Camera.main.transform.position.y + offsetY);
            SpawnTerrain();
        }
    }

    private void SpawnTerrain()
    {
        var randomIndex=Random.Range(0,terrainObjects.Count);
        while (lastIndex==randomIndex)
        {
            randomIndex=Random.Range(0,terrainObjects.Count);
        }
        lastIndex=randomIndex;
        spawnObject=terrainObjects[randomIndex];
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}

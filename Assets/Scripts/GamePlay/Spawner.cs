using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public int direction;
    public List<GameObject> spawnObjects;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn),0.2f,Random.Range(5f,7f));
    }

    private void Spawn()
    {
        int index=Random.Range(0,spawnObjects.Count);
        GameObject target=Instantiate(spawnObjects[index],transform.position,Quaternion.identity,transform);
        target.GetComponent<MoveForward>().dir=direction;
    }
}

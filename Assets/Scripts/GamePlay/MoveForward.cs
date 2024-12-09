using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveForward : MonoBehaviour
{
    public float speed ;
    private Vector2 starPos;
    public int dir;

    private void Start()
    {
        starPos = transform.position;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x-starPos.x)>25)
        {
            Destroy(gameObject);
        }
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * (dir * speed * Time.deltaTime);
    }
}

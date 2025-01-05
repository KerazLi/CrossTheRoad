using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (Camera.main.transform.position.y-transform.position.y>25)
        {
            Destroy(gameObject);
        }
    }
}

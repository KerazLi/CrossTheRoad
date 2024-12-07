using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpDistance;
    private float moveDistance;
    private bool buttonHeld;
    private Vector2 distance;
    private bool isJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //FIXME:临时操作
        if (distance.y-transform.position.y<=0.1f)
        {
            isJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            rb.position = Vector2.Lerp(transform.position, distance, 0.134f); 
        }
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = jumpDistance;
            Debug.Log("JUMP!!"+" "+moveDistance);
            distance = new Vector2(transform.position.x,transform.position.y + moveDistance);
            isJump = true;
        }
    }
    public void LongJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = jumpDistance * 2;
            buttonHeld = true;
        }
        if (context.canceled && buttonHeld && !isJump)
        {
            distance = new Vector2(transform.position.x,transform.position.y + moveDistance);
            //Debug.Log("LONGJUMP!!"+" "+moveDistance);
            buttonHeld = false;
            isJump = true;
        }
    }
    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        
    }
}

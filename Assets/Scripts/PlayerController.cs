using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    public float jumpDistance;
    private float _moveDistance;
    private bool _buttonHeld;
    private Vector2 _distance;
    private bool _isJump;
    private bool _canJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //FIXME:临时操作
        /*if (_distance.y-transform.position.y<=0.1f)
        {
            _isJump = false;
        }*/
        if (_canJump)
        {
            JumpTrigger();
        }
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            _rb.position = Vector2.Lerp(transform.position, _distance, 0.134f); 
        }
        
    }

    #region Input 输入移动的回调函数
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !_isJump)
        {
            _moveDistance = jumpDistance;
            Debug.Log("JUMP!!"+" "+_moveDistance);
            _distance = new Vector2(transform.position.x,transform.position.y + _moveDistance);
            _canJump = true;
        }
    }
    public void LongJump(InputAction.CallbackContext context)
    {
        if (context.performed && !_isJump)
        {
            _moveDistance = jumpDistance * 2;
            _buttonHeld = true;
        }
        if (context.canceled && _buttonHeld && !_isJump)
        {
            _distance = new Vector2(transform.position.x,transform.position.y + _moveDistance);
            //Debug.Log("LONGJUMP!!"+" "+moveDistance);
            _buttonHeld = false;
            _canJump = true;
        }
    }
    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        
    }
    #endregion

    private void JumpTrigger()
    {
        _canJump = false;
        _animator.SetTrigger("Jump");
    }

    public void JumpAniamtionEvent()
    {
        _isJump = true;
    }
    public void JumpAnimationEndEvent()
    {
        _isJump = false;
    }
}

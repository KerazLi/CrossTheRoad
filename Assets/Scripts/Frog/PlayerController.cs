using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Up
    }

    private Direction dir;

    private Animator _animator;
    private Rigidbody2D _rb;
    public float jumpDistance;
    private float _moveDistance;
    private bool _buttonHeld;
    private Vector2 _distance;
    private bool _isJump;
    private bool _canJump;
    private Vector2 _touchPosition;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
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
            //Debug.Log("JUMP!!"+" "+_moveDistance);
            //_distance = new Vector2(transform.position.x,transform.position.y + _moveDistance);
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
            //Debug.Log("LONGJUMP!!"+" "+moveDistance);
            _buttonHeld = false;
            _canJump = true;
        }
    }
    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            var offset = ((Vector3)_touchPosition - transform.position).normalized;
            if (Mathf.Abs(offset.x)<=0.7f)
            {
                dir = Direction.Up;
            }else if (offset.x<0)
            {
                dir = Direction.Left;
            }else
            {
                dir = Direction.Right;
            }
        }
        
    }
    #endregion

    private void JumpTrigger()
    {
        _canJump = false;
        _animator.SetTrigger("Jump");
        switch (dir)
        {
            case Direction.Left:
                _animator.SetBool("isSlide",true);
                _distance = new Vector2(transform.position.x- _moveDistance,transform.position.y );
                transform.localScale = Vector3.one;
                break;
            case Direction.Right:
                _animator.SetBool("isSlide",true);
                _distance = new Vector2(transform.position.x+ _moveDistance,transform.position.y );
                transform.localScale = new Vector3(-1,1,1);
                break;
            case Direction.Up:
                _animator.SetBool("isSlide",false);
                _distance = new Vector2(transform.position.x,transform.position.y + _moveDistance);
                transform.localScale = Vector3.one;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void JumpAniamtionEvent()
    {
        _isJump = true;
        _spriteRenderer.sortingLayerName = "UI";
        Debug.Log(dir );
    }
    public void JumpAnimationEndEvent()
    {
        _isJump = false;
        _spriteRenderer.sortingLayerName = "Character";
    }
}

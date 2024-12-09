using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 定义方向枚举，用于表示玩家的移动方向
    private enum Direction
    {
        Left,
        Right,
        Up
    }
    
    // 当前移动方向
    private Direction dir;
    
    // Animator组件，用于控制角色动画
    private Animator _animator;
    // Rigidbody2D组件，用于物理模拟和移动
    private Rigidbody2D _rb;
    // 跳跃距离
    public float jumpDistance;
    // 移动距离
    private float _moveDistance;
    // 按钮是否被按住，用于持续移动控制
    private bool _buttonHeld;
    // 记录移动的距离，用于计算和更新位置
    private Vector2 _distance;
    // 是否正在跳跃
    private bool _isJump;
    // 是否可以跳跃，用于控制跳跃时机
    private bool _canJump;
    // 触摸位置，用于移动设备控制
    private Vector2 _touchPosition;
    // SpriteRenderer组件，用于渲染角色精灵
    private SpriteRenderer _spriteRenderer;
    
    private RaycastHit2D[] result=new RaycastHit2D[2];
    private bool isDead;
    public TerrainManager terrainManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 在游戏循环的更新阶段执行逻辑。
    /// </summary>
    private void Update()
    {
        // 检查是否可以跳跃
        if (_canJump)
        {
            // 触发跳跃动作
            JumpTrigger();
        }
    }

    /// <summary>
    /// 固定更新方法，用于处理物理引擎相关的逻辑。
    /// </summary>
    private void FixedUpdate()
    {
        // 检查是否处于跳跃状态
        if (_isJump)
        {
            // 当处于跳跃状态时，平滑地改变物体的位置，使其移动到目标距离。
            // 使用Vector2.Lerp实现从当前位置到目标位置的平滑过渡。
            _rb.position = Vector2.Lerp(transform.position, _distance, 0.134f); 
        }
        
    }

    #region Input 输入移动的回调函数
    /// <summary>
    /// 处理跳跃动作的函数
    /// </summary>
    /// <param name="context">包含输入动作回调上下文的信息</param>
    public void Jump(InputAction.CallbackContext context)
    {
        // 当跳跃动作被执行且当前状态不是跳跃时
        if (context.performed && !_isJump)
        {
            // 设置移动距离为跳跃距离
            _moveDistance = jumpDistance;
            //Debug.Log("JUMP!!"+" "+_moveDistance);
            //_distance = new Vector2(transform.position.x,transform.position.y + _moveDistance);
            // 允许跳跃
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
        transform.parent=null;
        _spriteRenderer.sortingLayerName = "UI";
        //Debug.Log(dir );
    }
    public void JumpAnimationEndEvent()
    {
        _isJump = false;
        _spriteRenderer.sortingLayerName = "Character";
        if (dir==Direction.Up && !isDead)
        {
            terrainManager.CheckPosition();
        }
    }

    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water") && !_isJump)
        {
            Physics2D.RaycastNonAlloc(transform.position + Vector3.up * 0.1f, Vector2.zero, result);
            bool isWater = true;
            foreach (var hit2D in result)
            {
                if (hit2D.collider==null)
                {
                    continue;
                }
                if (hit2D.collider.CompareTag("Wood"))
                {
                    //Debug.Log("在木板上");
                    transform.parent=hit2D.collider.transform;
                    isWater = false;
                }
                if (isWater && !_isJump)
                {
                    Debug.Log("InWater Game Over");
                    isDead = true;
                }
                
            }
        }
        if (other.gameObject.CompareTag("Border")||other.gameObject.CompareTag("Car"))
        {
            Debug.Log("GameOver");
            isDead = true;
        }if (other.gameObject.CompareTag("abstacle")&& !_isJump)
        {
            Debug.Log("Dead");
            isDead = true;
        }
    }

    
}

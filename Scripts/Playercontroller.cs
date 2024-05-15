using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class Plyaercontroller : MonoBehaviour
{
    public float walkSpeed = 5f;//走路速度
    public float runSpeed = 8f;//跑步速度
    public float airWalkSpeed = 3f;//空中跳跃速度
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;


    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            //返回跑步速度
                            return runSpeed;
                        }
                        else
                        {
                            //返回走路速度
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        //空气移动
                        return airWalkSpeed;
                    }
                }
                else
                {
                    //静态时速度为0
                    return 0;
                }
            }
            else
            {
                //运动锁
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                //反转局部刻度来实现玩家反转方向
                //x轴*-1来实现x轴正反变化，y轴不变
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    Rigidbody2D rb;
    Animator animator;

    //引用2D重力
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //计算人物移动 x水平 y跳跃，移动输入x*移动距离*帧数时间
    private void FixedUpdate()
    {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    //人物进行平面移动
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
            if (moveInput.x > 0 && !IsFacingRight )
            {
                //往前方进行移动
                IsFacingRight = true;
            }
            else if (moveInput.x < 0 && IsFacingRight )
            {
                //往反向进行移动
                IsFacingRight = false;
            }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
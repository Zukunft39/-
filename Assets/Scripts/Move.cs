using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerSpeed = 5f;
    private float moveX;//判断左右移动
    //以下为跳跃检测
    [Range(1, 10)]
    private float jumpSpeed = 6.3f;
    private bool moveJump;//判断是否按下跳跃
    private bool isGround;//判断是否在地面上
    public Transform groundCheck;//地面检测
    public LayerMask ground;
    //以下为跳跃优化
    public float fallMultiplier = 2.5f;//大跳的重力
    public float lowJumpMultiplier = 2f;//小跳的重力
    //以下为多段跳功能的实现
    public int jumpCount = 2;//跳跃次数
    private bool isJump;//表示跳跃状态
    //以下为传递跳跃次数
    public int k;//用作传递跳跃次数

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        PlayerMove();
        PlayerJumpByOnce();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,0.1f,ground);//地面检测
    }

    void PlayerMove()//左右移动
    {
        moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);

        if (moveX != 0)
        {
            transform.localScale = new Vector3(moveX, 1, 1);
        }
    }

    void PlayerJumpByOnce()//一段跳
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByOnce();
        //我是分界线，以下为优化跳跃手感内容
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier + 2) * Time.deltaTime;
        }
    }
    
    void JumpDetectionByOnce()//一段跳检测
    {
        if (moveJump && isGround)
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    void PlayerJumpByTwice()//二段跳
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByTwice();
        //我是分界线，以下为优化跳跃手感内容
        if (Input.GetButtonDown("Jump") && rb.velocity.y < 0 && jumpCount > 0 )
        {
            rb.velocity = Vector2.up * 7;                    
        }
        else if (rb.velocity.y < 0 && !Input.GetButtonDown("Jump"))
        {
            if (jumpCount > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            if (jumpCount == 0) rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else 
        {
            rb.gravityScale = 1.5f;
        }
    }
    void JumpDetectionByTwice()//二段跳检测
    {
        if (moveJump&&jumpCount> 0)
        {
            isJump = true;
        }
        if (isGround)//判断是否在地面
        {
            jumpCount = (int)2f;//四舍五入为2
        }
        if (isJump)
        {
            rb.AddForce(Vector2.up* jumpSpeed,ForceMode2D.Impulse);
            jumpCount--;
            isJump= false;
        }
    }
    void PlayerJumpByTwiceAndFly()//二段跳与滑翔
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByTwiceAndFly();
        //我是分界线，以下为优化跳跃手感内容
        if (Input.GetButtonDown("Jump") && rb.velocity.y < 0 && jumpCount > 0)
        {
            rb.velocity = Vector2.up * 7;
        }
        else if (rb.velocity.y < 0 && !Input.GetButton("Jump"))
        {
            if (jumpCount > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
            if (jumpCount == 0) rb.gravityScale = fallMultiplier + 2.5f;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
    void JumpDetectionByTwiceAndFly()//二段跳检测
    {
        if (moveJump && jumpCount > 0)
        {
            isJump = true;
        }
        if (isGround)//判断是否在地面
        {
            jumpCount = (int)2f;//四舍五入为2
        }
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }
    }
    public void Printf()
    {
        k = 2;
    }
}



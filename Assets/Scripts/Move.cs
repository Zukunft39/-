using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerSpeed = 5f;
    private float moveX;//�ж������ƶ�
    //����Ϊ��Ծ���
    [Range(1, 10)]
    private float jumpSpeed = 6.3f;
    private bool moveJump;//�ж��Ƿ�����Ծ
    private bool isGround;//�ж��Ƿ��ڵ�����
    public Transform groundCheck;//������
    public LayerMask ground;
    //����Ϊ��Ծ�Ż�
    public float fallMultiplier = 2.5f;//����������
    public float lowJumpMultiplier = 2f;//С��������
    //����Ϊ��������ܵ�ʵ��
    public int jumpCount = 2;//��Ծ����
    private bool isJump;//��ʾ��Ծ״̬
    //����Ϊ������Ծ����
    public int k;//����������Ծ����

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
        isGround = Physics2D.OverlapCircle(groundCheck.position,0.1f,ground);//������
    }

    void PlayerMove()//�����ƶ�
    {
        moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);

        if (moveX != 0)
        {
            transform.localScale = new Vector3(moveX, 1, 1);
        }
    }

    void PlayerJumpByOnce()//һ����
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByOnce();
        //���Ƿֽ��ߣ�����Ϊ�Ż���Ծ�ָ�����
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier + 2) * Time.deltaTime;
        }
    }
    
    void JumpDetectionByOnce()//һ�������
    {
        if (moveJump && isGround)
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    void PlayerJumpByTwice()//������
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByTwice();
        //���Ƿֽ��ߣ�����Ϊ�Ż���Ծ�ָ�����
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
    void JumpDetectionByTwice()//���������
    {
        if (moveJump&&jumpCount> 0)
        {
            isJump = true;
        }
        if (isGround)//�ж��Ƿ��ڵ���
        {
            jumpCount = (int)2f;//��������Ϊ2
        }
        if (isJump)
        {
            rb.AddForce(Vector2.up* jumpSpeed,ForceMode2D.Impulse);
            jumpCount--;
            isJump= false;
        }
    }
    void PlayerJumpByTwiceAndFly()//�������뻬��
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByTwiceAndFly();
        //���Ƿֽ��ߣ�����Ϊ�Ż���Ծ�ָ�����
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
    void JumpDetectionByTwiceAndFly()//���������
    {
        if (moveJump && jumpCount > 0)
        {
            isJump = true;
        }
        if (isGround)//�ж��Ƿ��ڵ���
        {
            jumpCount = (int)2f;//��������Ϊ2
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



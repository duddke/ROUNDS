using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SY_PlayerMove : MonoBehaviourPun
{
    public enum State
    {
        Idle,
        Die
    }

   public State state = State.Idle;

    [SerializeField]
    float speed = 5f;
    float jumpForce = 1.5f;
    float h, v, flipRatio = 1f;
    public float maxSpeed;
    bool isJump;

    public Vector3 playerDir;

    Rigidbody2D rb;

    public static SY_PlayerMove instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        PlayerFlip();
        WallWalk();
        JumpHandle();

        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Die:
                Die();
                break;
        }

    }

    public void Idle()
    {

    }

    public void Die()
    {
        // ���� ������ ��ȯ
        print("Die");
        
    }

    void Movement()
    {
        // ����� �Է¿� ����
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;

        //Vector2 moveVec = isHorizontalMove ? new Vector2(hInput, 0) : new Vector2(0, vInput);
        //rb.velocity = moveVec * speed;

        // �̵��ϰ� �ʹ�.
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // �ִ� ���ǵ� maxSpeed �� ���� ���ϰ� ��
        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if (rb.velocity.x < -maxSpeed)
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);

        //photonView.RPC("Movement", RpcTarget.All);

        if (h == 0 && v != 0)
        {
            //isHorizontalMove = false;
            //if (vInput == 1)
            //    playerDir = Vector3.up;
            //else if (vInput == -1)
            //    playerDir = Vector3.down;
        }
        else
        {   // ����������� ���� ����
            //isHorizontalMove = true;
        }

    }

    void JumpHandle()
    {
        if (Input.GetButtonDown("Jump")) //���� Ű�� ������ ��
        {
            if (isJump == false) //���� ������ ���� ��
            {
                Jump();
                //isJump = true;
            }
            else return; //���� ���� ���� �������� �ʰ� �ٷ� return.
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //�������� ���� �ش�.
        isJump = true;
    }

    // �̵��ϴ� �������� �ٶ󺸱�
    void PlayerFlip()
    {
        Vector3 FlipScale = transform.localScale;

        if (Input.GetAxis("Horizontal") < 0)
        {
            FlipScale.x = -flipRatio;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            FlipScale.x = flipRatio;
        }

        //transform.localScale = FlipScale;
    }


    // ���� ������ ���Ʒ��� ������
    void WallWalk()
    {
        if (h > 0)
            playerDir = Vector2.right;
        else if (h < 0)
            playerDir = Vector2.left;

        // ����������� �̵��� �� DrawRay
        Debug.DrawRay(rb.position, playerDir, Color.blue);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, playerDir, 1f, LayerMask.GetMask("Wall"));

        //  ���̿� ������ ���Ϸ� �̵��� �� �ִ�.
        if (rayHit.collider != null)
        {
            //rb.velocity = Vector2.zero;
            Debug.Log(rayHit.collider.tag);

            // ���Ϸ� �̵��ϰ� �ʹ�.
            rb.AddForce(Vector2.up * v, ForceMode2D.Impulse);
            isJump = false;

            // �ִ� ���ǵ� maxSpeed �� ���� ���ϰ� ��
            if (rb.velocity.y > maxSpeed)
                rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
            else if (rb.velocity.y < -maxSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxSpeed);
        }
        else
        {

        }
    }

    [PunRPC]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������ ������
        if (collision.gameObject != null)
        {
            // ���� ����
            isJump = false;
            print("ground");
        }
        // �Ѿ˿� ������
        if (collision.gameObject.layer == 30)
        {
            //������ �Լ� ����
            SY_HpBar.instance.HandleHp();
        }
    }
}

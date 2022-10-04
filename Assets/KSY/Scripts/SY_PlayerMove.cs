using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using System;

public class SY_PlayerMove : MonoBehaviourPun
{
    //����
    public bool isCreated;
    Vector3 moveDir;
    Vector3 wallWalkDir;
    Vector3 jumpDir;
    RaycastHit2D rayHit;
    public string nicName;
    //
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
    public bool isJump;

    public Vector3 playerDir;

    Rigidbody2D rb;

    public static SY_PlayerMove instance;

    private void Awake()
    {
        instance = this;
        //����
        nicName = photonView.Owner.NickName;
        isCreated = photonView.IsMine;
        //
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.players.Add(gameObject);
        if (!PhotonNetwork.IsMasterClient && photonView.IsMine)
        {
            photonView.TransferOwnership(PhotonNetwork.MasterClient);
            //��ȣ���� ������� ����. Ư�� �ι����� �ѱ� ���� Ư�� �÷��̾��� photonView.Owner
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            Destroy(rb);
            GetComponent<Collider2D>().enabled = false;
        }
        if (isCreated)
        {
            if ((gameObject.name.Contains("Red") && SYA_CardManager.Instance.redCard[4])||
                (gameObject.name.Contains("Blue") && SYA_CardManager.Instance.blueCard[4]))
            {
                Debug.Log("playmovespeed");
                h = Input.GetAxis("Horizontal") * speed * 10f;
                v = Input.GetAxis("Vertical") * speed * 10f;

                photonView.RPC("RpcMove", RpcTarget.MasterClient, h);
            }
            else
            {
                // ����� �Է� �ޱ�
                h = Input.GetAxis("Horizontal") * speed;
                v = Input.GetAxis("Vertical") * speed;
                //Vector2 moveVec = isHorizontalMove ? new Vector2(hInput, 0) : new Vector2(0, vInput);
                //rb.velocity = moveVec * speed;
                photonView.RPC("RpcMove", RpcTarget.MasterClient, h);
            }

           
            #region ��Ÿ��

            // ����������� �̵��� �� DrawRay
            //���� �д� ���ǽ�
            photonView.RPC("RpcWallWalk", RpcTarget.MasterClient, h, v); 
            //
            #endregion

            #region ����
            if (Input.GetButtonDown("Jump"))
            {
                if (isJump) return;
                {
                    photonView.RPC("Jump", RpcTarget.MasterClient);
                    isJump = true;
                }
                
            }
            #endregion
        }
        //�̵�
        if (PhotonNetwork.IsMasterClient)
        {

            //�̵��ϰ� �ʹ�.
            if (moveDir.sqrMagnitude > 0)
            {
                rb.AddForce(moveDir);
                // �ִ� ���ǵ� maxSpeed �� ���� ���ϰ� ��
                if (rb.velocity.x > maxSpeed)
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                else if (rb.velocity.x < -maxSpeed)
                    rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }

            if (wallWalkDir.sqrMagnitude > 0)
            {
                //��Ÿ�� ������ �� �̵�
                Debug.DrawRay(rb.position, playerDir, Color.blue);
                rayHit = Physics2D.Raycast(rb.position, playerDir, 1f, LayerMask.GetMask("Wall"));
                if (rayHit.collider != null)
                {
                    if (rayHit.collider != null)
                    {
                        // ���Ϸ� �̵��ϰ� �ʹ�.
                        rb.AddForce(wallWalkDir);
                        photonView.RPC("RpcIsJump", RpcTarget.All, false);
                        // �ִ� ���ǵ� maxSpeed �� ���� ���ϰ� ��
                        if (rb.velocity.y > maxSpeed)
                            rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
                        else if (rb.velocity.y < -maxSpeed)
                            rb.velocity = new Vector2(rb.velocity.x, -maxSpeed);
                    }

                }
            }
            //
            //if (Input.GetButtonDown("Jump") && jumpDir.sqrMagnitude > 0)
            //{
            //    if (isJump) return;
            //    rb.AddForce(jumpDir, ForceMode2D.Impulse); //�������� ���� �ش�.
            //    isJump = true;
            //}
        }
        PlayerFlip();

        //if (isGround)
        //{
        //    isJump = false;
        //}

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

    public void JumpOn()
    {
        if(PhotonNetwork.IsMasterClient&&GetComponent<Rigidbody2D>())
        photonView.RPC("Jump", RpcTarget.All);
    }

    [PunRPC]
    public void Jump()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        jumpDir = Vector2.up * jumpForce;
        rb.AddForce(jumpDir, ForceMode2D.Impulse);
    }

    //�̵��� ���� Rpc
    [PunRPC]
    void RpcMove(float h)
    {
        moveDir = Vector2.right * h;
    }

    //��Ÿ�⿡ ���� rpc
    [PunRPC]
    void RpcWallWalk(float h,  float v)
    {
        //rb.velocity = Vector2.zero;
        //Debug.Log(rayHit.collider.tag);

        //���� �׸���
        if (h > 0)
            playerDir = Vector2.right;
        else if (h < 0)
            playerDir = Vector2.left;

        wallWalkDir = Vector2.up * v;
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������ ������
        if (collision.gameObject != null)
        {
            // ���� ����
            photonView.RPC("RpcIsJump", RpcTarget.All, false);
            print("ground");
        }
        if(collision.gameObject.layer==31)
        {

            photonView.RPC("RpcIsJump", RpcTarget.All, false);
        }
        // �Ѿ˿� ������
        if (collision.gameObject.layer == 30)
        {
            //������ �Լ� ����
            GetComponentInChildren<SY_HpBar>().HandleHp();
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    [PunRPC]
    void RpcIsJump(bool jump)
    {
        isJump = jump;
    }

}

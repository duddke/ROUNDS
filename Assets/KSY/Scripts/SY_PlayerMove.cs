using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SY_PlayerMove : MonoBehaviourPun
{
    //영아
    public bool isCreated;
    Vector3 moveDir;
    Vector3 wallWalkDir;
    Vector3 jumpDir;
    RaycastHit2D rayHit;
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
        //영아
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
            //괄호안의 사람에게 전달. 특정 인물에게 넘길 떄는 특정 플레이어의 photonView.Owner
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!photonView.IsMine)
        //{
        //    if (rb)
        //        Destroy(rb);
        //    if (GetComponent<HingeJoint2D>())
        //        Destroy(GetComponent<HingeJoint2D>());
        //    if (GetComponent<Collider2D>())
        //        GetComponent<Collider2D>().enabled = false;
        //}
        if (isCreated)
        {
            // 사용자 입력 받기
            h = Input.GetAxis("Horizontal") * speed;
            v = Input.GetAxis("Vertical") * speed;
            //Vector2 moveVec = isHorizontalMove ? new Vector2(hInput, 0) : new Vector2(0, vInput);
            //rb.velocity = moveVec * speed;
            photonView.RPC("RpcMove", RpcTarget.MasterClient, h);

            #region 벽타기

            // 수평방향으로 이동할 때 DrawRay
            //레이 읽는 알피시
            photonView.RPC("RpcWallWalk", RpcTarget.MasterClient, h, v);
            //
            #endregion

            #region 점프
            if (Input.GetButtonDown("Jump"))
            {
                photonView.RPC("Jump", RpcTarget.MasterClient);

            }
            #endregion
        }
        //이동
        if (PhotonNetwork.IsMasterClient)
        {

            // 이동하고 싶다.
            if (moveDir.sqrMagnitude > 0)
            {
                rb.AddForce(moveDir);
                // 최대 스피드 maxSpeed 를 넘지 못하게 함
                if (rb.velocity.x > maxSpeed)
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                else if (rb.velocity.x < -maxSpeed)
                    rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
            //
            if (wallWalkDir.sqrMagnitude > 0)
            {
                //벽타기 가능할 때 이동
                Debug.DrawRay(rb.position, playerDir, Color.blue);
                rayHit = Physics2D.Raycast(rb.position, playerDir, 1f, LayerMask.GetMask("Wall"));
                if (rayHit.collider != null)
                {
                    if (rayHit.collider != null)
                    {
                        // 상하로 이동하고 싶다.
                        rb.AddForce(wallWalkDir);
                        isJump = false;
                        // 최대 스피드 maxSpeed 를 넘지 못하게 함
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
            //    rb.AddForce(jumpDir, ForceMode2D.Impulse); //위쪽으로 힘을 준다.
            //    isJump = true;
            //}
        }

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
        // 다음 씬으로 전환
        print("Die");

    }

    [PunRPC]
    public void Jump()
    {
        if (isJump) return;

        isJump = true;
        jumpDir = Vector2.up * jumpForce;
        rb.AddForce(jumpDir, ForceMode2D.Impulse);
    }

    //이동에 대한 Rpc
    [PunRPC]
    void RpcMove(float h)
    {
        moveDir = Vector2.right * h;
    }

    //벽타기에 대한 rpc
    [PunRPC]
    void RpcWallWalk(float h, float v)
    {
        //rb.velocity = Vector2.zero;
        //Debug.Log(rayHit.collider.tag);

        //레이 그리기
        if (h > 0)
            playerDir = Vector2.right;
        else if (h < 0)
            playerDir = Vector2.left;

        wallWalkDir = Vector2.up * v;

    }

    // 이동하는 방향으로 바라보기
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
        // 충하지 않으면
        if (collision.gameObject != null)
        {
            // 점프 없음
            isJump = false;
            print("ground");
        }
        // 총알에 맞으면
        if (collision.gameObject.layer == 30)
        {
            //데미지 함수 실행
            SY_HpBar.instance.HandleHp();
        }
        if (collision.gameObject.layer == 31)
        {

            isJump = false;
        }
    }
    bool isGround;
}

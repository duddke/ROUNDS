using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SY_PlayerMove : MonoBehaviourPun
{
    [SerializeField]
    float speed = 5f;
    float jumpForce = 10f;
    float h, v, flipRatio = 1f;
    public float maxSpeed;
    bool isJump;
    bool isHorizontalMove;
   
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
        Jump();
    }

    
    void Movement()
    {
        // 사용자 입력에 따라
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;

        //Vector2 moveVec = isHorizontalMove ? new Vector2(hInput, 0) : new Vector2(0, vInput);
        //rb.velocity = moveVec * speed;

        // 이동하고 싶다.
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 최대 스피드 maxSpeed 를 넘지 못하게 함
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
        {   // 수평방향으로 방향 설정
            //isHorizontalMove = true;
        
        }

        // 점프구현
        //if (rb.velocity.y == 0)
        //{
        //    isJump = true;
        //}
        //else
        //{
        //    isJump = false;
        //}

        //if (Input.GetButtonDown("Jump") && isJump)
        //{
        //    rb.AddForce(Vector2.up * jumpForce);
        //}
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump")) //점프 키가 눌렸을 때
        {
            if (isJump == false) //점프 중이지 않을 때
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //위쪽으로 힘을 준다.
                isJump = true;
            }
           else return; //점프 중일 때는 실행하지 않고 바로 return.

        }
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


    // 벽에 닿으면 위아래로 움직임
    void WallWalk()
    {
        if (h > 0)
            playerDir = Vector2.right;
        else if (h < 0)
            playerDir = Vector2.left;

        // 수평방향으로 이동할 때 DrawRay
        Debug.DrawRay(rb.position, playerDir, Color.blue);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, playerDir, 1f, LayerMask.GetMask("Wall"));
        
        //  레이에 닿으면 상하로 이동할 수 있다.
        if (rayHit.collider != null)
        {
            //rb.velocity = Vector2.zero;
            Debug.Log(rayHit.collider.tag);
             
            // 상하로 이동하고 싶다.
            rb.AddForce(Vector2.up * v, ForceMode2D.Impulse);
            isJump = false;

            // 최대 스피드 maxSpeed 를 넘지 못하게 함
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
        // 충동하지 않으면
        if (collision.gameObject != null)
        {
            // 점프 없음
            isJump = false;
            print("ground");
        }
    }
}

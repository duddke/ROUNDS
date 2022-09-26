using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SY_Bullet : MonoBehaviourPun
{
    public GameObject explosion;
    public float bulletSpeed;
    private Sprite[] sprites;



    public Vector3 dir;
    Rigidbody2D rb;

    public static SY_Bullet instance;

    GameObject target;
    private void Awake()
    {
        instance = this;
    }
    public bool red; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(rb);
            GetComponent<Collider2D>().enabled = false;
        }

        if (!red)
        {
            target = GameObject.Find("Red_Player");
        }
        else
        {
            target = GameObject.Find("Blue_Player");
        }
        //dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //�Ұ��� ���� �Լ� ����
        if (barrageBullet)
        {
            BarrageBullet();
        }
        if (followBullet)
        {
            FollowBullet();
        }
        if (bounceBullet)
        {
            BounceBullet();
        }
        if (huge)
        {
            Huge();
        }
        if (poison)
        {
            Poison();
        }
        if (quickShoot)
        {
            QuickShoot();
        }
        if (bigBullet)
        {
            BigBullet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            transform.right = rb.velocity;
        }
    }


    public bool barrageBullet;
    // �Ϲ� �Ѿ�
    public void BarrageBullet()
    {
        dir.Normalize();
        // �߻��ϰ� �ʹ�.
        rb.AddForce(dir * bulletSpeed);
    }


    // ����ź
    Quaternion rotTarget;
    public bool followBullet;
    public float followSpeed;
    float rotSpeed = 10f;
    public float creatTime = 3f;
    //Camera m_cam = null;
    public void FollowBullet()
    {
        print("FollowBullet");
        StopCoroutine("OnFollowBullet");
        StartCoroutine("OnFollowBullet");
    }

    [SerializeField] [Range(100f, 2000f)] float BBSpeed;
    // 2�� �ñ�� �Ѿ�
    public bool bounceBullet;
    int count;
    public void BounceBullet()
    {

        //GetComponent<SY_BounceBullet>();
        Debug.Log("Bounce");
        rb.AddForce(dir * BBSpeed);
        //rb.AddForce(dir * BBSpeed);
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);
        //dir.Normalize();
        //// �߻��ϰ� �ʹ�.
        //rb.AddForce(dir * bulletSpeed);
    }

    // �߻��� �÷��̾� hp ���� �Ѿ�
    public bool huge;
    public void Huge()
    {
        print("huge");
        dir.Normalize();
        // �߻��ϰ� �ʹ�.
        rb.AddForce(dir * bulletSpeed);
    }

    public bool poison;
    // ���� �÷��̾� ü�� ���� ���̴� �Ѿ�
    public void Poison()
    {
        print("poison");
        dir.Normalize();
        // �߻��ϰ� �ʹ�.
        rb.AddForce(dir * bulletSpeed);
    }

    // �Ѿ� �ӵ� ����
    public bool quickShoot;
    public void QuickShoot()
    {
        dir.Normalize();
        rb.AddForce(dir * bulletSpeed * 2f);
    }  
    
    // ĳ���� �̵��ӵ� ����
    public bool speedDouble;
    public void Chase()
    {
        print("SpeedDouble");
        dir.Normalize();
        // �߻��ϰ� �ʹ�.
        rb.AddForce(dir * bulletSpeed);
    }

    // ���� �Ѿ�
    public bool bigBullet;
    [SerializeField] [Range(1f, 5f)] float scaleSpeed = 1f;
    public void BigBullet()
    {

        dir.Normalize();

        rb.AddForce(dir * bulletSpeed);

        StartCoroutine("OnBigBullet");

    }
    IEnumerator OnBigBullet()
    {
        float coCurrentTime = 0;

        while (coCurrentTime < 5f)
        {
            coCurrentTime += Time.deltaTime;
            transform.localScale += 10f * Vector3.one * Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            photonView.RPC("RpcCreate", RpcTarget.MasterClient, transform.position);
               // transform.position - Vector3.forward;
        }

        if (!bounceBullet)
            PhotonNetwork.Destroy(gameObject);

        if (collision.gameObject.tag == "Player") // �浹 ���ӿ�����Ʈ�� �÷��̾�� ��ü
        {
            if (huge)
            {
                if (collision.gameObject.name.Contains("Red"))
                    GameObject.Find("Blue_Player").GetComponentInChildren<SY_HpBar>().HugeHp();
                else
                    GameObject.Find("Red_Player").GetComponentInChildren<SY_HpBar>().HugeHp();
                //GameObject.Find("Player").GetComponentInChildren<SY_HpBar>().HugeHp();
            }
            if (poison)
            {
                collision.gameObject.GetComponentInChildren<SY_HpBar>().PoisonHp();
                //GameObject.Find("Player").GetComponentInChildren<SY_HpBar>().PoisonHp();

            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (bounceBullet)
            {
                // �浹 Ƚ���� 2��             
                count += 1;
                if (count > 2)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }

    [PunRPC]
    void RpcCreate(Vector3 dir)
    {
        PhotonNetwork.Instantiate("WFX_Explosion Small", dir, Quaternion.identity);
    }


    float currentTime;
    
    // ���� �̻��� 
    IEnumerator OnFollowBullet()
    {
        float coCurrentTime = 0;
        while (coCurrentTime < 3.0f)
        {
            currentTime += Time.deltaTime;
            if (currentTime < 3)
            {

            coCurrentTime += Time.deltaTime;
            
            dir = (target.transform.position - transform.position);
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);

            // ȸ���ӵ�
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
            //rb.velocity = new Vector2(dir.x * speed, dir.y * speed);

            // �Ѿ� �ӵ�
            rb.velocity = transform.right * 5f;
            }

            dir.Normalize();
            // �߻��ϰ� �ʹ�.
            rb.AddForce(dir * followSpeed);
            yield return null;
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SY_Bullet : MonoBehaviourPun
{
    public GameObject explosion;
    public float bulletSpeed;
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

        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("RpcFindTarget", RpcTarget.MasterClient, red);
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
        if (brawler) //0928
        {
            Brawler();
        }
        if (cannon) //0928
        {
            Cannon();
        }
        if (chase)
        {
            Chase();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            transform.right = rb.velocity;
        }
    }

    [PunRPC]
    void RpcFindTarget(bool red)
    {
        if (red)
        {
            target = GameObject.FindWithTag("Blue_Player");
        }
        else
        {
            target = GameObject.FindWithTag("Red_Player");
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
        Debug.Log("Bounce");
        rb.AddForce(dir * BBSpeed);
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

    public bool cannon;
    // ������ HP 2�� ���� 
    public void Cannon()   // 0928
    {
        print("cannon");
        dir.Normalize();
        // �߻��ϰ� �ʹ�.
        rb.AddForce(dir * bulletSpeed);
    }

    public bool brawler;
    // 3�� ���� HP ����
    public void Brawler() //0928
    {
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
    public bool chase;
    public void Chase()
    {
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
            transform.localScale += 5f * Vector3.one * Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        bool destroy = false;
        if (collision.gameObject.tag == "Wall")
        {
            photonView.RPC("RpcCreate", RpcTarget.All, transform.position);
            //PhotonNetwork.Instantiate("WFX_Explosion Small", transform.position, Quaternion.identity);
            // transform.position - Vector3.forward;
        }

        if (!bounceBullet)
            destroy = true;

        if (collision.gameObject.name.Contains("Player")) // �浹 ���ӿ�����Ʈ�� �÷��̾�� ��ü
        {
            destroy = true;
            if (poison)
            {
                collision.gameObject.GetComponentInChildren<SY_HpBar>().PoisonHp();
            }
            if (cannon)
            {
                collision.gameObject.GetComponentInChildren<SY_HpBar>().Cannon(); //0928
            }

            if (huge)
            {
                if (collision.gameObject.name.Contains("Red"))
                    GameObject.FindWithTag("Blue_Player").GetComponentInChildren<SY_HpBar>().HugeHp();
                else
                    GameObject.FindWithTag("Red_Player").GetComponentInChildren<SY_HpBar>().HugeHp();
            }

            if (brawler) //0928
            {
                if (collision.gameObject.name.Contains("Blue"))
                    GameObject.FindWithTag("Red_Player").GetComponentInChildren<SY_HpBar>().Brawler();

                else
                    GameObject.FindWithTag("Blue_Player").GetComponentInChildren<SY_HpBar>().Brawler();
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
                    destroy = true;
                }
            }
        }
        if(destroy)
        {
            //PhotonNetwork.Destroy(gameObject);
            photonView.RPC("RpcDestroy", RpcTarget.All);
        }
    }

    [PunRPC]
    void RpcCreate(Vector3 dir)
    {
        Instantiate(explosion, dir, Quaternion.identity);
    }

    [PunRPC]
    void RpcDestroy()
    {
        Destroy(gameObject);
    }


    // ���� �̻��� 
    IEnumerator OnFollowBullet()
    {
        float coCurrentTime = 0;
        while (coCurrentTime < 10.0f)
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
            dir.Normalize();
            // �߻��ϰ� �ʹ�.
            rb.AddForce(dir * followSpeed);
            yield return null;
        }
    }



}

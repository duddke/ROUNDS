using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_Bullet : MonoBehaviour
{
    public GameObject explosion;
    public float bulletSpeed;

    Vector2 dir;
    Rigidbody2D rb;

    public static SY_Bullet instance;

    GameObject target;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Blue_Player");
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //불값에 따라 함수 실행
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

    }


    public bool barrageBullet;
    // 일반 총알
    public void BarrageBullet()
    {
        print("barrage");
        dir.Normalize();
        // 발사하고 싶다.
        rb.AddForce(dir * bulletSpeed);
    }


    // 유도탄
    Quaternion rotTarget;
    public bool followBullet;
    float rotSpeed = 1f;
    float currentTime;
    public float creatTime = 3f;
    Vector3 targetdir;
    //Camera m_cam = null;
    public void FollowBullet()
    {
        print("FollowBullet");
        StopCoroutine("OnFollowBullet");
        StartCoroutine("OnFollowBullet");


    }

    // 2번 팅기는 총알
    public bool bounceBullet;
    public void BounceBullet()
    {
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);
        dir.Normalize();
        // 발사하고 싶다.
        rb.AddForce(dir * bulletSpeed);
    }

    // 발사한 플레이어 hp 증가 총알
    public bool huge;
    public void Huge()
    {
        dir.Normalize();
        // 발사하고 싶다.
        rb.AddForce(dir * bulletSpeed);
    }

    public bool poison;

    // 상대방 플레이어 체력 점점 깍이는 총알
    public void Poison()
    {
        dir.Normalize();
        // 발사하고 싶다.
        rb.AddForce(dir * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explo = Instantiate(explosion);
        explo.transform.position = transform.position;

        Destroy(gameObject);

        if (collision.gameObject.tag == "Wall") // 충돌 게임오브젝트를 플레이어로 교체
        {
            if (huge)
            {
                //if (collision.gameObject.name.Contains("Red"))
                //GameObject.Find("Blue_Player").GetComponentInChildren<SY_HpBar>().HugeHp();
                //else
                //GameObject.Find("Red_Player").GetComponentInChildren<SY_HpBar>().HugeHp();
                GameObject.Find("Player").GetComponentInChildren<SY_HpBar>().HugeHp();


            }
            if (poison)
            {
                //collision.gameObject.GetComponentInChildren<SY_HpBar>().PoisonHp();
                GameObject.Find("Player").GetComponentInChildren<SY_HpBar>().PoisonHp();

            }
        }
    }

    IEnumerator OnFollowBullet()
    {
        float coCurrentTime = 0;
        while (coCurrentTime < 3.0f)
        {
            currentTime += Time.deltaTime;
            if (currentTime > creatTime)
            {
                 dir = (target.transform.position - transform.position);
                 dir.Normalize();

                 float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                 rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);

                 // 회전속도
                 transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
                 //rb.velocity = new Vector2(dir.x * speed, dir.y * speed);

                 // 총알 속도
                 rb.velocity = transform.right * 20f;
            }
            else
            {
                dir.Normalize();
                // 발사하고 싶다.
                rb.AddForce(dir * bulletSpeed);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

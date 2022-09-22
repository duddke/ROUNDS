using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_Bullet : MonoBehaviour
{
    public GameObject explosion;
    public float bulletSpeed;

    Vector3 dir;
    Rigidbody2D rb;

    public static SY_Bullet instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //�Ұ��� ���� �Լ� ����

    }

    // �Ϲ� �Ѿ�
    public void NomalBullet()
    {
        // �߻��ϰ� �ʹ�.
        rb.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
    }


    // ����ź
    GameObject target;
    [SerializeField] float speed = 2f, rotSpeed = 2f;
    Quaternion rotTarget;
    public float creatTime = 1f;
    public float currentTime;
    public void FollowBullet()
    {
        currentTime += Time.deltaTime;

        if (currentTime > creatTime)
        {
            dir = (target.transform.position - transform.position);
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);

            // ȸ���ӵ�
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
            //rb.velocity = new Vector2(dir.x * speed, dir.y * speed);

            // �Ѿ� �ӵ�
            rb.velocity = transform.right * 20f;
        }


    }

    // 2�� �ñ�� �Ѿ�
    bool bounce;
    public void BounceBullet()
    {
        bounce = true;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explo = Instantiate(explosion);
        explo.transform.position = transform.position;

        Destroy(gameObject);

        if (bounce == true)
        {
            collision.rigidbody.AddForce(new Vector3(300, 300, 0));
        }
    }
}

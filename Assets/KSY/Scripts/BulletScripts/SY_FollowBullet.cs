using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_FollowBullet : MonoBehaviour
{
    GameObject target;
    [SerializeField] float speed = 2f, rotSpeed = 2f;

    Quaternion rotTarget;
    Vector3 dir;
    Rigidbody2D rb;

    Camera m_cam = null;

    public static SY_FollowBullet instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Blue_Player");
        m_cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GuideMissile();
    }

    void GuideMissile()
    {
        // 마우스 입력 방향으로
        Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
       
        dir = (target.transform.position  - transform.position);
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            //Instantiate(FirePos, transform.position, Quaternion.identity);
            //Destroy(gameObject);   
    }
}

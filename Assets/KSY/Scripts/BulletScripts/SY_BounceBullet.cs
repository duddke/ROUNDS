using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_BounceBullet : MonoBehaviour
{
    [SerializeField] [Range(500f, 2000f)] float bulletSpeed;
    Rigidbody2D rigid;
    float randomX, randomY;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            randomX = Random.Range(-1f, 1f);
            randomY = Random.Range(-1f, 1f);

            Vector2 dir = new Vector2(randomX, randomY).normalized;

            rigid.AddForce(transform.right * bulletSpeed);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //rigid.AddForce(new Vector3(300, 300, 0));
        //float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);

    }
}

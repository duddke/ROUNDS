using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_BounceBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    public float bulletSpeed;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //rigid.AddForce(new Vector3(300, 300, 0));
        float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);

    }
}

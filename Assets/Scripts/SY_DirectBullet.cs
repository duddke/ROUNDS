using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_DirectBullet : MonoBehaviour
{
    // ¼Óµµ
    public float speed = 10f;
    //Vector3 dir = Vector2.right;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        //if (PlayerMove.instance.playerDir != Vector3.zero)
        //{
        //    dir = PlayerMove.instance.playerDir;
        //}
    }
    

    // Update is called once per frame
    void Update()
    {
        dir.Normalize();

        transform.position += dir* speed * Time.deltaTime;
        
        //transform.right = GetComponent<Rigidbody2D>().velocity;
        //transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}

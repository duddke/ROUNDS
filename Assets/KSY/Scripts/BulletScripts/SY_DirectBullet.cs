using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_DirectBullet : MonoBehaviour
{
    public GameObject explosion;
    public float bulletSpeed;

    Vector3 dir;

    public static SY_DirectBullet instance;

    private void Awake()
    {
        instance = this;
    }

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
        GameObject explo = Instantiate(explosion);
        explo.transform.position = transform.position;

        Destroy(gameObject);
    }
}

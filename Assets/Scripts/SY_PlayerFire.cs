using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_PlayerFire : MonoBehaviour
{
    // �ʿ�Ӽ� : �Ѿ˰���
    public GameObject bulletFactory; // �Ѿ� ����
    public Transform firePosition; // �Ѿ� ���� ��ġ
    float bulletSpeed = 5;
    public Vector3 dir; // ���콺 ������ ������ ������ ����
    Camera cam; // ����ī�޶�

    public static SY_PlayerFire instance;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!bulletFactory.name.Contains("ParaBullet"))
        //{
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.transform.position = firePosition.transform.position;
        //}
        
        //else
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        GameObject t_arrow = Instantiate(bulletFactory);
        //        t_arrow.transform.position = firePosition.transform.position;
        //        //ParaBullet.instance.m_tfArrow.position, ParaBullet.instance.m_tfArrow.rotation
        //        t_arrow.GetComponent<Rigidbody2D>().velocity = t_arrow.transform.right * 10f;
        //    }
        Shoot();
        dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        //dir.Normalize();
    }

    private bool m_IsStart;
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
            bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}

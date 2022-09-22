using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SY_FirePos: MonoBehaviourPun
{
    [SerializeField] GameObject BulletFactory = null;
    //[SerializeField] GameObject followBullet = null;
    //[SerializeField] GameObject bounceBullet = null;
    [SerializeField] Transform FirePos = null;

    Camera m_cam = null;
    public Vector2 t_mousePos;
    public Vector2 t_direction;
    

    public static SY_FirePos instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
        SY_Bullet.instance.NomalBullet();
        SY_Bullet.instance.FollowBullet();
        SY_Bullet.instance.BounceBullet();
    }

    void LookAtMouse()
    {
        //// ���콺 ��ġ
        //Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        //// ���콺 ��ġ ��������
        //Vector2 t_direction = new Vector2(t_mousePos.x - FirePos.position.x, t_mousePos.y - FirePos.position.y);

        Vector2 t_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(t_direction.y, t_direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

            //FirePos.right = t_direction;
    }

    public bool nomalBullet;
    public bool followBullet;
    public bool bounceBullet;

    void TryFire()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            // ���ӹ߻� �Ѿ��� �����Ͽ�
            GameObject bullet = Instantiate(BulletFactory, FirePos.position, FirePos.rotation); ;
            // �߻��ϰ� �ʹ�.
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 10f;
            if (nomalBullet)
            {
                bullet.GetComponent<SY_Bullet>().NomalBullet();
            }

            if (followBullet)
            {
                SY_Bullet.instance.FollowBullet();
            }

            if (bounceBullet)
            {
                SY_Bullet.instance.BounceBullet();
            }
            

            //SY_FollowBullet.instance.GuideMissile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        TryFire();
    }
}

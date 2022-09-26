using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SY_FirePos : MonoBehaviourPun
{
    [SerializeField] GameObject BulletFactory = null;
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
        SY_Bullet.instance.BarrageBullet();
        SY_Bullet.instance.FollowBullet();
        SY_Bullet.instance.BounceBullet();
        SY_Bullet.instance.Huge();
        SY_Bullet.instance.Poison();
    }

    // 마우스가 바라보는 방향으로
    public void LookAtMouse()
    {
        //// 마우스 위치
        //Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        //// 마우스 위치 방향으로
        //Vector2 t_direction = new Vector2(t_mousePos.x - FirePos.position.x, t_mousePos.y - FirePos.position.y);

        Vector2 t_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(t_direction.y, t_direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        if (Input.GetButtonDown("Fire1"))
        {
            TryFire(rotation);
        }

        //FirePos.right = t_direction;
    }


    void TryFire(Quaternion rot)
    {
        // 연속발사 총알을 생성하여
        GameObject bullet = Instantiate(BulletFactory, FirePos.position, FirePos.rotation);
        bullet.GetComponent<SY_Bullet>().barrageBullet = false; // 일반총알
        bullet.GetComponent<SY_Bullet>().followBullet = true;  // 유도탄
        bullet.GetComponent<SY_Bullet>().bounceBullet = false;  // 바운스 총알
        bullet.GetComponent<SY_Bullet>().huge = false;  // hp증가
        bullet.GetComponent<SY_Bullet>().poison = false; // hp 점점 감소
        bullet.GetComponent<SY_Bullet>().quickShoot = false; // Quick shoot 총알 속도 증가
        bullet.GetComponent<SY_Bullet>().bigBullet = false; // big Bullet 총알 크기 증가
        // chase 캐릭터 이동속도 증가

    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
    }
}

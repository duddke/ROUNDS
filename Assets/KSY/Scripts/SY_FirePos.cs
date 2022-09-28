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
/*        SY_Bullet.instance.BarrageBullet();
        SY_Bullet.instance.FollowBullet();
        SY_Bullet.instance.BounceBullet();
        SY_Bullet.instance.Huge();
        SY_Bullet.instance.Poison();*/
    }

    // 마우스가 바라보는 방향으로
    public void LookAtMouse()
    {
        if (!GetComponentInParent<SY_PlayerMove>().isCreated) return;
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
            TryFire(rotation,gameObject.name.Contains("Red_Player"));
        }

        //FirePos.right = t_direction;
    }


    void TryFire(Quaternion rot, bool red)
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - FirePos.position;
        photonView.RPC("RpcTryFire", RpcTarget.MasterClient, FirePos.position,rot,red, dir);
    }
    [PunRPC]
    void RpcTryFire(Vector3 pos, Quaternion rot ,bool red, Vector3 dir)

    {
        // 연속발사 총알을 생성하여
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", pos, rot);
        SY_Bullet syBullet = bullet.GetComponent<SY_Bullet>();
        if (red)
        {
            syBullet.barrageBullet = SYA_CardManager.Instance.redCard[0]; // 일반총알
            syBullet.bigBullet = SYA_CardManager.Instance.redCard[1]; // big Bullet 총알 크기 증가
            syBullet.bounceBullet = SYA_CardManager.Instance.redCard[2];  // 바운스 총알

            syBullet.barrageBullet= SYA_CardManager.Instance.redCard[3]; //brawler
            syBullet.barrageBullet= SYA_CardManager.Instance.redCard[4]; //chase

            syBullet.poison = SYA_CardManager.Instance.redCard[5]; // hp 점점 감소
            syBullet.huge = SYA_CardManager.Instance.redCard[6];  // hp증가
            syBullet.followBullet = SYA_CardManager.Instance.redCard[7];  // 유도탄
            syBullet.quickShoot = SYA_CardManager.Instance.redCard[8]; // Quick shoot 총알 속도 증가

            syBullet.barrageBullet= SYA_CardManager.Instance.redCard[9]; //Burst
        }
        else
        {
            syBullet.barrageBullet = SYA_CardManager.Instance.blueCard[0]; // 일반총알
            syBullet.bigBullet = SYA_CardManager.Instance.blueCard[1]; // big Bullet 총알 크기 증가
            syBullet.bounceBullet = SYA_CardManager.Instance.blueCard[2];  // 바운스 총알

            syBullet.barrageBullet = SYA_CardManager.Instance.blueCard[3]; //brawler
            syBullet.barrageBullet = SYA_CardManager.Instance.blueCard[4]; //chase

            syBullet.poison = SYA_CardManager.Instance.blueCard[5]; // hp 점점 감소
            syBullet.huge = SYA_CardManager.Instance.blueCard[6];  // hp증가
            syBullet.followBullet = SYA_CardManager.Instance.blueCard[7];  // 유도탄
            syBullet.quickShoot = SYA_CardManager.Instance.blueCard[8]; // Quick shoot 총알 속도 증가

            syBullet.barrageBullet = SYA_CardManager.Instance.blueCard[9]; //Burst
        }
        bullet.GetComponent<SY_Bullet>().red = red;
        bullet.GetComponent<SY_Bullet>().dir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<SY_PlayerMove>().isCreated) return;
        LookAtMouse();
    }
}

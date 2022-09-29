using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SY_FirePos : MonoBehaviourPun
{
    //[SerializeField] GameObject BulletFactory = null;
    [SerializeField] Transform FirePos = null;
    AudioSource bulletImpact;
    public AudioClip clip; 

    Camera m_cam = null;
    public Vector2 t_mousePos;
    public Vector2 t_direction;

    public bool chase;

    public static SY_FirePos instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
        bulletImpact = GetComponent<AudioSource>(); 
/*        SY_Bullet.instance.BarrageBullet();
        SY_Bullet.instance.FollowBullet();
        SY_Bullet.instance.BounceBullet();
        SY_Bullet.instance.Huge();
        SY_Bullet.instance.Poison();*/
    }

    // ���콺�� �ٶ󺸴� ��������
    public void LookAtMouse()
    {
        if (!GetComponentInParent<SY_PlayerMove>().isCreated) return;
        //// ���콺 ��ġ
        //Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        //// ���콺 ��ġ ��������
        //Vector2 t_direction = new Vector2(t_mousePos.x - FirePos.position.x, t_mousePos.y - FirePos.position.y);

        Vector2 t_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(t_direction.y, t_direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetButtonDown("Fire1"))
        {
            TryFire(rotation,gameObject.name.Contains("Red_Player"));
            print("�߻�");
            bulletImpact.PlayOneShot(clip);
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
        // ���ӹ߻� �Ѿ��� �����Ͽ�
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", pos, rot);
        SY_Bullet syBullet = bullet.GetComponent<SY_Bullet>();
        if (red)
        {
            syBullet.barrageBullet = SYA_CardManager.Instance.redCard[0]; // �Ϲ��Ѿ�
            syBullet.bigBullet = SYA_CardManager.Instance.redCard[1]; // big Bullet �Ѿ� ũ�� ����
            syBullet.bounceBullet = SYA_CardManager.Instance.redCard[2];  // �ٿ �Ѿ�

            syBullet.brawler = SYA_CardManager.Instance.redCard[3]; //brawler 3�ʵ��� ü�� ����.
            syBullet.chase= SYA_CardManager.Instance.redCard[4]; //chase ĳ���� �̵�����      �̿Ϸ�

            syBullet.poison = SYA_CardManager.Instance.redCard[5]; // hp ���� ����
            syBullet.huge = SYA_CardManager.Instance.redCard[6];  // hp +10����
            syBullet.followBullet = SYA_CardManager.Instance.redCard[7];  // ����ź
            syBullet.quickShoot = SYA_CardManager.Instance.redCard[8]; // Quick shoot �Ѿ� �ӵ� ����

            syBullet.cannon = SYA_CardManager.Instance.redCard[9]; // Burst(cannon) ������ 2�� ����
        }
        else
        {
            syBullet.barrageBullet = SYA_CardManager.Instance.blueCard[0]; // �Ϲ��Ѿ�
            syBullet.bigBullet = SYA_CardManager.Instance.blueCard[1]; // big Bullet �Ѿ� ũ�� ����
            syBullet.bounceBullet = SYA_CardManager.Instance.blueCard[2];  // �ٿ �Ѿ�

            syBullet.brawler = SYA_CardManager.Instance.blueCard[3]; //brawler 3�ʵ��� ü�� ����.
            syBullet.chase = SYA_CardManager.Instance.blueCard[4]; //chase

            syBullet.poison = SYA_CardManager.Instance.blueCard[5]; // hp ���� ����
            syBullet.huge = SYA_CardManager.Instance.blueCard[6];  // hp����
            syBullet.followBullet = SYA_CardManager.Instance.blueCard[7];  // ����ź
            syBullet.quickShoot = SYA_CardManager.Instance.blueCard[8]; // Quick shoot �Ѿ� �ӵ� ����

            syBullet.cannon = SYA_CardManager.Instance.blueCard[9]; // Burst(cannon) ������ 2�� ����
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

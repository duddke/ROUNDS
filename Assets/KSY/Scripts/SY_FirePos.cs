using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_FirePos: MonoBehaviour
{
    [SerializeField] GameObject directBullet = null;
    [SerializeField] GameObject followBullet = null;
    [SerializeField] GameObject bounceBullet = null;
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
    }

    void LookAtMouse()
    {
        // ���콺 ��ġ
        Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        // ���콺 ��ġ ��������
        Vector2 t_direction = new Vector2(t_mousePos.x - FirePos.position.x, t_mousePos.y - FirePos.position.y);

        FirePos.right = t_direction;
    }

    private Vector3 prevPosition;
    void TryFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
                // ���ӹ߻� �Ѿ��� �����Ͽ�
                GameObject bullet = Instantiate(directBullet, FirePos.position, FirePos.rotation);
                // �߻��ϰ� �ʹ�.
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 10f;

           
        }
        if (Input.GetButtonDown("Fire2"))
        {
                // ���� �Ѿ��� �����Ͽ�
                GameObject bullet = Instantiate(followBullet, FirePos.position, FirePos.rotation);
                // �߻��ϰ� �ʹ�.
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 10f;
        }

  
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        TryFire();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_FirePos: MonoBehaviour
{
    [SerializeField] GameObject DirBullet = null;
    [SerializeField] GameObject ParabolaBullet = null;
    [SerializeField] Transform m_tfArrow = null;

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
        Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 t_direction = new Vector2(t_mousePos.x - m_tfArrow.position.x, t_mousePos.y - m_tfArrow.position.y);

        m_tfArrow.right = t_direction;
    }

    private Vector3 prevPosition;
    void TryFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject t_arrow = Instantiate(DirBullet, m_tfArrow.position, m_tfArrow.rotation);
            t_arrow.GetComponent<Rigidbody2D>().velocity = t_arrow.transform.right * 10f;
        }
        //if (Input.GetButtonDown("Fire2"))
        //{

        //    GameObject para = Instantiate(ParabolaBullet, m_tfArrow.position, m_tfArrow.rotation);
        //    para.GetComponent<Rigidbody2D>().velocity = para.transform.right * 10f;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        TryFire();
    }
}

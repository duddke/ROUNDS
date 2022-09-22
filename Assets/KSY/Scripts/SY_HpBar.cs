using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SY_HpBar : MonoBehaviour
{

    //[SerializeField] GameObject m_goPrefab = null;

    //List<Transform> m_objectList = new List<Transform>();
    //List<GameObject> m_hpBarList = new List<GameObject>();

    //Camera m_cam = null;

    [SerializeField]
    private Slider hpbar;
    float imsi;
    private float maxHp = 100;
    private float curHp = 100;

    public static SY_HpBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpbar.value = (float)curHp / (float)maxHp;
        //m_cam = Camera.main;

        //GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Player");
        //for (int i = 0; i < t_objects.Length; i++)
        //{
        //    m_objectList.Add(t_objects[i].transform);
        //    GameObject t_hpbar = Instantiate(m_goPrefab, t_objects[i].transform.position, Quaternion.identity, transform);
        //    m_hpBarList.Add(t_hpbar);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        hpbar.value = (float)curHp / (float)maxHp;

        //for (int i = 0; i < m_objectList.Count; i++)
        //{
        //    m_hpBarList[i].transform.position = m_cam.WorldToScreenPoint(m_objectList[i].position + new Vector3(0, 1f, 0));
        //}
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    HandleHp();
        //}
    }

    // 데미지 함수
    public void HandleHp()
    {
        curHp -= 10;
        if (curHp <= 0)
        {
            curHp = 0;
            // state => Die상태로 변경
            SY_PlayerMove.instance.state = SY_PlayerMove.State.Die;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


}

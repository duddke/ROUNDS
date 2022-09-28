using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_BlockNet : MonoBehaviourPun
{

    public float[] exGravity;
    Vector2 pos;
    Quaternion rot;

    public Rigidbody2D[] rigs;
    public Collider2D[] colls;
    public HingeJoint2D[] hinges;
    private void Awake()
    {
        pos = transform.position;
        rot = transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        transform.position = new Vector2(15, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (intro < 3)
        {
            Hinges();
            Coolls();
            Rigs();
        }
        
        if(!photonView.IsMine)
        {
            for (int i = 0; i < hinges.Length; ++i)
            {
                Destroy(hinges[i]);
                if (i >= hinges.Length) return;
            }
            for (int i = 0; i < rigs.Length; ++i)
            {
                Destroy(rigs[i]);
                if (i >= rigs.Length) return;
            }
        }
        else
        {
            if (!introEnd&&intro >= 3)
            {
                transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * 2);
                if (Vector2.Distance(transform.position, pos) < 0.2)
                {
                    for (int i = 0; i < colls.Length; ++i)
                    {
                        colls[i].enabled = true;
                        if (i >= colls.Length) return;
                    }
                    for (int i = 0; i < hinges.Length; ++i)
                    {
                        hinges[i].enabled = true;
                        if (i >= hinges.Length) return;
                    }
                    for (int i = 0; i < rigs.Length; ++i)
                    {
                        rigs[i].gravityScale= 1;
                        if (i >= rigs.Length) return;
                    }
                    transform.position = pos;
                    transform.rotation = rot;
                    print("11");
                    introEnd = true;
                    //OuttroBlock();
                }
            }
            if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
            {
                OuttroBlock();

                if (no)
                {
                    transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * 10);
                    if (Vector2.Distance(transform.position, pos) < 0.2)
                    {
                        //상태바꾸고, 맵 바꾸고
                        no = false;
                        print("11");
                    }
                }
            }
        }
    }

    //콜라이더들 배열로 담기
    void Coolls()
    {
        colls = new Collider2D[GetComponents<Collider2D>().Length];
        colls = GetComponents<Collider2D>();
        for (int i = 0; i < colls.Length; ++i)
        {
            colls[i].enabled = false;
            if (i >= colls.Length) return;
        }
        intro++;
    }

    //힌지들 배열로 담기
    void Hinges()
    {
        hinges = new HingeJoint2D[GetComponents<HingeJoint2D>().Length];
        hinges = GetComponents<HingeJoint2D>();
        for (int i = 0; i < hinges.Length; ++i)
        {
            hinges[i].enabled = false;
            if (i >= hinges.Length) return;
        }
        intro++;
    }

    bool rigsend;
    //리지드바디들 배열로 담기
    void Rigs()
    {
        if (rigsend) return;
        rigs = new Rigidbody2D[GetComponents<Rigidbody2D>().Length];
        rigs = GetComponents<Rigidbody2D>();
        for (int i = 0; i < rigs.Length; ++i)
        {
            rigs[i].gravityScale = 0;
            if (i >= rigs.Length) rigsend = true;
        }
        intro++;
    }

    public int intro;
    public bool introEnd;
    public bool no;
    public void OuttroBlock()
    {
        GetComponent<Collider2D>().enabled = false;
        pos = new Vector2(-15, transform.position.y);
        no = true;
    }
}

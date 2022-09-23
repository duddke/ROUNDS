using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_BlockNet : MonoBehaviourPun
{
    Rigidbody2D rig;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Rigidbody2D>())
        rig = GetComponent<Rigidbody2D>();
        pos=transform.position;
/*        GetComponent<Collider2D>().enabled = false;
        transform.position = new Vector2(15, transform.position.y);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            if (rig)
                Destroy(rig);
            if (GetComponent<HingeJoint2D>())
                Destroy(GetComponent<HingeJoint2D>());
                Destroy(GetComponent<HingeJoint2D>());
                Destroy(GetComponent<HingeJoint2D>());
                Destroy(GetComponent<HingeJoint2D>());
                Destroy(GetComponent<HingeJoint2D>());
            if (GetComponent<Collider2D>())
                GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * 2);
        if (Vector2.Distance(transform.position, pos) < 0.2)
        {
            GetComponent<Collider2D>().enabled = true;
            print("11");
            //OuttroBlock();
        }
        if(no)
        {
            transform.position = Vector2.Lerp(transform.position,pos, Time.deltaTime * 2);
            if (Vector2.Distance(transform.position, pos) < 0.2)
            {
                //상태바꾸고, 맵 바꾸고
                no = false;
                print("11");
            }
        }
        }
    }

    bool no;
    public void OuttroBlock()
    {
        GetComponent<Collider2D>().enabled = false;
        pos = new Vector2(-15, transform.position.y);
        no = true;
    }
}

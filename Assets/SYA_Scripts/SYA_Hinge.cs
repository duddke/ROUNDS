using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class SYA_Hinge : MonoBehaviour
{
    public Transform trans;
    HingeJoint2D hinge;
    Vector3 myPos;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        myPos = GetComponent<Collider2D>().bounds.center;
        float x = 0;
        if (trans.position.x < myPos.x)
            x = -(trans.position.x + myPos.x);
        else if (trans.position.x > myPos.x)
            x = trans.position.x - myPos.x;
        float y = 0;
        if (myPos.y < 0)
            y = trans.position.y + myPos.y;
        else
            y = trans.position.y - myPos.y;
        Vector2 pos = new Vector2(x, y);
        hinge.anchor = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

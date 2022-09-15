using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_CirMove : MonoBehaviour
{
    public GameObject brokenBlock;
    Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (brokenBlock != null)
        {
            rig.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rig.constraints = RigidbodyConstraints2D.None;
            transform.position = new Vector2(transform.position.x + 0.001f, transform.position.y + 0.001f);
        }

        //pos = Rope.GetComponent<SYA_Rope>().segments[1].position;
        //transform.position = pos;
    }
}

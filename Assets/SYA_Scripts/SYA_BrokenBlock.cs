using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_BrokenBlock : MonoBehaviour
{
    //public GameObject start;
    //public float dis = 1;
    //Rigidbody2D rig;

    //public GameObject Rope;

    // Start is called before the first frame update
    void Start()
    {
        //rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*        if (Vector2.Distance(transform.position, start.transform.position) > 1)
                    rig.gravityScale =0;
                else
                    rig.gravityScale=-9.18f;

        */
        //transform.position = Rope.GetComponent<SYA_Rope>().segments[1].position;
        if (Input.GetKeyDown(KeyCode.X))
            Destroy(gameObject);
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }*/
}

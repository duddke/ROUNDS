using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_PlayerNet : MonoBehaviourPun
{
    Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<SY_PlayerMove>().isCreated)
        {
            if (rig)
                Destroy(rig);
            if (GetComponent<Collider2D>())
                GetComponent<Collider2D>().enabled = false;
        }
    }
}

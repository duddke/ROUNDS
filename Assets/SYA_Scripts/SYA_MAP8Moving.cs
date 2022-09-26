using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_MAP8Moving : MonoBehaviourPun
{
    float runTime = 0;
    public float speed = 2;
    float yPos;
    public float length=5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
        if (GetComponent<SYA_BlockNet>().introEnd)
        {
            runTime += Time.fixedDeltaTime * speed;
            yPos = MathF.Sin(runTime) * length;
            transform.position = new Vector2(transform.position.x, transform.position.y + yPos);
        }
    }
}

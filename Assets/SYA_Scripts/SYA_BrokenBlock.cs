using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_BrokenBlock : MonoBehaviourPun
{


    // Start is called before the first frame update
    /*void Start()
    {
    }

    private void Update()
    {
        *//*if (Input.GetKeyDown(KeyCode.X))
            Destroy(gameObject);*//*
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        if(collision.gameObject.layer==30)
            PhotonNetwork.Destroy(gameObject);
    }
}

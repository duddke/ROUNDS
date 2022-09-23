using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_DestroyZone : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        //ÃÑ¾Ë »èÁ¦
        if (collision.gameObject.layer == 30)
            PhotonNetwork.Destroy(collision.gameObject);
    }
}

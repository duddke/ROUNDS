using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_DestroyZone_Damage : MonoBehaviourPun
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine) return;
        //플레이어 데미지 점프
        if (collision.gameObject.layer == 29)
        {
            //데미지 
            collision.gameObject.GetComponentInChildren<SY_HpBar>().HandleHp();//RPC구현해야함
            //온데미지 점프함수
            collision.gameObject.GetComponent<SY_PlayerMove>().JumpOn();

        }
    }

}

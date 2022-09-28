using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_DestroyZone_Damage : MonoBehaviourPun
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine) return;
        //�÷��̾� ������ ����
        if (collision.gameObject.layer == 29)
        {
            //������ 
            collision.gameObject.GetComponentInChildren<SY_HpBar>().HandleHp();//RPC�����ؾ���
            //�µ����� �����Լ�
            collision.gameObject.GetComponent<SY_PlayerMove>().JumpOn();

        }
    }

}

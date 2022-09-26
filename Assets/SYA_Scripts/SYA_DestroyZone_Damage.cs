using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_DestroyZone_Damage : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        //�÷��̾� ������ ����
        if (collision.gameObject.layer == 29)
        {
            //������ 
            collision.GetComponentInChildren<SY_HpBar>().HandleHp();//RPC�����ؾ���
            //�µ����� �����Լ�
            collision.GetComponent<SY_PlayerMove>().JumpOn();

        }
    }

}

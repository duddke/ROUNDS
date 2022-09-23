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
            //������ �Լ�
            collision.GetComponentInChildren<SY_HpBar>().HandleHp();
            //�µ����� �����Լ�
            collision.GetComponent<SY_PlayerMove>().Jump();

        }
    }
}

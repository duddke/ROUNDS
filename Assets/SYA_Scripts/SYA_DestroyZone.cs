using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_DestroyZone : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        //�Ѿ� ����
        if (collision.gameObject.layer == 30)
            Destroy(collision.gameObject);
        //�÷��̾� ������ ����
        if (collision.gameObject.layer == 29)
        {
            //������ �Լ�
            //�µ����� �����Լ�
        }
    }
}
